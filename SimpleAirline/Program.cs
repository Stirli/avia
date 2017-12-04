// ??????????????????????????????????????????????????
// todo: Collections
// todo:     Airport    // +++
// todo:     DataConext
// todo:     DataLoadException    // +++    ??? (ИСПОЛЬЗУЕТСЯ В ЭКСЕПШЕНЕ.)
// todo:     Passangers
// todo:     Tariffs    // +++
// todo: Data
// todo:     Discount
// todo:     Passanger    // +++
// todo:     Tariff    // +++
// todo:     Ticket    // +++
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Program.cs SelectItem
// todo: Program.cs Print
// todo: Program.cs RegTicket
// todo: Program.cs ShowPassangerPrice
// todo: Program.cs ShowPrice
// todo: Program.cs Main
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Input.cs ReadString    // (+++)
// todo: Input.cs ReadInt    // (+++) В "Input.cs" не используется.
// todo: Input.cs ReadDouble    // (---)
// todo: Input.cs ReadDate    // (---)
// todo: Input.cs ReadTariff    // (+++) В "Input.cs" не используется.
// todo: Input.cs ReadDiscount    // (---)
// todo: Input.cs ReadPassanger    // (+++) В "Input.cs" не используется.
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: namespace SimpleAirline
//
// todo: Нужны ли все библиотеки?
//
// todo: Почему сделана такая структура проекта?
// todo:   + "Data" "Данные" и "Collections" - "Коллекции".
// todo: Как и куда подключаются папки классов "Collections" и "Data".
// todo:   + "Input.cs".
// todo:   + Нахрена выделять "Input.cs" в отдельный класс.
// ??????????????????????????????????????????????????

//using SimpleAirline;
using System;
using System.Collections.Generic;
//using System.Globalization;
using System.Linq;

