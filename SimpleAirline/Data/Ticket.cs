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
            DiscountPrice = discount.DiscountType == DiscountType.Static
                  ? price - discount.Value
                  : price - price / 100 * discount.Value;
        }

        public string Passport { get; }
        public string Name { get; }
        public Discount Discount { get; }
        public string From { get; }
        public string Destination { get; }
        public DateTime Date { get; }
        public int SeatNo { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; private set; }

        public static explicit operator string(Ticket ticket)
        {
            return ticket.Passport + ";" + ticket.Name + ";" + ticket.Discount + ";" +
                ticket.From + ";" + ticket.Destination + ";" + ticket.Date.ToString(CultureInfo.InstalledUICulture) + ";" + ticket.SeatNo + ";" +
                ticket.Price;
        }

        public override string ToString()
        {
            return string.Format(
                "ФИО: {0}\nРейс: {1} - {2}\nДата: {3:d} Время: {3:t}\nМесто: {4}\n Стоимость: {5}\n С учетом скидки({6}): {7}", Name,
                From, Destination, Date, SeatNo, Price, Discount,
                DiscountPrice);
        }
    }
}