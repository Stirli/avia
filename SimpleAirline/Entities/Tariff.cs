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
        /*
         * Конструктор
         * ArgumentException
         */
        public Tariff(string @from, string destination, DateTime date, double price)
        {
            From = @from;
            Destination = destination;
            Date = date;
            Price = price;
        }

        // ID тарифа
        public string Id { get; set; }

        // Пункт отправки
        public string From { get; set; }
        //Пункт назначения
        public string Destination { get; set; }
        // Дата
        public DateTime Date { get; set; }
        // Цена
        public double Price { get; set; }

        /*
         * Прибавляет стоимость
         * InvalidOperationException
         */
        public static Tariff operator +(Tariff tariff, double delta)
        {
            if (tariff.Price + delta < 0)
            {
                throw new InvalidOperationException("Сумма приведет к опусканию цены ниже нуля");
            }

            tariff.Price += delta;
            return tariff;
        }

        /*
         * Оператор явного приведения типа (для извдечения из строки)
         * FormatException
         */
        public static explicit operator Tariff(string s)

        {
            try
            {
                string[] strings = s.Split(';');
                return new Tariff(strings[1], strings[2], DateTime.Parse(strings[3]), double.Parse(strings[4]))
                {
                    Id = strings[0]
                };
            }
            catch (Exception ex)
            {
                throw new FormatException("Строка \"" + s + "\" не соответствует типу Tariff", ex);
            }
        }

        // Оператор явного приведения типа в строку (для сохранения в файл
        public static explicit operator string(Tariff s)
        {
            return s.Id + ";" + s.From + ";" + s.Destination + ";" + s.Date.ToString(CultureInfo.InstalledUICulture) + ";" + s.Price;
        }

        // Для вывода на экран в виде текста
        public override string ToString()
        {
            return String.Format("От {1}, До: {2}; Дата: {4} Цена: {3}$", Id, From, Destination, Price, Date);
        }
    }
}