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
            _loader.Passangers.Add(passanger);
        }

        public IEnumerable<Passanger> GetAll()
        {
            return _loader.Passangers;
        }

        public Passanger this[string index]
        {
            get
            {
                foreach (var passanger in _loader.Passangers)
                {
                    if (passanger.Passport.Equals(index)) return passanger;
                }
                return null;
            }
        }
    }
}
