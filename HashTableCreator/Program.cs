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
            FileStream file = new FileStream(path + "Records.txt", FileMode.Open);
            //GetStartPosition(path + "Records.txt", out List<int> positions);
        }

        
    }
}
