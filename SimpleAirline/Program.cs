using SimpleAirline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// TODO ВАЖНО Если возникнет вопрос: Почему у всех классов данных открытые (public, а не private) setтеры? 
// Ответ: при помощи такой коллекции, как ObservableCollection и интерфейса INotifyPropertyChanged можно отслеживать изменения в модели
// На уровне DataLoader, и разрешать/запрещать изменения в зависимости от прав доступа
namespace SimpleAirline
{
    class Program
    {
        private const string ADD_TARIFF = "Добавить тариф";
        private const string TARIFF_LIST = "Список тарифов";
        private const string REG_PASSANGER = "Зарегестрировать пассажира";
        private const string REG_TICKET = "Регистрировать покупку билета";
        private const string PRICE_OF_PGER = "Cтоимость купленных пассажиром билетов";
        private const string PRICE_ALL = "Cтоимость всех купленных билетов";
        private const string SAVE = "Сохранить данные на диск";
        private const string LIST_PASSANGER = "Список пассажиров";

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

        private static double ReadDouble(string message = "Введите число", double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    Console.WriteLine(" Или Ctrl+Z для отмены");
                    string readLine = Console.ReadLine();
                    double readInt = double.Parse(readLine);
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

        private static string ReadString(string message)
        {
            Console.Write(message);
            Console.WriteLine(" Или CTRL+Z для отмены");
            string val = Console.ReadLine();
            if (val == null)
                throw new ApplicationException("Ввод был отменен.");
            return val;
        }

        static Tariff ReadTariff()
        {
            Console.WriteLine("Ввод данных о тарифе");
            return new Tariff(ReadString("Из"), ReadString("В"), ReadDate(), ReadDouble("Цена", 0));
        }
        static Discount ReadDiscount()
        {
            while (true)
            {
                try
                {
                    return (Discount)ReadString("Введите скидку (-10 или 10%");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static Passanger ReadPassanger()
        {
            Passanger passanger = new Passanger() { Passport = ReadString("Номер пасспорта"), Name = ReadString("ФИО"), Discount = ReadDiscount() };
            return passanger;
        }
        static void Print<T>(IEnumerable<T> enumearble)
        {
            Console.WriteLine("---");
            foreach (T obj in enumearble)
            {
                Console.WriteLine(obj);
                Console.WriteLine("---");
            }
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
                Console.Write("{0,3}: ", ++i);
                // Выводим сам элемент
                Console.WriteLine(item);
            }
            return enumerable[ReadInt("Введите индекс действия:", 1, i) - 1];
        }

        static void Main(string[] args)
        {

            Airport airport;
            try
            {
                // Создаем пустой каталог
                airport = new Airport();

                while (true)
                {
                    // Меню
                    string select = SelectItem(new[]
                {
                        ADD_TARIFF,
                        TARIFF_LIST,
                        REG_PASSANGER,
                        LIST_PASSANGER,
                       REG_TICKET,
                        PRICE_ALL,
                        PRICE_OF_PGER,
                        SAVE
                    }, "Главное меню");
                    Console.Clear();
                    try
                    {
                        switch (select)
                        {
                            case ADD_TARIFF:
                                {
                                    airport.Tariffs.Create(ReadTariff());
                                }
                                break;
                            case TARIFF_LIST:
                                {
                                    Console.WriteLine(TARIFF_LIST);
                                    Print(airport.Tariffs.GetAll());
                                }
                                break;
                            case REG_PASSANGER:
                                {
                                    Passanger passanger = ReadPassanger();
                                    airport.Passangers.Create(passanger);
                                }
                                break;
                            case LIST_PASSANGER:
                                {
                                    Print(airport.Passangers.GetAll());
                                }
                                break;
                            case REG_TICKET:
                                {
                                    RegTicket(airport);
                                }
                                break;
                            case PRICE_ALL:
                                {
                                    ShowPrice(airport);
                                }
                                break;
                            case PRICE_OF_PGER:
                                {
                                    ShowPassangerPrice(airport);
                                }
                                break;
                            case SAVE:
                                {
                                    airport.Save();
                                }
                                break;
                        }
                    }
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Нажмите любую клавишу");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            catch (DataLoaderException e)
            {
                Console.WriteLine(e.Message, e);
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

        private static void RegTicket(Airport airport)
        {
            string passport = ReadString("Введите номер пасспорта");
            Passanger passanger = airport.Passangers.Get(passport);
            if (passanger == null)
            {
                Console.WriteLine("Пассажир не найден");
                return;
            }

            Console.WriteLine(passanger.ToString());

            ICollection<Ticket> tickets = airport.Passangers.GetTickets(passport);
            Tariff tariff = SelectItem(airport.Tariffs.GetAll(), "Выберите тариф");
            int seatNo = ReadInt("Посадочное место");

            tickets.Add(new Ticket(passanger, tariff, seatNo));
        }

        private static void ShowPrice(Airport airport)
        {
            Console.WriteLine(PRICE_ALL);
            double sum = airport.Sum();
            if (double.IsInfinity(sum))
            {
                Console.WriteLine(
                    "Результат вычесления слишком большой. Обратитесь в службу поддержки.");
            }
            else
            {
                Console.WriteLine(sum);
            }
        }

        private static void ShowPassangerPrice(Airport airport)
        {
            Console.WriteLine(PRICE_OF_PGER);
            try
            {
                string passport = ReadString("Введите номер паспорта пассажира");
                Print(airport.Passangers.GetTickets(passport));
                double sum = airport.Sum(passport);
                if (double.IsInfinity(sum))
                {
                    Console.WriteLine(
                        "Результат вычесления слишком большой. Обратитесь в службу поддержки.");
                }
                else
                {
                    Console.WriteLine(sum);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Пассажир не неайден");
            }
        }
    }
}

