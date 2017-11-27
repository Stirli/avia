﻿using SimpleAirline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// TODO ВАЖНО Если возникнет вопрос: Почему у всех классов данных открытые (public, а не private) setтеры? 
// Ответ: при помощи такой коллекции, как ObservableCollection и интерфейса INotifyPropertyChanged можно отслеживать изменения в модели
// На уровне DataConext, и разрешать/запрещать изменения в зависимости от прав доступа
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
            catch (DataLoadException e)
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

        /*
         * Регистрация покупки билета
         */
        private static void RegTicket(Airport airport)
        {
            try
            {
                string passport = ReadString("Введите номер пасспорта");
                // Получаем инфо о пассажире
                Passanger passanger = airport.Passangers.Get(passport);
                if (passanger == null)
                {
                    Console.WriteLine("Пассажир не найден");
                    return;
                }

                Console.WriteLine(passanger.ToString());
                // получаем список тарифов
                IEnumerable<Tariff> tariffs = airport.Tariffs.GetAll();
                // Выбор тарифа из списка тарифов
                Tariff tariff = SelectItem(tariffs, "Выберите тариф");
                // ввод номера места
                int seatNo = ReadInt("Посадочное место");
                // Создаем билет
                Ticket ticket = new Ticket(passanger, tariff, seatNo);
                // добавляем билет в базу
                airport.Passangers.BuyTicket(passport, ticket);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не найден");
            }
        }

        /*
         * Выводит на консоль стоимость купленных билетов.
         * Исключения:
         * ApplicationException
         */
        private static void ShowPrice(Airport airport)
        {
            Console.WriteLine(PRICE_ALL);
            double sum = airport.AllTicketsPrice();
            if (double.IsInfinity(sum))
            {
                Console.WriteLine(
                    "Результат вычесления слишком большой. Обратитесь в службу поддержки.");
            }
            else
            {
                Console.WriteLine("Итого с учетом скидки: "+sum);
            }
        }

        /*
         * Выводит на консоль стоимость купленных пассажиром билетов.
         * Исключеня:
         * ApplicationException
         */
        private static void ShowPassangerPrice(Airport airport)
        {
            Console.WriteLine(PRICE_OF_PGER);
            try
            {
                // получаем номер паспорта
                string passport = ReadString("Введите номер паспорта пассажира");
                // Получаем список билетов
                IEnumerable<Ticket> tickets = airport.Passangers.GetTickets(passport);
                // Выводим на экран
                Print(tickets);
                // Получаем сумму. выбросит KeyNotFoundException
                double sum = airport.PassangersTicketsPrice(passport);
                // В случае переполнения double получится бесконечно число
                if (double.IsInfinity(sum))
                {
                    Console.WriteLine(
                        "Результат вычесления слишком большой. Обратитесь в службу поддержки.");
                }
                else
                {
                    // Если все нормально, выводим результат
                    Console.WriteLine("Итого с учетом скидки: "+sum);
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не неайден");
            }
        }
    }
}

