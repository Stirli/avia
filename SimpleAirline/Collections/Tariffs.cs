using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleAirline
{
    public class Tariffs
    {
        private readonly DataProvider _provider;

        public Tariffs(DataProvider provider)
        {
            _provider = provider;
        }
        public void Create(Tariff entity)
        {
            _provider.Tariffs.Add(entity);
        }

        public IEnumerable<Tariff> GetAll()
        {
            return _provider.Tariffs;
        }
    }
}