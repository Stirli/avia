using SimpleAirline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            return enumerable[Input.ReadInt("Введите индекс действия:", 1, i) - 1];
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
                                    airport.Tariffs.Create(Input.ReadTariff());
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
                                    Passanger passanger = Input.ReadPassanger();
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
                string passport = Input.ReadString("Введите номер пасспорта");
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
                int seatNo = Input.ReadInt("Посадочное место");
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
            try
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
                    Console.WriteLine("Итого с учетом скидки: " + sum);
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
         * Выводит на консоль стоимость купленных пассажиром билетов.
         * Исключеня:
         * ApplicationException - ввод отменен
         */
        private static void ShowPassangerPrice(Airport airport)
        {
            Console.WriteLine(PRICE_OF_PGER);
            try
            {
                // получаем номер паспорта
                string passport = Input.ReadString("Введите номер паспорта пассажира");
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
                    Console.WriteLine("Итого с учетом скидки: " + sum);
                }
            }
            // стоимость билета со сскидкой меньше 0
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            // при попытке извлечь список билетов по несуществующему пасспорту из DataContext.PassangersTickets
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не неайден");
            }
        }
    }
}

