using SimpleAirline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleAirline;

namespace SimpleAirline
{
    class Program
    {
        private const string ADD_TARIFF = "Добавить тариф";
        private const string REG_TICKET = "Регистрировать покупку билета";
        private const string PRICE_OF_PGER = "Cтоимость купленных пассажиром билетов";
        private const string PRICE_ALL = "Cтоимость всех купленных билетов";

        private static int ReadInt(string message = "Введите число", int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    Console.WriteLine(" Или Ctrl+Z для отмены");
                    string readLine = Console.ReadLine();
                    int readInt = int.Parse(readLine);
                    if (readInt < min || readInt > max)
                    {
                        throw new OverflowException();
                    }

                    return readInt;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введенная строка не является целым числом.\n Попробуйте еще раз");
                }
                catch (ArgumentNullException)
                {
                    // если ввести ctr+z, а потом enter, то Console.ReadLine вернет null
                    throw new ApplicationException("Ввод был отменен");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слшком большое или слишком маленькое");
                    Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }
        private static DateTime ReadDate(string message = "Введите дату и время")
        {
            DateTime min = DateTime.Now;
            DateTime max = DateTime.MaxValue;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    Console.WriteLine(" Или Ctrl+Z для отмены");
                    string readLine = Console.ReadLine();
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
                catch (ArgumentNullException)
                {
                    // если ввести ctr+z, а потом enter, то Console.ReadLine вернет null
                    throw new ApplicationException("Ввод был отменен");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Дата слшком большая или слишком маленькая");
                    Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        private static string ReadString(string v)
        {
            Console.Write(v);
            Console.WriteLine(" Или CTRL+Z для отмены");
            string val = Console.ReadLine();
            if (val == null)
                throw new ApplicationException("Ввод был отменен.");
            return val;
        }

        static Tariff ReadTariff()
        {
            Console.WriteLine("Ввод данных о тарифе");
            return new Tariff(ReadString("Из"), ReadString("В"), ReadDate() , ReadInt("Цена", 0));
        }
        static Discount ReadDiscount()
        {
            Regex reg = new Regex(@"^(?<type>-)?(?<value>\d+)(?<type>%)?$");
            Match match = reg.Match(ReadString("Скидка (Статическа -50 или Процентная 50%"));

            DiscountType discountType = match.Groups["type"].Value.Equals("-") ? DiscountType.Static :
                match.Groups["type"].Value.Equals("%") ? DiscountType.Procent
                : throw new FormatException("Неверный тип скидки");
            return new Discount(double.Parse(match.Groups["value"].Value), discountType);
        }
        static Passanger ReadPassanger()
        {
            Passanger passanger = new Passanger(ReadString("Номер пасспорта"), ReadString("ФИО"), ReadDiscount());
            return passanger;
        }
        static void Print<T>(IEnumerable<T> enumearble)
        {
            Console.WriteLine("---");
            foreach (T obj in enumearble)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("---");
        }
        // Выводит список на экран, индексируя, и возвращает введенный пользователем индекс
        static T SelectItem<T>(IEnumerable<T> items, string message)
        {
            Console.WriteLine(message);
            // Счетчик
            int i = 0;
            IList<T> enumerable = items as IList<T> ?? items.ToList();
            foreach (T item in enumerable)
            {
                // Сначала выводим индекс
                Console.Write("{0,3}: ", i++);
                // Выводим сам элемент
                Console.WriteLine(item);
            }
            return enumerable[ReadInt("Введите индекс:", 0, i)];
        }

        static void Main(string[] args)
        {

            try
            {
                // Создаем пустой каталог
                Airport airport = new Airport();

                while (true)
                {
                    // Меню
                    string select = SelectItem(new[]
                {
                        ADD_TARIFF,
                       REG_TICKET,
                        PRICE_ALL,
                        PRICE_OF_PGER,
                    }, "Главное меню");
                    try
                    {
                        switch (select)
                        {
                            case ADD_TARIFF:
                                {
                                    airport.Tariffs.Create(ReadTariff());
                                }
                                break;
                            case REG_TICKET:
                                {
                                    Passanger passanger = ReadPassanger();
                                    // Clone() - создает новый объект с теми же данными. Изменения в тарифах не должны влиять на информацию в билете.
                                    Tariff tariff = SelectItem(airport.Tariffs.GetAll(), "Выберите тариф").Clone();
                                    passanger.Tickets.Add(new Ticket(tariff, passanger, ReadInt("Место", 1, 800)));
                                    airport.Passangers.Create(passanger);
                                }
                                break;
                            case PRICE_ALL:
                                {
                                    Console.WriteLine(PRICE_ALL);
                                    double sum = 0;
                                    foreach (var passanger in airport.Passangers.GetAll())
                                        sum += passanger.TicketsPrice;
                                    Console.WriteLine(sum);
                                }
                                break;
                            case PRICE_OF_PGER:
                                {

                                }
                                break;
                        }

                        Console.WriteLine("Нажмите любую клавишу");
                        Console.ReadKey(true);
                    }
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);
        }
    }
}

