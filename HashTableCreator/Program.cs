using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableCreator
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            HashTable hashTable = new HashTable();
            Stopwatch sw = Stopwatch.StartNew();
            hashTable.CreateHashTable(path + @"Records.txt");
            sw.Stop();
            Console.WriteLine($"Генерация хеш-таблицы заняла: {sw.Elapsed.TotalMilliseconds}мс");
            sw.Restart();
            hashTable.Serialize(path + @"Records.dat");
            sw.Stop();
            Console.WriteLine($"Сериализация хеш-таблицы заняла: {sw.Elapsed.TotalMilliseconds}мс");
            HashTable hash = new HashTable();
            sw.Restart();
            hash.Deserialize(path + @"Records.dat");
            sw.Stop();
            Console.WriteLine($"Десериализация хеш-таблицы заняла: {sw.Elapsed.TotalMilliseconds}мс");
            var table = hash.Table;
        }
    }
}
