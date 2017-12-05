using System;
using System.Globalization;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    // ??? (ПОЯСНИТЬ.) Класс "Ticket" ("Билет"). ??? Предоставляет ...
    public class Ticket
    {
        public Ticket(Passanger passanger, Tariff tariff, int seatNo)
            : this(passanger.Passport,
                  passanger.Name,
                  passanger.Discount,
                  tariff.From,
                  tariff.Destination,
                  tariff.Date,
                  seatNo,
                  tariff.Price)

        {
        }

        public Ticket(string passport, string name, Discount discount, string @from, string destination, DateTime date, int seatNo, double price)
        {
            Passport = passport;
            Name = name;
            Discount = discount;
            From = from;
            Destination = destination;
            Date = date;
            SeatNo = seatNo;
            Price = price;
        }

        // Номер паспорта
        public string Passport { get; set; }
        // Имя пассажира
        public string Name { get; set; }
        // Скидка
        public Discount Discount { get; set; }
        // Пункт вылета
        public string From { get; set; }
        // Пункт назначения
        public string Destination { get; set; }
        // Время вылета
        public DateTime Date { get; set; }
        // Номер места
        public int SeatNo { get; set; }
        // Цена
        public double Price { get; set; }
        // Цена с учетом скидки
        // Цена с учетом скидки
        public double DiscountPrice
        {
            get
            {
                if (Discount.DiscountType == DiscountType.Static)
                {
                    return Price - Discount.Value;
                }

                return Price - Price / 100 * Discount.Value;
            }
        }

        // Оператор явного приведение (для сохранения в файл)
        public static explicit operator string(Ticket ticket)
        {
            return ticket.Passport + ";" + ticket.Name + ";" + ticket.Discount + ";" +
                ticket.From + ";" + ticket.Destination + ";" + ticket.Date.ToString(CultureInfo.InstalledUICulture) + ";" + ticket.SeatNo + ";" +
                ticket.Price;
        }

        // Для вывода на экран в виде текста
        public override string ToString()
        {
            return string.Format(
                "    ФИО: {0}.\n    Рейс: {1} - {2}.\n    Дата: {3:d}, время: {3:t}.\n    Место: {4}.\n    Стоимость: {5} р.\n    С учетом скидки ({6}): {7} р.", Name,
                From, Destination, Date, SeatNo, Price, Discount,
                DiscountPrice);
        }
    }
}