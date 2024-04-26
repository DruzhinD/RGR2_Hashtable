using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HashTableCreator;

namespace SearchRecordsApp
{
    public partial class Form1 : Form
    {
        string directory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
        HashTable hashTable = new HashTable();
        public Form1()
        {
            InitializeComponent();

            FillListWithValues(directory + @"cities_info.txt");
            hashTable.Deserialize(directory + @"Records.dat");
        }

        //вывод списка доступных значений для поиска
        void FillListWithValues(string path)
        {
            StreamReader stream = new StreamReader(path, Encoding.Default);
            //считываем первые 4 неинтересующие строки
            for (int i = 0; i < 4; i++)
                stream.ReadLine();

            string[] items = stream.ReadLine().Split('|');
            listField.Items.AddRange(items);

            stream.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //очистка списка
            listIndexOfRecord.Items.Clear();

            //получаем индекс элемента в хеш-таблице
            int index = Array.FindIndex(hashTable.Table, x => {
                if (x == null)
                    return false;
                return x.key == textSearch.Text;
            });

            if (index == -1)
            {
                listIndexOfRecord.Items.Add(index);
                return;
            }
            //позиции символов, с которых начинаются записи
            string[] recordsId = new string[hashTable.Table[index].indexes.Count];
            for (int i = 0; i < recordsId.Length;i++)
                recordsId[i] = hashTable.Table[index].indexes[i].ToString();
            listIndexOfRecord.Items.AddRange(recordsId);
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            int recordId;
            if (!int.TryParse(textOutput.Text, out recordId) || recordId < 0)
            {
                textConcreteRecord.Text = "Указанная запись не найдена";
                return;
            }

            FileStream file = new FileStream(directory + @"Records.txt", FileMode.Open);

            //чтение конкретной записи из файла
            file.Seek(recordId, SeekOrigin.Begin);
            byte[] array = new byte[512];
            file.Read(array, 0, array.Length);

            //ищем индекс первого вхождения \r
            int rPos = Array.IndexOf(array, (byte)'\r');
            //декодированная запись
            string record = Encoding.Default.GetString(array, 0, rPos);

            textConcreteRecord.Text = record;

            file.Close();
        }
    }
}
