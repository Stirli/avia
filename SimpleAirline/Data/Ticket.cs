using System;

namespace SimpleAirline
{
    public class Ticket
    {
        private static int lastId;

        public Ticket(Tariff tariff, Passanger passanger, int seatNo)
        {
            Tariff = tariff;
            Passanger = passanger;
            SeatNo = seatNo;
            Id = ++lastId;
        }
        public int Id { get; private set; }
        public Tariff Tariff { get; private set; }
        public Passanger Passanger { get; private set; }
        public int SeatNo { get; private set; }
        public double Price { get
        {
            Discount dis = Passanger.Discount;
            double price = Tariff.Price;
            return dis.DiscountType == DiscountType.Static ? price - dis.Value : price - price * dis.Value;
        }}
    }
}