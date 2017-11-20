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

        public string Passport { get; private set; }
        public string Name { get; private set; }
        public Discount Discount { get; private set; }
        public ICollection<Ticket> Tickets { get; private set; }

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