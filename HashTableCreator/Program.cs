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
        
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            HashTable hashTable = new HashTable();
            hashTable.CreateHashTable(path + @"Records.txt");
            hashTable.Serialize(path + @"Records.dat");
            HashTable hash = new HashTable();
            hash.Deserialize(path + @"Records.dat");
            var table = hash.Table;
        }
    }
}
