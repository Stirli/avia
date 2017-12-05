// ??????????????????????????????????????????????????
// todo: var
// ??????????????????????????????????????????????????

using System;
using System.Collections.Generic;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    /*
     * Предоставляет доступ к тарифам
     */

    // !!! Класс "Tariffs" ("Тарифы"). Предоставляет доступ к тарифам.
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
        {// длина не должна быть меньше 3
            if (tariff.From.Length < 3)
            {
                throw new ArgumentException("Длина навания отправления не может быть меньше 3", "from");
            }

            // длина не должна быть меньше 3
            if (tariff.Destination.Length < 3)
            {
                throw new ArgumentException("Длина навания назначения не может быть меньше 3", "destination");
            }

            // цена не может быть
            if (tariff.Price < 0)
            {
                throw new ArgumentException("Цена не может быть меньше ", "price");
            }
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