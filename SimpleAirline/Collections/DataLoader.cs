using System;
using System.Collections;
using System.Collections.Generic;
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
            Tariffs = new HashSet<Tariff>();
            Passangers = new HashSet<Passanger>();
        }
    }
}
