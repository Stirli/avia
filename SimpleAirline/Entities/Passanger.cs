using System;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    /*
     * 
     */
    // ??? (ПОЯСНИТЬ.) Класс "Passanger" ("Пассажир"). ??? Предоставляет ...
    public class Passanger
    {
        // Номер паспорта
        public string Passport { get; set; }
        // Имя
        public string Name { get; set; }
        // Скидка
        public Discount Discount { get; set; }

        /*
         * Парсит строку в пассажира
         * IndexOutOfRangeException
         */
        public static explicit operator Passanger(string str)
        {
            try
            {
                // делим строку на подстроки
                string[] strings = str.Split(';');
                // Создаем объект пассажира
                Passanger passanger = new Passanger { Passport = strings[0], Name = strings[1], Discount = (Discount)strings[2] };
                return passanger;
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
            return string.Format("ФИО: {0}. Номер паспорта: {1}. Скидка: {2}.", Name, Passport, Discount);
        }
    }
}