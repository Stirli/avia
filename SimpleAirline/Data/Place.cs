using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleAirline.Data
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ShortName
        {
            get => default(int);
            set
            {
            }
        }

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