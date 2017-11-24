using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    class Passangers
    {
        private readonly DataLoader _loader;

        public Passangers(DataLoader loader)
        {
            _loader = loader;
        }

        public void Create(Passanger passanger)
        {
            if (passanger == null)
            {
                throw new ArgumentNullException("passanger");
            }

            if (_loader.Passangers.ContainsKey(passanger.Passport) ||
                _loader.PassangersTickets.ContainsKey(passanger.Passport))
            {
                throw new ArgumentException("Пассажир уже существует", "passanger");
            }

            _loader.Passangers.Add(passanger.Passport, passanger);
            _loader.PassangersTickets.Add(passanger.Passport, new List<Ticket>());
        }

        public Passanger Get(string passport)
        {
            if (_loader.Passangers.ContainsKey(passport))
            {
                return _loader.Passangers[passport];
            }

            return null;
        }
        public List<Passanger> GetAll()
        {
            List<Passanger> list = new List<Passanger>();
            foreach (var value in _loader.Passangers.Values)
                list.Add(value);
            return list;
        }

        public List<Ticket> GetTickets(string passport)
        {
            return _loader.PassangersTickets[passport];
        }
    }
}
