using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    public class DataProvider
    {
        public ICollection<Tariff> Tariffs { get; private set; }
        public ICollection<Passanger> Passangers { get; private set; }

        public DataProvider()
        {
            try
            {
                Tariffs = new HashSet<Tariff>();
                foreach (string line in File.ReadLines("tariffs.txt"))
                {
                    Tariffs.Add(ParseTariff(line));
                }
                Passangers = new HashSet<Passanger>();
                foreach (string line in File.ReadLines("passangers.txt"))
                {
                    Passangers.Add(ParsePassanger(line));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Во время чтения данных произошла ошибка:" + e.Message, e);
            }
        }
        // Парсит строку в тариф
        private Tariff ParseTariff(string s)
        {
            string[] strings = s.Split(';');
            return new Tariff(strings[0], strings[1], DateTime.Parse(strings[2]), double.Parse(strings[3]));
        }

        // Парсит строку в пассажира
        private Passanger ParsePassanger(string s)
        {
            string[] strings = s.Split(';');
            /*
               Преобразование строки в Discount выполнено через оператор внутри класса Discount, а не при помощи метода
               так как Discount - очень простая структура данных логически и физически
             */
            return new Passanger(strings[0], strings[1], (Discount)strings[2]);
        }
    }
}
