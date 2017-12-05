// ??????????????????????????????????????????????????
// todo: Почему сделана такая структура проекта?
// todo:   + "Data" "Данные" и "Collections" - "Коллекции".
// todo: Как и куда подключаются папки классов "Collections" и "Data".
// todo:   + "Input.cs".
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Collections            Коллекции
// todo:     Airport                Аэропорт
// todo:     DataConext             Контекст данных               ???
// todo:     DataLoadException      Исключение загрузки данных    ???
// todo:     Passangers             Пассажиры
// todo:     Tariffs                Тарифы
// todo: Data                   Данные
// todo:     Discount               Скидка
// todo:     Passanger              Пассажир
// todo:     Tariff                 Тариф
// todo:     Ticket                 Билет
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: Program.cs  Print
// todo: Program.cs  SelectItem
// todo: Program.cs  RegTicket
// todo: Program.cs  ShowPrice
// todo: Program.cs  ShowPassangerPrice
// todo: Program.cs  Main
// ??????????????????????????????????????????????????



// ??????????????????????????????????????????????????
// todo: namespace SimpleAirline
//
// todo: Где надо поставить 90% (0,9) при подсчете скидки?
// todo: Не сработал пассажир ВВ3333333. См. скриншоты 2, 3.
// ??????????????????????????????????????????????????

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;



