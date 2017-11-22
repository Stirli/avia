using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    class Airport
    {
        private DataLoader _db;
        public Airport()
        {
            _db = new DataLoader();
            Passangers = new Passangers(_db);
            Tariffs = new Tariffs(_db);
        }
        public Passangers Passangers { get; private set; }
        public Tariffs Tariffs { get; private set; }

        public void Save()
        {
            _db.Save();
        }

        public double Sum()
        {
            double sum = 0;
            foreach (var passanger in Passangers.GetAll())
                sum += passanger.TicketsPrice;
            return sum;
        }
        public double Sum(string id)
        {
            return Passangers[id].TicketsPrice;
        }
    }
}
