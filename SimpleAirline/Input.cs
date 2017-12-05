// ??????????????????????????????????????????????????
// todo: Collections            Коллекции
// todo:     Airport                Аэропорт
// todo:     DataConext             Контекст данных
// todo:     DataLoadException      Исключение загрузки данных
// todo:     Passangers             Пассажиры
// todo:     Tariffs                Тарифы
// todo: Data                   Данные
// todo:     Discount               Скидка
// todo:     Passanger              Пассажир
// todo:     Tariff                 Тариф
// todo:     Ticket                 Билет
// ??????????????????????????????????????????????????



// ??????????
// ??????????????????????????????????????????????????
// todo: Input.cs ReadString
// todo: Input.cs ReadInt          // В "Input.cs" не используется.
// todo: Input.cs ReadDouble
// todo: Input.cs ReadDate
// todo: Input.cs ReadTariff       // В "Input.cs" не используется.
// todo: Input.cs ReadDiscount
// todo: Input.cs ReadPassanger    // В "Input.cs" не используется.
// ??????????????????????????????????????????????????


using System;
using System.Globalization;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    // ??? (ПОЯСНИТЬ.) Класс "Input" ("Ввод"). ...
    class Input
    {
        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // !!! Выводит сообщение (message) и читает строку.
        // ??? Если ввод был отменен - бросает исключение ApplicationException.
        // ??? + входные параметры?
        public static string ReadString(string message)
        {
            // +++ !!! Выводим сообщение.
            Console.Write(message);
            Console.Write(" (отмена - Ctrl+Z): ");

            // +++ !!! Читаем переменную из консоли.
            string val = Console.ReadLine();

            // ??? Ctrl+Z = null
            if (val == null)
            {
                throw new ApplicationException("\n  Ввод был отменен.");
            }

            // +++ !!! Возвращаем введенное значение.
            return val;
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // ??? Дальше идут 2 метода, почти одинаковые.

        // +++ !!! Выводит сообщение (message), запрашивает ввод, пока пользователь не введет корректное число,
        // либо срабатывает исключение ApplicationException в случае отмены ввода (ввод - Ctrl+Z = null)

        // ??? Исключение "ApplicationException" отсутствует в данном методе. Где оно срабатывает?

        // +++ !!! min - минимально допустимое число. max - максимально допустимое число.
        // ??? В строке ниже "Введите число". Где оно используется?
        public static int ReadInt(string message = "Введите число", int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    // +++ !!! Вывод сообщения.
                    // +++ !!! Приглашение к вводу.
                    string readLine = ReadString(message);

                    // +++ !!! Преобразование в число int.
                    int readInt = int.Parse(readLine);

                    // +++ !!! Если число не входит в заданный диапазон.
                    if (readInt < min || readInt > max)
                    {
                        // ??? Примерно такая же ошибка произойдет при обычном переполненнии типа. в int.Parse
                        // ??? Чтобы не дублировать сообщение об ощибке бросим исключение сами
                        throw new OverflowException();
                    }

                    // +++ !!! Возвращаем число.
                    return readInt;
                }
                // +++ !!! Введенная строка не является числом.
                catch (FormatException)
                {
                    Console.WriteLine("\n  Введенная строка не является целым числом.\n  Попробуйте еще раз.");
                }
                //
                catch (OverflowException)
                {
                    Console.WriteLine("\n  Число слшком большое или слишком маленькое.");
                    Console.WriteLine("  Допустимые значения: {0} - {1}.", min, max);
                    Console.WriteLine("  Попробуйте еще раз.");
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // ??? Где этот метод используется?

        // +++ !!! Выводит сообщение (message), запрашивает ввод, пока пользователь не введет корректное число,
        // либо срабатывает исключение ApplicationException в случае отмены ввода (ввод - Ctrl+Z = null)

        // ??? Исключение "ApplicationException" отсутствует в данном методе. Где оно срабатывает?

        // +++ !!! min - минимально допустимое число. max - максимально допустимое число.
        // ??? В строке ниже "Введите число". Где оно используется?
        public static double ReadDouble(string message = "Введите число", double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    // +++ !!! Вывод сообщения.
                    // +++ !!! Приглашение к вводу.
                    string readLine = ReadString(message);

                    // +++ !!! Преобразование в число double.
                    double readDouble = double.Parse(readLine);

                    // ??? Когда это сработает?
                    // +++ !!! Если число не входит в заданный диапазон.
                    if (readDouble < min || readDouble > max)
                    {
                        Console.WriteLine("Число слшком большое или слишком маленькое.");
                        Console.WriteLine("Допустимые значения: {0} - {1}.", min, max);
                        Console.WriteLine("Попробуйте еще раз.");
                    }

                    // +++ !!! Возвращаем число.
                    return readDouble;
                }
                // +++ !!! Введенная строка не является числом
                catch (FormatException)
                {
                    Console.WriteLine("    Введенная строка не является целым числом.\n    Попробуйте еще раз.");
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Выводит сообщение (message), запрашивает ввод, пока пользователь не введет корректное число,
        // либо срабатывает исключение ApplicationException в случае отмены ввода (ввод - Ctrl+Z = null)

        // ??? Исключение "ApplicationException" отсутствует в данном методе. Где оно срабатывает?

        // ??? В строке ниже "Дата и время вылета". Где оно используется?
        public static DateTime ReadDate(string message = "  Дата и время вылета.")
        {
            // ??? min - минимально допустимая дата.
            DateTime min = DateTime.Now;
            // ??? max - максимально допустимая дата.
            DateTime max = DateTime.MaxValue;

            while (true)
            {
                try
                {
                    // +++ !!! Вывод сообщения.
                    // +++ !!! Приглашение к вводу.
                    string readLine = ReadString(message);

                    // +++ !!! Преобразование в дату.
                    // ??? В скобках.
                    DateTime date = DateTime.Parse(readLine, CultureInfo.InstalledUICulture);

                    // +++ !!! Если число не входит в заданный диапазон.
                    if (date < min || date > max)
                    {
                        throw new OverflowException();
                    }

                    // +++ !!! Возвращаем дату.
                    return date;
                }
                // +++ !!! Введенная строка не является датой.
                catch (FormatException)
                {
                    Console.WriteLine("    Введенная строка не является датой.\n    Попробуйте еще раз.");
                }
                //
                catch (OverflowException)
                {
                    Console.WriteLine("    Дата слишком большая или слишком маленькая.");
                    Console.WriteLine("    Допустимые значения: {0} - {1}.", min, max);
                    Console.WriteLine("    Попробуйте еще раз.");
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Читаем информацию о тарифе.

        // ??? Исключение "ApplicationException" отсутствует в данном методе. Где оно срабатывает?
        public static Tariff ReadTariff()
        {
            // +++ !!! Вывод сообщения.
            Console.WriteLine("\nВвод данных о тарифе:");

            while (true)
            {
                try
                {
                    string fromName;
                    while (true)
                    {
                        // ??? @from
                        fromName = ReadString("  Вылет из");
                        //
                        if (fromName.Length >= 3) break;
                        Console.WriteLine("    Длина названия пункта отправления не может быть меньше 3 символов.");
                    }

                    string destination;
                    while (true)
                    {
                        // "destination" - "место назначения".
                        destination = ReadString("  Место назначения");

                        if (destination.Length >= 3) break;
                        Console.WriteLine("    Длина названия пункта назначения не может быть меньше 3 символов.");
                    }

                    // +++ !!! Читаем дату.
                    DateTime date = ReadDate();

                    // 
                    //double price = ReadDouble("Цена на " + DateTime.Now, 0);    // Было.
                    double price = ReadDouble("  Цена перелета");

                    //
                    if (price < 0)
                    {
                        //throw new ArgumentException("Цена не может быть меньше '0'.", "price");    // Было.
                        throw new ArgumentException("    Цена не может быть меньше '0'.");
                    }

                    // +++ !!! Создаем новый тариф.
                    // +++ !!! Передаем параметы: от кудо, куда, дату и времы, стоимость.
                    Tariff tariff = new Tariff(fromName, destination, date, price);

                    // +++ !!! Возвращаем тариф.
                    return tariff;
                }
                //
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Читает из консоли скидку (-50 или 50%).
        static Discount ReadDiscount()
        {
            while (true)
            {
                try
                {
                    // ??? Читаем строку и при помощия оператора явного приведения, определенного в Discount получаем и возвращаем.
                    // ??? Discount
                    // ??? Где используется то, что в кавычках?
                    Discount discount = (Discount)ReadString("  Введите скидку");

                    //
                    return discount;
                }
                //
                catch (ApplicationException)
                {
                    throw;
                }
                //
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Читаем из консоли инфо о пассажире
        public static Passanger ReadPassanger()
        {
            while (true)
            {
                try
                {
                    string passport;    // +++
                    while (true)
                    {
                        // +++ !!! Читаем скидку.
                        passport = ReadString("  Номер паспорта");

                        // +++ !!! Проверка длины паспорта.
                        if (passport.Length >= 9) break;
                        Console.WriteLine("    Длина номера паспорта не может быть меньше 9 символов.");    // +++
                    }

                    string name;
                    while (true)
                    {
                        // +++ !!! Читаем ФИО.
                        name = ReadString("  ФИО");

                        // +++ !!! ФИО д.б. не менее3 символов.
                        if (name.Length >= 3) break;
                        Console.WriteLine("    Введите хотя бы 3 символа.");    // +++
                    }

                    // ???
                    Passanger passanger = new Passanger { Passport = passport, Name = name, Discount = ReadDiscount() };

                    // +++ ??? Возвращаем пассажира.
                    return passanger;
                }
                //
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

    }
}
