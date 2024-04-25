using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableCreator
{
    internal class HashTable
    {
        //хранит индексы начала строк
        private List<int> indexesOfLines;

        /// <summary>Представляет собой хеш-таблицу</summary>
        public Dictionary<string, Record> Table { get; }

        private Dictionary<string, Record> table;

        /// <summary>Значение ключа в хеш-таблице</summary>
        internal class Record
        {
            /// <summary>true - элемент в наличие, false - запись пуста</summary>
            internal bool isExist;

            //не доступен для замены ссылки
            internal readonly List<int> indexes;

            internal Record(bool existing = false)
            {
                isExist = existing;
                indexes = new List<int>();
            }
        }

        public HashTable()
        {
            this.indexesOfLines = new List<int>();
            this.table = new Dictionary<string, Record>();
        }

        //Default - Default норм работает
        //13 - \r   10 - \n     7C (124) - |    44 - ,      65279 - нулевой ширины неразрывный пробел (начало файла)
        /// <summary>
        /// Индексирование записей, путем поиска порядкового номера первого символа в каждой записи
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void IndexRecords(string path)
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

        /// <summary>
        /// индексирование списка в каждой записи
        /// </summary>
        public void CreateHashTable(string[] items, string path)
        {
            //известно, что необходимый список начнется после достижения 4-го разделителя |
            char separator = '|';

            FileStream file = new FileStream(path, FileMode.Open);
        }

        public void CreateHashTable(string pathToFileWithItems, string pathToRecords, char separator = '|')
        {
            //читаем из потока элементы, которые будут ключами для хеш-таблицы 
            StreamReader reader = new StreamReader(pathToFileWithItems, Encoding.Default);
            string[] items = reader.ReadToEnd().Split(separator);
            reader.Close();
            //вызываем перегрузку текущего метода, которая создаст хеш-таблицу
            CreateHashTable(items, pathToRecords);
        }
    }
}
