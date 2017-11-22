using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    public class DataLoader
    {
        private const string TariffsTxt = "tariffs.txt";
        private const string PassangersTxt = "passangers.txt";
        public ICollection<Tariff> Tariffs { get; private set; }
        public ICollection<Passanger> Passangers { get; private set; }

        public DataLoader()
        {
            Tariffs = new HashSet<Tariff>();
            Passangers = new HashSet<Passanger>();
            try
            {
                IEnumerable<string> lines = File.ReadLines(TariffsTxt);
                foreach (string line in lines)
                {
                    Tariffs.Add((Tariff)line);
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException("Tariff", e);
            }

            try
            {
                IEnumerable<string> lines2 = File.ReadLines(PassangersTxt);
                foreach (string line in lines2)
                {
                    Passangers.Add(ParsePassanger(line));
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException("Passanger", e);
            }
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

        public void Save()
        {
            List<string> tariffsList = new List<string>();
            foreach (Tariff tariff in Tariffs)
                tariffsList.Add((string)tariff);
            File.WriteAllLines(TariffsTxt, tariffsList);
            List<string> list = new List<string>();
            foreach (var pass in Tariffs)
                list.Add(pass.ToString());
            File.WriteAllLines(PassangersTxt, list);
        }
    }
}
