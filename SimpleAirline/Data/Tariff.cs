﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline.Data
{
    public class Tariff
    {
        public int Id { get; set; }
        public Place From { get; set; }
        public Place To { get; set; }
        public char SeatClass { get; set; }
        public double PriceUsd { get; set; }
        public Carrier Carrier { get; set; }
        public override string ToString()
        {
            return String.Format("ID: {0}; Перевозчик: {5}; От {1}, До: {2}; Класс: {3}; Цена: {4}$" , Id, From, To, SeatClass, PriceUsd, Carrier);
        }
    }
}
