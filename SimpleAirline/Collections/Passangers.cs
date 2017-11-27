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
            // Переносим элементы в новый список, чтобы он не был связан со списков в БД
            // На самом деле все еще можно изменить сами элементы, но добавить или удалить что-то уже нельзя
            List<Passanger> list = new List<Passanger>();
            foreach (var value in _conext.Passangers.Values)
            {
                list.Add(value);
            }
            return list;
        }

        /*
         * Возвращает билеты пассажира, null, если пассажир не найден
         * ArgumentNullException - passport - null
         * KeyNotFoundException - такого passport нет
         */
        public IEnumerable<Ticket> GetTickets(string passport)
        {
            // Создаем и Возвращаем список билетов
            List<Ticket> list = new List<Ticket>();
            foreach (var ticket in _conext.PassangersTickets[passport])
            {
                list.Add(ticket);
            }

            return list;
        }

        /*
         * Добавляет билет ticket в список билетов пассажира с паспортом passport
         * ArgumentNullException - passport - null
         * KeyNotFoundException - такого passport нет
         */
        public void BuyTicket(string passport, Ticket ticket)
        {
            // Возвращаем пассажира
            List<Ticket> tickets = _conext.PassangersTickets[passport];
            tickets.Add(ticket);
        }
    }
}
