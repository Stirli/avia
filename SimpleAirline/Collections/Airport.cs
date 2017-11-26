using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    /*
     * Предоставляет Методы для удобного взаимодействия с БД
     */
    class Airport
    {
        /*
         * Хранит объект DataConext, загружающий данные в память и предоставляющий доступ непосредственно к ним
         */
        private DataConext _db;


        public Passangers Passangers { get; private set; }
        public Tariffs Tariffs { get; private set; }


        /*
         * Конструктор
         */
        public Airport()
        {
            // инициализация DataConext. DataConext читает данные из источника
            _db = new DataConext();

            // Создаем Passangers и Tariffs передаем им общий DataConext
            Passangers = new Passangers(_db);
            Tariffs = new Tariffs(_db);
        }

        /*
         * Вызывает сохранение данных
         */
        public void Save()
        {
            _db.Save();
        }

        /*
         * Стоимость всех билетов с учетом скидки
         */
        public double Sum()
        {
            // сумма
            double sum = 0;
            // перебираем списки билетов. _db.PassangersTickets - словарь, с ключом из паспорта пассажира и значением в виде списков
            foreach (List<Ticket> tickets in _db.PassangersTickets.Values)
            {
                // перебираем билеты
                foreach (Ticket ticket in tickets)
                {

                    sum += ticket.DiscountPrice;
                }
            }

            return sum;
        }

        /*
         * Стоимость всех билетов пассажира с учетом скидки
         * string passport - номер пасспорта пассажира
         * KeyNotFoundException
         */
        // 
        public double Sum(string passport)
        {
            double sum = 0;
            // Получаем конкретный спсиок билетов конкретного пассажира
            List<Ticket> tickets = _db.PassangersTickets[passport];
            foreach (Ticket ticket in tickets)
            {
                sum += ticket.DiscountPrice;
            }

            return sum;
        }
    }
}
