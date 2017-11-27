using System;
using System.Globalization;
using System.Text;

namespace SimpleAirline
{
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
        public string Passport { get; }
        // Имя пассажира
        public string Name { get; }
        // Скидка
        public Discount Discount { get; }
        // Пункт вылета
        public string From { get; }
        // Пункт назначения
        public string Destination { get; }
        // Время вылета
        public DateTime Date { get; }
        // Номер места
        public int SeatNo { get; set; }
        // Цена
        public double Price { get; set; }

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
            double discountPrice;
            if (Discount.DiscountType == DiscountType.Procent)
            {
                discountPrice = Price * (1 - Discount.Value);
            }
            else
            {
                discountPrice = Price - Discount.Value;
            }
            return string.Format(
                "ФИО: {0}\nРейс: {1} - {2}\nДата: {3:d} Время: {3:t}\nМесто: {4}\n Стоимость: {5}\n С учетом скидки({6}): {7}", Name,
                From, Destination, Date, SeatNo, Price, Discount,
                discountPrice);
        }
    }
}