using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAirline
{
    /*
     * 
     */
    public class Passanger
    {
        public string Passport { get; set; }
        public string Name { get; set; }
        public Discount Discount { get; set; }

        /*
         * Парсит строку в пассажира
         * IndexOutOfRangeException
         */
        public static explicit operator Passanger(string str)
        {
            try
            {
                string[] strings = str.Split(';');
                return new Passanger { Passport = strings[0], Name = strings[1], Discount = (Discount)strings[2] };
            }
            catch (Exception e)
            {
                throw new FormatException("Строка '"+ str + "' не может быть верно преобразована в Passanger\n" + e.Message);
            }
        }

        /*
         * Преобразовывает в строку для 
         */
        public static explicit operator string(Passanger passanger)
        {
            return passanger.Passport + ";" + passanger.Name + ";" + passanger.Discount;
        }

        /*
         * Преобразовывает в строку для отображения на экране
         */
        public override string ToString()
        {
            return string.Format("{0}. Номер паспорта: {1}. Скидка: {2}", Name, Passport, Discount);
        }
    }
}