using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    class Passangers
    {
        private readonly DataProvider _provider;

        public Passangers(DataProvider provider)
        {
            _provider = provider;
        }

        public void Create(Passanger passanger)
        {
            _provider.Passangers.Add(passanger);
        }

        public IEnumerable<Passanger> GetAll()
        {
            return _provider.Passangers;
        }
    }
}
