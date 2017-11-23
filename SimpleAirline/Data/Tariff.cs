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

        public string Id { get; set; }

        public string From { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public static Tariff operator +(Tariff tariff, double delta)
        {
            if (tariff.Price + delta < 0)
            {
                throw new ArgumentOutOfRangeException("Сумма приведет к опусканию цены ниже нуля");
            }

            tariff.Price += delta;
            return tariff;
        }
        
        public override string ToString()
        {
            return String.Format("От {1}, До: {2}; Дата: {4} Цена: {3}$", Id, From, Destination, Price, Date);
        }

        public static explicit operator Tariff(string s)
        {
            string[] strings = s.Split(';');
            return new Tariff(strings[1], strings[2], DateTime.Parse(strings[3]), double.Parse(strings[4])) { Id = strings[0] };
        }

        public static explicit operator string(Tariff s)
        {
            return s.Id + ";" + s.From + ";" + s.Destination + ";" + s.Date.ToString(CultureInfo.InstalledUICulture) + ";" + s.Price;
        }
    }
}