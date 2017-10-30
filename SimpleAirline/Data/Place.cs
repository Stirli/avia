using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAirline.Data
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
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