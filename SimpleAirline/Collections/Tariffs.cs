using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleAirline
{
    public class Tariffs
    {
        private readonly DataLoader _loader;
        public Tariffs(DataLoader loader)
        {
            _loader = loader;
        }
        public void Create(Tariff entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _loader.Tariffs.Add(entity);
        }

        public List<Tariff> GetAll()
        {
            return _loader.Tariffs;
        }
    }
}