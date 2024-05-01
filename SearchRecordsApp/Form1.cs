using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

            hashTable.Deserialize(directory + @"Records.dat");
            FillListWithValues();
        }

        //вывод списка доступных значений для поиска
        void FillListWithValues()
        {
            var items = hashTable.Keys;
            listField.Items.AddRange(items);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //очистка списка
            listIndexOfRecord.Items.Clear();

            //получаем индекс элемента в хеш-таблице
            if (hashTable.ElementAtKey(textSearch.Text, out List<int> indexes))
            {
                //заполнение списка, который выводит список индексов
                string[] array = new string[indexes.Count];
                for (int i = 0; i < indexes.Count; i++)
                    array[i] = indexes[i].ToString();
                listIndexOfRecord.Items.AddRange(array);

                //вывод информации по количеству элементов
                textRecordCount.Text = string.Format("{0:N0}", indexes.Count);
            }
            //если не удалось найти информацию по ключу в хеш-таблице
            else
            {
                listIndexOfRecord.Items.Add(-1);
            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            int recordId;
            if (!int.TryParse(textOutput.Text, out recordId) || recordId < 0)
            {
                textConcreteRecord.Text = "Указанная запись не найдена";
                return;
            }

            StreamReader file = new StreamReader(directory + @"Records.txt", Encoding.Default);
            file.BaseStream.Seek(recordId, SeekOrigin.Begin);
            string record = file.ReadLine();
            file.Close();

            string[] splittedRecord = record.Split('|');
            textConcreteRecord.Lines = splittedRecord;

            //textConcreteRecord.Text = record;
            

        }

        private void listIndexOfRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            textOutput.Text = ((ListBox)sender).SelectedItem.ToString();
        }

        private void listField_SelectedIndexChanged(object sender, EventArgs e)
        {
            textSearch.Text = ((ListBox)sender).SelectedItem.ToString();
        }
    }
}
