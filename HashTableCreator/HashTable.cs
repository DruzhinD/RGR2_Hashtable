using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HashTableCreator
{
    internal class HashTable
    {
        /// <summary>Представляет собой хеш-таблицу</summary>
        public Record[] Table { get; }

        private Record[] table;

        /// <summary>Значение ключа в хеш-таблице</summary>
        internal class Record
        {
            public string key;
            /// <summary>true - элемент в наличие, false - запись пуста</summary>
            internal bool isExist;

            //не доступен для замены ссылки
            internal readonly List<int> indexes;

            internal Record(string key, bool existing, int lineIndex = -1)
            {
                this.key = key;
                isExist = existing;
                indexes = new List<int>();

                //если не равен -1, то добавляем в список
                if (lineIndex > -1)
                {
                    indexes.Add(lineIndex);
                }
            }
        }

        public HashTable()
        {
            this.indexesOfLines = new List<int>();
            this.table = new Record[59];
        }

        //Default - Default норм работает
        //13 - \r   10 - \n     7C (124) - |    44 - ,      65279 - нулевой ширины неразрывный пробел (начало файла)
        /// <summary>
        /// Индексирование записей, путем поиска порядкового номера первого символа в каждой записи
        /// </summary>
        /// <param name="path">путь к файлу</param>
        private void IndexRecords(string path)
        {
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

            /*#region проверка на соответствие. (Работает корректно)
            foreach (int pos in indexesOfLines)
            {
                file.Seek(pos, SeekOrigin.Begin);
                var c = file.ReadByte();
                byte[] chars = new byte[1];
                chars[0] = (byte)c;
                string symb = Encoding.Default.GetString(chars);
                Console.WriteLine($"Позиция: {pos}  первый символ {symb}");
            }
            #endregion*/

            file.Close();
        }

        //хранит индексы начала строк
        private List<int> indexesOfLines;

        /// <summary>
        /// индексирование списка в каждой записи
        /// </summary>
        public void CreateHashTable(string path)
        {
            //индексируем строки в файле
            IndexRecords(path);

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
                    KeyHashTable(element, indexesOfLines[i]); //работает

            }
            file.Close();
        }

        private char[] chars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();

        //добавляем новый ключ в хеш-таблицу
        private void KeyHashTable(string key, int lineIndex)
        {
            int recordPos = 0;
            foreach (char c in key)
                recordPos = Array.IndexOf(chars, c); //получаем сумму индексов символов

            //шаг, необходимый в случае коллизий
            int collision = 0;
            while (true)
            {
                //высчитываем действительную позицию элемента в хеш-таблице
                recordPos = (recordPos + collision) % table.Length;
                //если ячейка свободна
                if (table[recordPos] == null)
                {
                    table[recordPos] = new Record(key, true, lineIndex);
                    break;
                }
                //если текущий ключ в хеш-таблице уже есть, то просто добавляем новый элемент в список текущей записи
                else if (table[recordPos] != null && table[recordPos].key == key)
                {
                    table[recordPos].indexes.Add(lineIndex);
                    break;
                }

                //в случае не выполнения условий, двигаемся с шагом
                collision += 4;
            }
        }
    }
}
