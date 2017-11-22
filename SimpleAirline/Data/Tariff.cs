using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline
{
    public class Tariff
    {
        public Tariff(string @from, string destination, DateTime date, double price)
        {
            From = @from;
            Destination = destination;
            Date = date;
            Price = price;
        }

        public int Id { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public static Tariff operator +(Tariff tariff, double delta)
        {
            tariff.Price += delta;
            return tariff;
        }

        public Tariff Clone()
        {
            return new Tariff(From, Destination, Date, Price);
        }

        public override string ToString()
        {
            return String.Format("От {1}, До: {2}; Дата: {4} Цена: {3}$", Id, From, Destination, Price, Date);
        }


        public static explicit operator Tariff(string s)
        {
            string[] strings = s.Split(';');
            return new Tariff(strings[0], strings[1], DateTime.Parse(strings[2]), double.Parse(strings[3]));
        }
        public static explicit operator string(Tariff s)
        {
            return s.From + ";" + s.Destination + ";" + s.Date.ToString(CultureInfo.InstalledUICulture) +";"+s.Price;
        }
    }
}
