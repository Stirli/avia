using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    class Airport
    {
        private DataProvider _db;
        public Airport()
        {
            _db = new DataProvider();
            Passangers = new Passangers(_db);
            Tariffs = new Tariffs(_db);
        }
        public Passangers Passangers { get; private set; }
        public Tariffs Tariffs { get; private set; }
    }
}