// ??????????
// TODO ВАЖНО Если возникнет вопрос: Почему у всех классов данных открытые (public, а не private) setтеры? 
// Ответ: при помощи такой коллекции, как ObservableCollection и интерфейса INotifyPropertyChanged можно отслеживать изменения в модели
// На уровне DataConext, и разрешать/запрещать изменения в зависимости от прав доступа

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    // ??? Класс "Program". ...
    class Program
    {
        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // Константы
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
        // ??? + входные параметры?   Коллекция, которя будет выведена на экран.
        static void Print(IEnumerable enumearble)
        {
            Console.WriteLine("---------------------------------------------------------------------------");

            foreach (object obj in enumearble)
            {
                Console.WriteLine(obj);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////
        
        // ??? Выводим список на экран, индексируя.    (ЧТО ЗА СПИСОК И ИНДЕКСИРУЯ?)
        // ??? Возвращает введенный пользователем индекс.
        // ??? + входные параметры?
        // ??? Пояснить всю строку...
        // ??? Здесь происходит нумерация.
        // ??? Этим методом нумеруем:
        //       -Program.cs  Меню.
        //       -Program.cs  Тарифы
        //       -...
        static T SelectItem<T>(IEnumerable<T> items, string message)
        {
            //
            Console.WriteLine(message);

            // ??? Счетчик ... (ДЛЯ ЧЕГО?)  Индекс элемента
            int i = 0;

            // Приводим переданную коллекцию к типу IList
            IList<T> list = items as IList<T> ?? items.ToList();

            // Перечисляем  список.
            foreach (T item in list)
            {
                // +++ !!! Выводим индекс.
                Console.Write("{0,3}: ", ++i);
                // +++ !!! Выводим элемент.
                Console.WriteLine(item);
            }

            //
            // --- !!!!! Обращаемся к классу "Input" к методу "ReadInt".
            // ??? "...  1, i) - 1];" ?
            
            // Ползователь вводит индекс
            int index = Input.ReadInt("\nВведите пункт меню", 1, i);
            // Получаем элемент по индексу
            T selectItem = list[index - 1];
            // Возвращаем заданный (выбранный) элемент
            return selectItem;    // +++
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Регистрация покупки билета.
        // ??? + входные параметры?     Объект класса аэропорт
        private static void RegTicket(Airport airport)
        {
            try
            {
                // 
                string passport = Input.ReadString("  Введите номер паспорта");
                
                // ??? Получаем инфо о пассажире.
                // ??? Пояснить после знака равно.
                Passanger passanger = airport.Passangers.Get(passport);
                
                // +++ !!! Пустая строка.
                Console.WriteLine();

                //Пассажир не найден
                if (passanger == null)
                {
                    Console.WriteLine("    Пассажир не найден.");    // +++

                    //
                    return;
                }

                // Вывод пассажира по запросу паспорта.
                Console.WriteLine("  " + passanger.ToString());

                // +++ !!! Пустая строка.
                Console.WriteLine();

                // +++ !!! Получаем список тарифов.
                //
                IEnumerable<Tariff> tariffs = airport.Tariffs.GetAll();

                // +++ !!! Выбор тарифа из списка тарифов
                // +++ !!!!! Вызываем метод "SelectItem".
                // ??? Обращвемся к классу "Tariff". 
                Tariff tariff = SelectItem(tariffs, "Выберите тариф:");    // +++

                // !!! Ввод номера места.
                // ??? Обращвемся к классу "Input".
                int seatNo = Input.ReadInt("Посадочное место");    // +++

                // !!! Создаем билет.
                // ??? Пояснить строку.
                // ??? Обращвемся к классу "Ticket".
                Ticket ticket = new Ticket(passanger, tariff, seatNo);

                // !!! Добавляем билет в базу.
                airport.Passangers.BuyTicket(passport, ticket);
                Console.WriteLine("\n  Регистрация прошла успешно:");    // +++

                // ??? Выводим зарегистрированный билет.
                Console.WriteLine(ticket);
            }
            //
            // ??? В каких случаях выдает этот эксепшен, поскольку в пункте 5 выдает при вводе № паспорта сообщение "Пассажир не найден".
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не найден.....");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Выводим на консоль стоимость купленных билетов.
        // ??? Исключения: ApplicationException.
        // ??? + входные параметры?
        private static void ShowPrice(Airport airport)
        {
            try
            {
                // Выводим сообщение.
                Console.WriteLine("\nCтоимость всех купленных билетов:");

                //
                double sum = airport.AllTicketsPrice();

                //
                if (double.IsInfinity(sum))
                {
                    // ???
                    Console.WriteLine("Результат вычесления слишком большой. Обратитесь в службу поддержки.");
                }
                //
                else
                {
                    Console.WriteLine("Итого с учетом скидки: " + sum);
                }
            }
            //
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // +++ !!! Выводим на консоль стоимость купленных пассажиром билетов.
        // ??? Исключеня: ApplicationException.
        // ??? + входные параметры?
        private static void ShowPassangerPrice(Airport airport)
        {
            // +++ !!! Вывод сообщение.
            Console.WriteLine("\nCтоимость купленных пассажиром билетов:");

            try
            {
                // +++ !!! Получаем номер паспорта.
                // --- !!!!! Обращаемся к классу "Input" к методу "ReadString".
                string passport = Input.ReadString("  Введите номер паспорта пассажира");    // +++

                // !!! Получаем список билетов.
                // ??? Пояснить строку.
                IEnumerable<Ticket> tickets = airport.Passangers.GetTickets(passport);

                // ??? Выводим на экран список билетов.
                // +++ !!!!! Вызываем метод "Print".
                Print(tickets);

                // ??? Получаем сумму. выбросит KeyNotFoundException
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
                    // ?? Если все нормально, выводим результат.
                    Console.WriteLine("  Итого с учетом скидки: " + sum + " р.");
                }
            }
            // ??? Стоимость билета со сскидкой меньше 0.
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            // ??? При попытке извлечь список билетов по несуществующему паспорту из DataContext.PassangersTickets.
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Пасспорт не неайден.");
            }
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

        // !!! +++ Main.
        static void Main(string[] args)
        {
            //
            Airport airport;

            try
            {
                // +++ !!! Создаем пустой каталог.
                airport = new Airport();

                while (true)
                {
                    // +++ !!! Меню.
                    // +++ !!!!! Вызываем метод "SelectItem".
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
                    }, "Главное меню:");

                    try
                    {
                        switch (select)
                        {
                            // ??????????????????????????????????????????????????
                            // todo: Почему "break" вне фигурных скобок?
                            // ??????????????????????????????????????????????????

                            // 1. Добавить тариф.
                            case ADD_TARIFF:
                                {
                                    //
                                    // ??? Пояснитьь строку.
                                    // --- !!!!! Обращаемся к классу "Input" к методу "ReadTariff".
                                    airport.Tariffs.Create(Input.ReadTariff());
                                }
                                break;
                            // 2. Список тарифов.
                            case TARIFF_LIST:
                                {
                                    // +++ !!! Выводим сообщение.
                                    Console.WriteLine("\nСписок тарифов:");

                                    // ??? Выводим в консоли список тарифов.
                                    // +++ !!!!! Вызываем метод "Print".
                                    // ??? Пояснить строку.
                                    Print(airport.Tariffs.GetAll());
                                }
                                break;
                            // 3. Зарегестрировать пассажира.
                            case REG_PASSANGER:
                                {
                                    // +++ !!! Выводим сообщение.
                                    Console.WriteLine("\nВведите данные о пасажире:");

                                    //
                                    Passanger passanger = Input.ReadPassanger();

                                    //
                                    airport.Passangers.Create(passanger);
                                }
                                break;
                            // 4. Список пассажиров.
                            case LIST_PASSANGER:
                                {
                                    // +++ !!! Выводим сообщение.
                                    Console.WriteLine("\nСписок пассажиров:");

                                    //
                                    // +++ !!!!! Вызываем метод "Print".
                                    Print(airport.Passangers.GetAll());
                                }
                                break;
                            // 5. Регистрировать покупку билета.
                            case REG_TICKET:
                                {
                                    // +++ !!! Выводим сообщение.
                                    Console.WriteLine("\nРегистрация билета:");

                                    //
                                    // +++ !!!!! Вызываем метод "RegTicket".
                                    RegTicket(airport);
                                }
                                break;
                            // 6. Cтоимость купленных пассажиром билетов.
                            case PRICE_OF_PGER:
                                {
                                    //
                                    // !!!!! Вызываем метод "ShowPassangerPrice".
                                    ShowPassangerPrice(airport);
                                }
                                break;
                            // 7. Cтоимость всех купленных билетов.
                            case PRICE_ALL:
                                {
                                    //
                                    // +++ !!!!! Вызываем метод "ShowPrice".
                                    ShowPrice(airport);
                                }
                                break;
                            // 8. Сохранить данные на диск.
                            case SAVE:
                                {
                                    // +++ !!! Выводим сообщение.
                                    Console.WriteLine("\nДанные сохранены.");

                                    //
                                    airport.Save();
                                }
                                break;
                        }
                    }
                    //
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    //
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    // ??? Отличие здесь от того, что в конце "Program".

                    // +++ !!! Выводим сообщение.
                    Console.WriteLine("\nНажмите любую клавишу.");
                    // +++ !!! Задержка экрана.
                    Console.ReadKey(true);
                    // +++ !!! Очистка экрана.
                    Console.Clear();
                }
            }

            // ??? Обрабатывает ошибки загрузки данных.
            catch (DataLoadException e)
            {
                Console.WriteLine(e.Message, e);
            }
            // ??? Обрабатывет выход из меню (приложения).
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            // ??? Обрабатывает непредвиденные ошибки.
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // +++ !!! Выводим сообщение.
            Console.WriteLine("\nНажмите любую клавишу...");
            // +++ !!! Задержка экрана.
            Console.ReadKey(true);
        }

        // ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// ////////// //////////

    }
}

