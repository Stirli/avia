using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    class Input
    {
        /*
         * Выводит сообщение и читает строку. Если ввод был отменен - бросает исключение ApplicationException
         * message - сообщение
        */
        public static string ReadString(string message)
        {
            Console.Write(message);
            Console.WriteLine(" Или CTRL+Z для отмены");
            string val = Console.ReadLine();
            if (val == null)
                throw new ApplicationException("Ввод был отменен.");
            return val;
        }

        /*
         * Выводит сообщение message, запрашивает ввод, пока пользователь не введет корректное число, 
         * либо выбрасывает исключение ApplicationException в случае отмены ввода (ввод ctrl+z)
         * message - сообщение
         * min - минимально допустимое число. max - максимально допустимое число
        */
        public static int ReadInt(string message = "Введите число", int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    // Вывод сообщения
                    // Приглашение к вводу
                    string readLine = ReadString(message);
                    // Преобразование в число
                    int readInt = int.Parse(readLine);
                    // Если число не входит в заданный диапазон
                    if (readInt < min || readInt > max)
                    {
                        // Примерно такая же ошибка произойдет при обычном переполненнии типа. в int.Parse
                        // Чтобы не дублировать сообщение об ощибке бросим исключение сами
                        throw new OverflowException();
                    }

                    return readInt;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введенная строка не является целым числом.\n Попробуйте еще раз");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слшком большое или слишком маленькое");
                    Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        /*
         * Выводит сообщение message, запрашивает ввод, пока пользователь не введет корректное число, 
         * либо выбрасывает исключение ApplicationException в случае отмены ввода (ввод ctrl+z)
         * message - сообщение
         * min - минимально допустимое число. max - максимально допустимое число
        */
        public static double ReadDouble(string message = "Введите число", double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    // Вывод сообщения
                    // Приглашение к вводу
                    string readLine = ReadString(message);
                    // Преобразование в число
                    // если было введено ctrl+z
                    double readDouble = double.Parse(readLine);
                    // Если число не входит в заданный диапазон
                    if (readDouble < min || readDouble > max)
                    {
                        Console.WriteLine("Число слшком большое или слишком маленькое");
                        Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                        Console.WriteLine("Попробуйте еще раз");
                    }
                    // Возвращаем число
                    return readDouble;
                }
                // введенная строка не является числом
                catch (FormatException)
                {
                    Console.WriteLine("Введенная строка не является целым числом.\n Попробуйте еще раз");
                }
            }
        }

        /* 
         * Полная аналогия с ReadInt, только min и max задаются в теле метода
         * message - сообщение
         * ApplicationException
         */
        public static DateTime ReadDate(string message = "Введите дату и время")
        {
            DateTime min = DateTime.Now;
            DateTime max = DateTime.MaxValue;
            while (true)
            {
                try
                {
                    string readLine = ReadString(message);
                    DateTime date = DateTime.Parse(readLine, CultureInfo.InstalledUICulture);
                    if (date < min || date > max)
                    {
                        throw new OverflowException();
                    }

                    return date;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введенная строка не является датой.\n Попробуйте еще раз");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Дата слшком большая или слишком маленькая");
                    Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        /*
         * Читает информацию о тарифе
         * ApplicationException
         */
        public static Tariff ReadTariff()
        {
            Console.WriteLine("Ввод данных о тарифе");
            string @from = ReadString("Вылет из");
            string destination = ReadString("Место назначения");
            DateTime date = ReadDate();
            double price = ReadDouble("Цена на " + DateTime.Now, 0);
            Tariff tariff = new Tariff(@from, destination, date, price);
            return tariff;
        }

        /*
         * Читает скидку вида -50 или 50%
         * ApplicationException
         */
        static Discount ReadDiscount()
        {
            while (true)
            {
                try
                {
                    // читаем строку и при помощия оператора явного приведения, определенного в  Discount получаем и возвращаем
                    // Discount
                    Discount discount = (Discount)ReadString("Введите скидку (-10 или 10%");
                    return discount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // Читаем из консоли инфо о пассажире
        public static Passanger ReadPassanger()
        {
            Passanger passanger = new Passanger() { Passport = ReadString("Номер пасспорта"), Name = ReadString("ФИО"), Discount = ReadDiscount() };
            return passanger;
        }
    }
}
