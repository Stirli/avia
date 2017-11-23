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
            foreach (List<Ticket> tickets in _db.PassangersTickets.Values)
            {
                foreach (Ticket ticket in tickets)
                {
                    sum += ticket.DiscountPrice;
                }
            }

            return sum;
        }

        public double Sum(string passport)
        {
            double sum = 0;
            foreach (var t in _db.PassangersTickets[passport])
            {
                sum += t.DiscountPrice;
            }
            return sum;
        }
    }
}
