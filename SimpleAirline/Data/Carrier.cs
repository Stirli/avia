﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAirline.Data
{
    public class Carrier : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public  virtual ICollection<Tariff> Tariffs { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, ShortName);
        }
    }
}