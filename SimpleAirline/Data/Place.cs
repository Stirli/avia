using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAirline.Data
{
    public class Place
    {
        public Place()
        {
            FromTariffs = new List<Tariff>();
            ToTariffs = new List<Tariff>();
        }
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tariff> FromTariffs { get; set; }
        public virtual ICollection<Tariff> ToTariffs { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public static implicit operator string(Place p)
        {
            return p.Name;
        }
    }
}