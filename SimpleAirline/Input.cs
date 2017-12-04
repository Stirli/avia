// ??????????????????????????????????????????????????
// todo: Collections
// todo:     Airport
// todo:     DataConext
// todo:     DataLoadException
// todo:     Passangers
// todo:     Tariffs
// todo: Data
// todo:     Discount    // +++
// todo:     Passanger    // +++
// todo:     Tariff    // +++
// todo:     Ticket
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Input.cs ReadString
// todo: Input.cs ReadInt    // В "Input.cs" не используется.
// todo: Input.cs ReadDouble
// todo: Input.cs ReadDate
// todo: Input.cs ReadTariff    // В "Input.cs" не используется.
// todo: Input.cs ReadDiscount
// todo: Input.cs ReadPassanger    // В "Input.cs" не используется.
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Нужны ли все библиотеки?
// ??????????????????????????????????????????????????

using System;
//using System.Collections.Generic;
using System.Globalization;
//using System.Linq;
//using System.Text;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    // ??? (ПОЯСНИТЬ.) Класс "Input" ("Ввод"). ...
    class Input
    {
        /*
         * Выводит сообщение и читает строку. Если ввод был отменен - бросает исключение ApplicationException
         * message - сообщение
        */
        public static string ReadString(string message)
        {
            Console.Write(message);
            Console.Write(" (отмена - Ctrl+Z): ");
            string val = Console.ReadLine();
            if (val == null)
                throw new ApplicationException("\n  Ввод был отменен.");
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
                    Console.WriteLine("\n  Введенная строка не является целым числом.\n  Попробуйте еще раз.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("\n  Число слшком большое или слишком маленькое.");
                    Console.WriteLine("  Допустимые значения: {0} - {1}.", min, max);
                    Console.WriteLine("  Попробуйте еще раз.");
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
        public static DateTime ReadDate(string message = "  Дата и время вылета")
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
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВвод данных о тарифе:");
                    string @from = ReadString("  Вылет из");

                    if (@from.Length < 3)
                    {
                        // !!! Было.
                        //throw new ArgumentException("Длина названия пункта отправления не может быть меньше 3", "from");
                        throw new ArgumentException(
                            "\n  Длина названия пункта отправления не может быть меньше 3 символов.");
                    }

                    string destination = ReadString("  Место назначения");

                    if (destination.Length < 3)
                    {
                        // !!! Было.
                        //throw new ArgumentException("Длина названия назначения не может быть меньше 3", "destination");
                        throw new ArgumentException(
                            "\n  Длина названия пункта назначения не может быть меньше 3 символов.");
                    }
                    DateTime date = ReadDate();

                    //double price = ReadDouble("Цена на " + DateTime.Now, 0);    // Было.
                    double price = ReadDouble("  Цена перелета");

                    if (price < 0)
                    {
                        throw new ArgumentException("Цена не может быть меньше ", "price");
                    }

                    Tariff tariff = new Tariff(@from, destination, date, price);
                    return tariff;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
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
                    Discount discount = (Discount) ReadString("  Введите скидку");
                    return discount;
                }
                catch (ApplicationException)
                {
                    throw;
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
            while (true)
            {
                try
                {
                    string passport = ReadString("  Номер паспорта");    // +++

                    if (passport.Length < 9)
                    {
                        //throw new ArgumentException("Длина номера паспорта не может быть меньше 9", "Номер паспорта");    // !!! Было.
                        throw new ArgumentException("\n  Длина номера паспорта не может быть меньше 9 символов.");    // +++
                    }

                    string name = ReadString("  ФИО");
                    if (name.Length < 3)
                    {
                        //throw new ArgumentException("Введите хотя бы 3 символа", "ФИО");    // !!! Было.
                        throw new ArgumentException("\n  Введите хотя бы 3 символа.");    // +++
                    }
                    Passanger passanger = new Passanger() { Passport = passport, Name = name, Discount = ReadDiscount() };
                    return passanger;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
