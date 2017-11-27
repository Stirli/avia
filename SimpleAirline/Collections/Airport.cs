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
        private DataConext _context;


        public Passangers Passangers { get; private set; }
        public Tariffs Tariffs { get; private set; }


        /*
         * Конструктор
         */
        public Airport()
        {
            // инициализация DataConext. DataConext читает данные из источника
            _context = new DataConext();

            // Создаем Passangers и Tariffs передаем им общий DataConext
            Passangers = new Passangers(_context);
            Tariffs = new Tariffs(_context);
        }

        /*
         * Вызывает сохранение данных
         */
        public void Save()
        {
            _context.Save();
        }

        /*
         * Стоимость всех билетов с учетом скидки
         * InvalidOperationException - один из билетов имеет отрицательную стоимость с учетом скидки
         */
        public double AllTicketsPrice()
        {
            // сумма
            double sum = 0;
            // перебираем списки билетов. _db.PassangersTickets - словарь, с ключом из паспорта пассажира и значением в виде списков
            foreach (KeyValuePair<string, List<Ticket>> entry in _context.PassangersTickets)
            {
                List<Ticket> tickets = entry.Value;
                // перебираем билеты
                foreach (Ticket ticket in tickets)
                {
                    double discountPrice = ticket.DiscountPrice;
                    if (discountPrice < 0)
                    {
                        throw new InvalidOperationException("Билет пассажира " +
                           entry.Key + " имеет отрицательную стоимость с учетом скидки");
                    }

                    sum += discountPrice;
                }
            }

            return sum;
        }

        /*
         * Стоимость всех билетов пассажира с учетом скидки
         * string passport - номер пасспорта пассажира
         * KeyNotFoundException - пассажир не найден
         * InvalidOperationException - один из билетов имеет отрицательную стоимость с учетом скидки
         */
        // 
        public double PassangersTicketsPrice(string passport)
        {
            double sum = 0;
            // Получаем конкретный спсиок билетов конкретного пассажира
            List<Ticket> tickets = _context.PassangersTickets[passport];
            foreach (Ticket ticket in tickets)
            {
                double discountPrice = ticket.DiscountPrice;
                if (discountPrice < 0)
                {
                    throw new InvalidOperationException("Билет пассажира " +
                                                       passport + " имеет отрицательную стоимость с учетом скидки");
                }
                sum += ticket.DiscountPrice;
            }

            return sum;
        }
    }
}
