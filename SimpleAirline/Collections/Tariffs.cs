using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleAirline
{
    /*
     * Предоставляет доступ к тарифам
     */
    public class Tariffs
    {
        private readonly DataConext _conext;
        
        /*
         * Конструктор
         */
        public Tariffs(DataConext conext)
        {
            _conext = conext;
        }

        /*
         * Добавление тарифа
         */
        public void Create(Tariff tariff)
        {
            // В качестве ключа используем guid
            tariff.Id = Guid.NewGuid().ToString();
            // Добавить тариф
            _conext.Tariffs.Add(tariff);
        }

        /*
         * Возвращает тарифы
         */
        public IEnumerable<Tariff> GetAll()
        {
            List<Tariff> list = new List<Tariff>();
            foreach (var tariff in _conext.Tariffs)
                list.Add(tariff);
            return list;
        }
    }
}