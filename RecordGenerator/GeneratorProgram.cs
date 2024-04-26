using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RecordGenerator
{
    /*
    города
    страны
    год основания
    описание города
    список достопримечательностей
    -----------------------
    разделитель между элементами одного типа"|"
    */
    internal class GeneratorProgram
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            //чтение входных данных
            StreamReader reader = new StreamReader(path + "cities_info.txt", Encoding.Default);

            //коллекции, на основе которых будут генерироваться записи
            List<string> cities = new List<string>(50);
            ReadFileToList(reader, cities);
            List<string> countries = new List<string>(20);
            ReadFileToList(reader, countries);
            List<string> years = new List<string>(50);
            ReadFileToList(reader, years);
            List<string> descriptions = new List<string>(30);
            ReadFileToList(reader, descriptions);
            List<string> attractions = new List<string>(50); //достопримечательности
            ReadFileToList(reader, attractions);
            
            //закрываем рабочий поток
            reader.Close();
            Random rnd = new Random();

            //для просмотра затраченного времени на запись в файл
            Stopwatch watcher = Stopwatch.StartNew();

            //записываем информацию в файл в кодировке unicode
            StreamWriter writer = new StreamWriter(path + "Records.txt", false, Encoding.Default);

            int amountOfLines = 2;
            for (int i = 0; i < amountOfLines; i++)
            {
                //генерируем списки описания и достопримечательностей
                List<string> recordsDescr = SelectRandomElements(rnd, descriptions, 2);
                List<string> recordsAttrac = SelectRandomElements(rnd, attractions, 5);
                //создаем новую запись
                Record record = new Record(
                    cities[rnd.Next(0, cities.Count)], countries[rnd.Next(0, countries.Count)],
                    years[rnd.Next(0, years.Count)], recordsDescr, recordsAttrac);
                //сохраняем запись в файл в формате csv
                writer.WriteLine(record.ToCsvFormat());
            }
            writer.Close();
            Console.WriteLine($"Затраченное время на выполнение: {watcher.Elapsed.TotalSeconds:N2}сек. Нажмите Enter...");
            Console.ReadLine();
        }

        /// <summary>
        /// Читает содержимое первой строки и разбирает её на элементы по разделителю, с последующим сохранением в список
        /// </summary>
        static void ReadFileToList(in StreamReader reader, List<string> list)
        {
            if (reader.EndOfStream)
                throw new IOException("Конец файла, не удается прочитать содержимое дальше");

            string line = reader.ReadLine();
            list.AddRange(line.Split('|'));
        }

        static List<string> SelectRandomElements(in Random rnd, in List<string> elements, int amount)
        {
            List<string> rndElems = new List<string>(amount);
            for (int i = 0; i < amount; i++)
            {
                //достаем из списка случайный элемент
                string element = elements[rnd.Next(0, elements.Count)];
                //проверяем, был ли добавлен текущий случайный элемент ранее, чтобы избежать повторов
                if (rndElems.Contains(element))
                {
                    //сбрасываем текущую итерацию
                    i--;
                    continue;
                }
                rndElems.Add(element);
            }
            return rndElems;
        }
    }
}
