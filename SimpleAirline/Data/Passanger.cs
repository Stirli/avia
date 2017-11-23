using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAirline
{
    public class Passanger
    {
        public string Passport { get; set; }
        public string Name { get; set; }
        public Discount Discount { get; set; }

        // Парсит строку в пассажира
        public static explicit operator Passanger(string s)
        {
            string[] strings = s.Split(';');
            return new Passanger(){ Passport = strings[0], Name = strings[1], Discount = (Discount)strings[2] };
        }

        public static explicit operator string(Passanger passanger)
        {
            return passanger.Passport + ";" + passanger.Name + ";" + passanger.Discount;
        }

        public override string ToString()
        {
            return string.Format("{0}. Номер паспорта: {1}. Скидка: {2}", Name, Passport, Discount);
        }

        public override int GetHashCode()
        {
            return Passport != null ? Passport.GetHashCode() : 0;
        }

        protected bool Equals(Passanger other)
        {
            return string.Equals(Passport, other.Passport);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Passanger) obj);
        }
    }
}