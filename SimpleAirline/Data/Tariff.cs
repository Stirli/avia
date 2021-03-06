﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline.Data
{
    public class Tariff : IModel
    {
        public int Id { get; set; }
        public int? FromPlaceId { get; set; }
        public int? ToPlaceId { get; set; }
        public virtual Place FromPlace { get; set; }
        public virtual Place ToPlace { get; set; }
        public string SeatClass { get; set; }
        public double PriceUsd { get; set; }
        public virtual Carrier Carrier { get; set; }
        public override string ToString()
        {
            return String.Format("Перевозчик: {5}; От {1}, До: {2}; Класс: {3}; Цена: {4}$" , Id, FromPlace, ToPlace, SeatClass, PriceUsd, Carrier);
        }
    }
}
