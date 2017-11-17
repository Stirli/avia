using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAirline.Data
{
    class DataContext
    {
        public List<Carrier> Carriers { get; private set; }
        private List<Place> _places;

        public List<Passanger> Passangers { get; set; }

        // Тарифы можно получить только через перевозчиков, так как именно они занимаются ценообразованием
        public IEnumerable<Tariff> Tariffs
        {
            get
            {
                List<Tariff> list = new List<Tariff>();
                foreach (Carrier carrier in Carriers)
                    foreach (Tariff tariff in carrier.Tariffs)
                        list.Add(tariff);
                return list;
            }
        }
    }
}
