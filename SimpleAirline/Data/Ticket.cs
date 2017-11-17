using System;

namespace SimpleAirline.Data
{
    public class Ticket
    {
        public string Id { get; set; }
        public Tariff Tariff { get; set; }
        public Passanger Passanger { get; set; }
        public int Gate { get; set; }
        public int SeatNo { get; set; }
    }
}