// TODO ВАЖНО Если возникнет вопрос: Почему у всех классов данных открытые (public, а не private) setтеры? 
// Ответ: при помощи такой коллекции, как ObservableCollection и интерфейса INotifyPropertyChanged можно отслеживать изменения в модели
// На уровне DataConext, и разрешать/запрещать изменения в зависимости от прав доступа

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    class Program
    {
        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        //
        // ??? Почему мы делаем так, а потом это идет в Меню?
        // ??? "private const string".
        private const string ADD_TARIFF = "Добавить тариф.";
        private const string TARIFF_LIST = "Список тарифов.";
        private const string REG_PASSANGER = "Зарегестрировать пассажира.";
        private const string LIST_PASSANGER = "Список пассажиров.";
        private const string REG_TICKET = "Зарегистрировать покупку билета.";
        private const string PRICE_OF_PGER = "Cтоимость купленных пассажиром билетов.";
        private const string PRICE_ALL = "Cтоимость всех купленных билетов.";
        private const string SAVE = "Сохранить данные на диск.";

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        //
        // ??? + входные параметры?
        static void Print<T>(IEnumerable<T> enumearble)
        {
            Console.WriteLine("---------------------------------------------------------------------------");

            //
            foreach (T obj in enumearble)
            {
                Console.WriteLine(obj);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // Выводит список на экран, индексируя, и возвращает введенный пользователем индекс

        // ??? Выводим список на экран, индексируя.    (ЧТО ЗА СПИСОК?)
        // !!! Возвращает введенный пользователем индекс.
        // ??? + входные параметры?
        // ??? Пояснить всю строку...
        static T SelectItem<T>(IEnumerable<T> items, string message)
        {
            //
            Console.WriteLine(message);

            // ??? Счетчик ... (ДЛЯ ЧЕГО?)
            int i = 0;

            //
            // + "... ?? ..."
            IList<T> enumerable = items as IList<T> ?? items.ToList();

            //
            foreach (T item in enumerable)
            {
                // ??? Сначала выводим индекс.
                Console.Write("{0,3}: ", ++i);
                // ??? Выводим сам элемент.
                Console.WriteLine(item);
            }

            //
            // !!!!! Обращаемся к классу "Input" к методу "ReadInt".
            // ??? "...  1, i) - 1];" ?
            return enumerable[Input.ReadInt("\nВведите пункт меню", 1, i) - 1];
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // todo: Collections
        // todo:     Airport

        // !!! Регистрация покупки билета.
        // ??? + входные параметры?
        private static void RegTicket(Airport airport)
        {
            try
            {
                //string passport = Input.ReadString("Введите номер паспорта");    // !!! Было.
                string passport = Input.ReadString("  Введите номер паспорта");
                //Console.WriteLine("  Введите номер паспорта.");
                
                // ??? Получаем инфо о пассажире.
                Passanger passanger = airport.Passangers.Get(passport);
                
                Console.WriteLine();
                
                if (passanger == null)
                {
                    Console.WriteLine("  Пассажир не найден.");
                    return;
                }

                // Вывод пассажира по запросу паспорта.
                Console.WriteLine("  " + passanger.ToString());

                Console.WriteLine();

                // получаем список тарифов
                IEnumerable<Tariff> tariffs = airport.Tariffs.GetAll();
                // Выбор тарифа из списка тарифов
                Tariff tariff = SelectItem(tariffs, "Выберите тариф:");    // +++
                // ввод номера места
                int seatNo = Input.ReadInt("Посадочное место");    // +++
                // Создаем билет
                Ticket ticket = new Ticket(passanger, tariff, seatNo);
                // добавляем билет в базу
                airport.Passangers.BuyTicket(passport, ticket);
                Console.WriteLine("\n  Регистрация прошла успешно:");    // +++
                Console.WriteLine(ticket);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не найден");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        /*
         * Выводит на консоль стоимость купленных билетов.
         * Исключения:
         * ApplicationException
         */
        private static void ShowPrice(Airport airport)
        {
            try
            {

                Console.WriteLine("\nCтоимость всех купленных билетов:");
                double sum = airport.AllTicketsPrice();
                if (double.IsInfinity(sum))
                {
                    // ???
                    Console.WriteLine("Результат вычесления слишком большой. Обратитесь в службу поддержки.");
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

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // todo: Collections
        // todo:     Airport

        // !!! Выводим на консоль стоимость купленных пассажиром билетов.
        // ??? Исключеня: ApplicationException.
        // ??? + входные параметры?

        private static void ShowPassangerPrice(Airport airport)
        {
            Console.WriteLine("\nCтоимость купленных пассажиром билетов:");
            try
            {
                // !!! Получаем номер паспорта.
                // !!!!! Обращаемся к классу "Input" к методу "ReadString".
                // ??? Где выводит это сообщение?
                string passport = Input.ReadString("  Введите номер паспорта пассажира");    // +++


                // ??? todo: Collections
                // ??? todo:     Passangers

                // ??? todo: Data
                // ??? todo:     Ticket

                //
                // !!! Получаем список билетов.
                IEnumerable<Ticket> tickets = airport.Passangers.GetTickets(passport);

                // ??? Выводим на экран список билетов.
                // !!!!! Вызываем метод "Print".
                Print(tickets);



                // Получаем сумму. выбросит KeyNotFoundException
                double sum = airport.PassangersTicketsPrice(passport);

                //
                // ??? В случае переполнения double получится бесконечно число.
                if (double.IsInfinity(sum))
                {
                    // ??? Это что значит?
                    Console.WriteLine("Результат вычесления слишком большой. Обратитесь в службу поддержки.");    // ---
                }
                else
                {
                    // Если все нормально, выводим результат
                    Console.WriteLine("  Итого с учетом скидки: " + sum + " р.");
                }
            }
            // стоимость билета со сскидкой меньше 0
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            // при попытке извлечь список билетов по несуществующему паспорту из DataContext.PassangersTickets
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не неайден");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////


        static void Main(string[] args)
        {

            Airport airport;
            try
            {
                // Создаем пустой каталог
                airport = new Airport();

                while (true)
                {
                    // Меню.
                    string select = SelectItem(new[]
                    {
                        ADD_TARIFF,
                        TARIFF_LIST,
                        REG_PASSANGER,
                        LIST_PASSANGER,
                        REG_TICKET,
                        PRICE_OF_PGER,
                        PRICE_ALL,
                        SAVE
                    }, "\nГлавное меню:");

                    //Console.Clear();

                    try
                    {
                        switch (select)
                        {
                            // 1. Добавить тариф.
                            case ADD_TARIFF:
                                {
                                    airport.Tariffs.Create(Input.ReadTariff());
                                }
                                break;
                            // 2. Список тарифов.
                            case TARIFF_LIST:
                                {
                                    Console.WriteLine("\nСписок тарифов:");
                                    Print(airport.Tariffs.GetAll());
                                }
                                break;
                            // 3. Зарегестрировать пассажира.
                            case REG_PASSANGER:
                                {
                                    Console.WriteLine("\nВведите данные о пасажире:");

                                    Passanger passanger = Input.ReadPassanger();
                                    airport.Passangers.Create(passanger);
                                }
                                break;
                            // 4. Список пассажиров.
                            case LIST_PASSANGER:
                                {
                                    Print(airport.Passangers.GetAll());
                                }
                                break;
                            // 5. Регистрировать покупку билета.
                            case REG_TICKET:
                                {
                                    Console.WriteLine("\nРегистрация билета:");

                                    RegTicket(airport);
                                }
                                break;
                            // 6. Cтоимость купленных пассажиром билетов.
                            case PRICE_OF_PGER:
                                {
                                    ShowPassangerPrice(airport);
                                }
                                break;
                            // 7. Cтоимость всех купленных билетов.
                            case PRICE_ALL:
                                {
                                    ShowPrice(airport);
                                }
                                break;
                            // 8. Сохранить данные на диск.
                            case SAVE:
                                {
                                    Console.WriteLine("\nДанные сохранены.");

                                    airport.Save();
                                }
                                break;
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    //Console.WriteLine("Нажмите любую клавишу.");
                    //Console.ReadKey(true);
                    //Console.Clear();
                }
            }

            // обрабатывает ошибки загрузки данных
            catch (DataLoadException e)
            {
                Console.WriteLine(e.Message, e);
            }
            // Обрабатывет выход из меню(приложения)
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            // обрабатывает непредвиденные ошибки
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

    }
}

