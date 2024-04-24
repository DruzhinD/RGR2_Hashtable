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
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            FileStream f;
            char ch;
            byte[] array = new byte[2];
            f = new FileStream(path + "Records.txt", FileMode.Open);
            f.Seek(0, SeekOrigin.Begin);
            int count = f.Read(array, 0, array.Length);
            Console.WriteLine(Encoding.Default.GetString(array));
            f.Close();
        }
    }
}
