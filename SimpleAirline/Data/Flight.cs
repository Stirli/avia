using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline.Data
{
    // Представляет Рейс
    public class Flight
    {
        public string Id { get; set; }
        public virtual Place FromPlace { get; set; }
        public virtual Place ToPlace { get; set; }
        public DateTime Date { get; set; }
        public DateTime BoardingTill { get; set; }

    }
}
