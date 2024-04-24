using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordGenerator
{
    internal class Record
    {
        string city;
        string country;
        string yearOfFoundation;
        List<string> description = new List<string>(2);
        List<string> attractions = new List<string>(5);

        public Record(string city, string country, string yearOfFoundation, List<string> description, List<string> attractions)
        {
            this.city = city;
            this.country = country;
            this.yearOfFoundation = yearOfFoundation;
            this.description = description;
            this.attractions = attractions;
        }

        //преобразование записи в формат csv по указанному разделителю
        public string ToCsvFormat(string del = "|")
        {
            string str = $"{city}{del}{country}{del}{yearOfFoundation}{del}" +
                $"{string.Join(" ", description)}{del}{string.Join(", ", attractions)}";
            return str;
        }
    }
}
