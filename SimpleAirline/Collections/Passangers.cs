using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    /*
     * Предоставляет доступ к информации о пассажирах и их билетах
     */
    class Passangers
    {
        private readonly DataConext _conext;

        // Исключенения: DataloaderException
        public Passangers(DataConext conext)
        {
            _conext = conext;
        }


        public void Create(Passanger passanger)
        {
            if (passanger == null)
            {
                throw new ArgumentNullException("passanger");
            }

            if (_conext.Passangers.ContainsKey(passanger.Passport) ||
                _conext.PassangersTickets.ContainsKey(passanger.Passport))
            {
                throw new ArgumentException("Пассажир уже существует", "passanger");
            }

            _conext.Passangers.Add(passanger.Passport, passanger);
            _conext.PassangersTickets.Add(passanger.Passport, new List<Ticket>());
        }

        /*
         * Возвращает информацию о пассажире, null - если пассажир не найден
         * passport - номер пасспорта
         */
        public Passanger Get(string passport)
        {
            if (_conext.Passangers.ContainsKey(passport))
            {
                return _conext.Passangers[passport];
            }

            return null;
        }

        /*
         * Возвращает пассажиров
         */
        public IEnumerable<Passanger> GetAll()
        {
            List<Passanger> list = new List<Passanger>();
            foreach (var value in _conext.Passangers.Values)
                list.Add(value);
            return list;
        }

        /*
         * Возвращает билеты пассажира, null, если пассажир не найден
         * ArgumentNullException - passport == null (выбрасывает .ContainsKey(passport) )
         */
        public ICollection<Ticket> GetTickets(string passport)
        {
            // проверяем, есть ли в базе пассажиры с таким пасспартом
            if (_conext.Passangers.ContainsKey(passport))
            {
                // Возвращаем пассажира
                return _conext.PassangersTickets[passport];
            }
            // Возвращаем null, если нет
            return null;
        }
    }
}
