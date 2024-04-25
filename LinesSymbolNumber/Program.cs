using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableCreator
{
    internal class Program
    {
        //Default - Default норм работает
        //13 - \r   10 - \n     7C (124) - |    44 - ,      65279 - нулевой ширины неразрывный пробел (начало файла)
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            FileStream file = new FileStream(path + "Records.txt", FileMode.Open);
            GetStartPosition(file, out List<int> positions);
            file.Close();
            Console.WriteLine(string.Join(", ", positions));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">инициализированный поток</param>
        /// <param name="positions"></param>
        static void GetStartPosition(FileStream file, out List<int> positions)
        {
            //здесь будут храниться начало строк
            positions = new List<int>();
            byte[] buffer = new byte[16];
            file.Seek(0, SeekOrigin.Begin);

            //количество считанных байт
            int count = 0;
            //добавляем индекс начала первой строки в список
            positions.Add(0);
            do
            {
                //file.Seek(count, SeekOrigin.Current);
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
                        positions.Add(nextLine);
                    }
                }

            } while (count > 0);

            #region проверка на соответствие. (Работает корректно)
            foreach (int pos in positions)
            {
                file.Seek(pos, SeekOrigin.Begin);
                var c = file.ReadByte();
                byte[] chars = new byte[1];
                chars[0] = (byte)c;
                Console.WriteLine($"Позиция: {pos}  первый символ {Encoding.Default.GetString(chars)}");
            }
            #endregion
        }
    }
}
