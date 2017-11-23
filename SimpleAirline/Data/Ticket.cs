using System;

namespace SimpleAirline
{
    public class Ticket
    {
        public Ticket(Tariff tariff, Passanger passanger, int seatNo, double price)
        {
            Tariff = tariff;
            Passanger = passanger;
            SeatNo = seatNo;
            Price = price;
        }

        public Ticket(Tariff tariff, Passanger passanger, int seatNo)
        {
            Tariff = tariff;
            Passanger = passanger;
            SeatNo = seatNo;
            Discount dis = Passanger.Discount;
            double price = Tariff.Price;
            Price = dis.DiscountType == DiscountType.Static ? price - dis.Value : price - price * dis.Value;
        }
        public Tariff Tariff { get; set; }
        public Passanger Passanger { get; set; }
        public int SeatNo { get; set; }
        public double Price { get; set; }

        public static explicit operator string(Ticket ticket)
        {
            return ticket.Tariff.Id + ";" + ticket.Passanger.Passport + ";" + ticket.SeatNo + ";" + ticket.Price;
        }
    }
}