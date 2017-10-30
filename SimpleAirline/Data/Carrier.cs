using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAirline.Data
{
    public class Carrier
    {
        public Carrier()
        {
            Tariffs = new List<Tariff>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<Tariff> Tariffs { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, ShortName);
        }
    }
}