using SimpleAirline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// TODO ВАЖНО Если возникнет вопрос: Почему у всех классов данных открытые (public, а не private) setтеры? 
// Ответ: при помощи такой коллекции, как ObservableCollection можно отслеживать изменения в модели
// на уровне DataLoader, и разрешать/запрещать изменения в зависимости от прав доступа
namespace SimpleAirline
{
    class Program
    {
        /*
         * Константы представляющие пункты главного меню
         */
        private const string ADD_TARIFF = "Добавить тариф";
        private const string TARIFF_LIST = "Список тарифов";
        private const string REG_PASSANGER = "Зарегестрировать пассажира";
        private const string REG_TICKET = "Регистрировать покупку билета";
        private const string PRICE_OF_PGER = "Cтоимость купленных пассажиром билетов";
        private const string PRICE_ALL = "Cтоимость всех купленных билетов";
        private const string SAVE = "Сохранить данные на диск";
        private const string LIST_PASSANGER = "Список пассажиров";


        /*
         * Обобщенный метод для вывода коллекции на экран
         * Тип элемента должен переопределять .ToString()
         * enumearble - коллекция
         * Т - неизвестный тип элемента коллекции
         */
        static void Print<T>(List<T> enumearble)
        {
            Console.WriteLine("---");
            foreach (T obj in enumearble)
            {
                Console.WriteLine(obj);
                Console.WriteLine("---");
            }
        }

        /*
         * Выводит список на экран, индексируя, и возвращает введенный пользователем индекс
         * items - колекция, message - сообщение
         * Исключения: ApplicationException, если пользователь отменил ввод
         */
        static T SelectItem<T>(List<T> items, string message)
        {
            // Выводим сообщение
            Console.WriteLine(message);
            // Счетчик
            int i = 0;
            foreach (T item in items)
            {
                // Сначала выводим индекс
                Console.Write("{0,3}: ", ++i);
                // Выводим сам элемент
                Console.WriteLine(item);
            }
            // Запрашиваем у пользователя индекс от 1 до i
            int index = Input.ReadInt("Введите индекс действия:", 1, i) - 1;
            // Возвращаем элемент
            return items[index];
        }

        /*
         * Регистрация оплаты билета существующему пассажиру
         * airport: источник данных
         * Исключения: ApplicationException, если пользователь отменил ввод
         */
        private static void RegTicket(Airport airport)
        {
            // Зпрос номера паспорта
            string passport = Input.ReadString("Введите номер пасспорта");
            // Получаем информацию о пассажире из БД
            Passanger passanger = airport.Passangers.Get(passport);
            // Если пассажир не был найден -  Конец алгоритма
            if (passanger == null)
            {
                Console.WriteLine("Пассажир не найден");
                return;
            }

            // Отображаем на экране пассажира
            Console.WriteLine(passanger);

            // Получаем список билетов из БД
            ICollection<Ticket> tickets = airport.Passangers.GetTickets(passport);
            // Список тарифов из БД
            List<Tariff> tariffs = airport.Tariffs.GetAll();
            // Выбираем тариф
            Tariff tariff = SelectItem(tariffs, "Выберите тариф");
            // Пользователь водит индекс тарифа
            int seatNo = Input.ReadInt("Посадочное место");
            // Создаем объект билет и добавляем его в базу
            tickets.Add(new Ticket(passanger, tariff, seatNo));
        }

        /*
         * Выводит на экран стоимость всех билетов с учетом скидки
         * airport: источник данных
         */
        private static void ShowPrice(Airport airport)
        {
            Console.WriteLine(PRICE_ALL);

            // получаем сумму
            double sum = airport.Sum();

            // если сумма равна бесконечности - предупреждаем пользователя. иначе выводим
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


        /*
         * Выводит на экран стоимость всех билетов пассажира с учетом скидки
         * airport: источник данных
         * Исключения: ApplicationException, если пользователь отменил ввод
         */
        private static void ShowPassangerPrice(Airport airport)
        {
            Console.WriteLine(PRICE_OF_PGER);
            try
            {
                // получаем номер паспорта
                string passport = Input.ReadString("Введите номер паспорта пассажира");
                // Выводим список билетов пассажира
                Print(airport.Passangers.GetTickets(passport));
                //
                // получаем сумму
                double sum = airport.Sum();

                // если сумма равна бесконечности - предупреждаем пользователя. иначе выводим
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

        /*
         * Точка входа
         */
        static void Main(string[] args)
        {
            try
            {
                // Создаем пустой каталог
                Airport airport = new Airport();

                while (true)
                {
                    // Меню
                    string select = SelectItem(new List<string>
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
            // Ошибка загрузки данных (выбрасывает конструктор Airport
            catch (DataLoaderException e)
            {
                Console.WriteLine(e.Message, e);
            }
            // Отмена ввода
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            // Неожиданная ошибка
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);
        }
    }
}

