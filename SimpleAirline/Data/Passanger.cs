using System.Collections.Generic;
using System.Linq;

namespace SimpleAirline
{
    public class Passanger
    {
        public Passanger(string passport, string name, Discount discount)
        {
            Passport = passport;
            Name = name;
            Discount = discount;
            Tickets = new List<Ticket>();
        }

        public string Passport { get; set; }
        public string Name { get; set; }
        public Discount Discount { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }

        // Парсит строку в пассажира
        public static explicit operator Passanger(string s)
        {
            string[] strings = s.Split(';');
            return new Passanger(strings[0], strings[1], (Discount)strings[2]);
        }

        public static explicit operator string(Passanger passanger)
        {
            return passanger.Passport + ";" + passanger.Name + ";" + passanger.Discount;
        }

        // Стоимость купленных пассажиром билетов
        public double TicketsPrice
        {
            get
            {
                double sum = 0;
                foreach (var ticket in Tickets)
                    sum += ticket.Price;
                return sum;
            }
        }

    }
}