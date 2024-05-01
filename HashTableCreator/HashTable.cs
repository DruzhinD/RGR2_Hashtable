using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HashTableCreator
{
    public class HashTable
    {
        /// <exception cref="KeyNotFoundException"></exception>
        public List<int> this[string key]
        {
            get {
                if (ElementAtKey(key, out List<int> indexes))
                    return indexes;
                else
                    throw new KeyNotFoundException("Указанного ключа не существует");
            }
        }

        ///<summary>Получение всех имеющихся ключей в хеш-таблице</summary> 
        public string[] Keys
        {
            get
            {
                List<string> keys = new List<string>(50);
                foreach (var pair in table)
                {
                    if (pair.Key == false)
                        continue;

                    if (pair.Value != null)
                        keys.Add(pair.Value.key);
                }
                return keys.ToArray();
            }
        }

        //true - ключ по указанному индексу существует или был удален, false - ключа не существовало
        private KeyValuePair<bool, Record>[] table;

        /// <summary>Значение ключа в хеш-таблице</summary>
        [Serializable]
        private class Record
        {
            public string key;

            //не доступен для замены ссылки
            public readonly List<int> indexes;

            internal Record(string key, int lineIndex = -1)
            {
                this.key = key;
                indexes = new List<int>();

                //если не равен -1, то добавляем в список
                if (lineIndex > -1)
                {
                    indexes.Add(lineIndex);
                }
            }
        }

        public HashTable() { }

        //хранит индексы начала строк
        private List<int> indexesOfLines;

        /// <summary>
        /// Индексирование записей, путем поиска порядкового номера первого символа в каждой записи
        /// </summary>
        /// <param name="path">путь к файлу</param>
        private void IndexRecords(string path)
        {
            indexesOfLines = new List<int>();

            FileStream file = new FileStream(path, FileMode.Open);
            //здесь будут храниться начало строк
            indexesOfLines = new List<int>();
            byte[] buffer = new byte[256];
            file.Seek(0, SeekOrigin.Begin);

            //количество считанных байт
            int count = 0;
            //добавляем индекс начала первой строки в список
            indexesOfLines.Add(0);
            do
            {
                count = file.Read(buffer, 0, buffer.Length);

                //проверяем наличие переноса строки
                for (int i = 0; i < count; i++)
                {
                    //13=\r   10=\n
                    //если был обнаружен символ \r, то записываем позицию начала следующей строки
                    if (buffer[i] == 13)
                    {
                        //смещение на 2, т.к. начало индекс начала следующей строки на 2 больше, чем индекс \r
                        //Position - count = индекс последнего считанного байта во время предыдущей итерации
                        //(count - (i + 2)) = на сколько смещена строка от начала записанного потока (относительное смещение)
                        int nextLine = (int)(file.Position - (count - (i + 2)));
                        indexesOfLines.Add(nextLine);
                    }
                }

            } while (count > 0);

            //если последний индекс в списке при чтении равен -1, то удаляем последний индекс из списка, т.к. он указывает на пустую строку
            file.Seek(indexesOfLines[indexesOfLines.Count - 1], SeekOrigin.Begin);
            if (file.ReadByte() == -1)
                indexesOfLines.RemoveAt(indexesOfLines.Count - 1);

            file.Close();
        }

        //размер хеш-таблицы
        private int tableLength = 59;

        /// <summary>
        /// индексирование списка в каждой записи. Генерация хеш-таблицы
        /// </summary>
        public void CreateHashTable(string path)
        {
            //индексируем строки в файле
            IndexRecords(path);

            table = new KeyValuePair<bool, Record>[tableLength];

            //известно, что необходимый список начнется после достижения 4-го разделителя |
            char separator = '|';

            FileStream file = new FileStream(path, FileMode.Open);

            //проходим по списку с началами строк
            for (int i = 0; i < indexesOfLines.Count; i++)
            {
                //счетчик разделителей
                int separatorCounter = 0;
                //устанавливаем позицию в потоке по указанному индексу
                file.Seek(indexesOfLines[i], SeekOrigin.Begin);
                byte[] array = new byte[512];
                file.Read(array, 0, array.Length);

                //хранит индекс, с которого начинается поля со списком
                int listIndex = 0;
                //проход по массиву байт, для поиска разделителей
                for (int b = 0; b < array.Length; b++)
                {
                    //если встречен разделитель, то увеличиваем счетчик
                    if (array[b] == (int)separator)
                    {
                        separatorCounter++;
                        //при встрече последнего разделителя сохраняем индекс сохраняем индекс в массиве,
                        //с которого начинается поле со списком
                        if (separatorCounter == 4)
                        {
                            listIndex = b + 1;
                            break;
                        }
                    }
                }

                //получаем индекс каретки в массиве
                int rPos = Array.IndexOf(array, (byte)'\r');
                string fieldWithList = Encoding.Default.GetString(array, listIndex, rPos - listIndex); //работает

                string[] list = fieldWithList.Split(',');

                //заполяем хеш-таблицу имеющимися значениями из поля со списком
                foreach (string element in list)
                    CreateKeyHashTable(element, indexesOfLines[i]); //работает

            }
            file.Close();
        }

        private char[] chars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя ".ToCharArray();

        //добавляем новый ключ в хеш-таблицу
        private void CreateKeyHashTable(string key, int lineIndex)
        {
            int recordPos = 0;
            foreach (char c in key)
                recordPos += Array.IndexOf(chars, c); //получаем сумму индексов символов

            //шаг, необходимый в случае коллизий
            int collision = 0;
            while (true)
            {
                //высчитываем действительную позицию элемента в хеш-таблице
                recordPos = (recordPos + collision) % table.Length;
                //если в текущей ячейке не существовало ключа
                if (table[recordPos].Key == false)
                {
                    Record newRecord = new Record(key, lineIndex);
                    table[recordPos] = new KeyValuePair<bool, Record>(true, newRecord);
                    break;
                }
                //если текущий ключ в хеш-таблице уже есть, то просто добавляем новый элемент в список текущей записи
                else if (table[recordPos].Key == true && table[recordPos].Value.key == key)
                {
                    table[recordPos].Value.indexes.Add(lineIndex);
                    break;
                }

                //в случае не выполнения условий, двигаемся с шагом
                collision += 4;
            }
        }

        /// <summary>
        /// Поиск информации по ключу. Функционал аналогичен индексатору
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="indexes">массив индексов, где индекс - первый символ строки, в которой встречается ключ <br/>
        /// null - если ключ не найден</param>
        /// <returns>true - ключ найден, иначе false</returns>
        public bool ElementAtKey(string key, out List<int> indexes)
        {
            //потенциальная позиция записи в массиве
            int recordPos = 0;
            const int step = 4; //шаг сдвига в случае коллизии

            foreach (char c in key)
                recordPos += Array.IndexOf(chars, c); //получаем сумму индексов символов

            //шаг, необходимый в случае коллизий
            int collision = 0;
            while (true)
            {
                //высчитываем действительную позицию элемента в хеш-таблице
                recordPos = (recordPos + collision) % table.Length;

                //если по указанному hash записи не существует или не существовало, то возвращает false 
                if (table[recordPos].Key == false)
                {
                    indexes = null;
                    return false;
                }

                //здесь, table[recordPos].Key равен true
                if (table[recordPos].Value != null && table[recordPos].Value.key == key)
                {
                    indexes = table[recordPos].Value.indexes;
                    return true;
                }

                //в случае не выполнения условий, двигаемся с шагом
                collision += step;
            }
        }

        /// <summary>
        /// Преобразование хеш-таблицы в словарь
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public Dictionary<string, List<int>> ToDictionary()
        {
            if (table == null)
                throw new ArgumentNullException("Хеш-таблица не заполнена значениями");

            Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>(50);
            foreach (KeyValuePair<bool, Record> pair in table)
            {
                if (pair.Key == false)
                    continue;

                if (pair.Value != null)
                    dictionary.Add(pair.Value.key, pair.Value.indexes);
            }
            return dictionary;
        }

        /// <exception cref="ArgumentNullException"/>
        public void Serialize(string path)
        {
            if (table == null)
                throw new ArgumentNullException("Хеш-таблица не заполнена значениями");

            FileStream binaryFile = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(binaryFile, table); //сериализуем хеш-таблицу
            binaryFile.Close();
        }

        public void Deserialize(string path)
        {
            FileStream binaryFile = new FileStream(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            table = (KeyValuePair<bool, Record>[])bf.Deserialize(binaryFile);
            binaryFile.Close();
        }
    }
}
