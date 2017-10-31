using SimpleAirline.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAirline.Utils;
using static System.Console;
using static SimpleAirline.Utils.ConsoleUtils;

namespace SimpleAirline
{
    class Program
    {

        static void AddTarriff(DataContext db)
        {
            WriteLine("Выбор перевозчика");
            var carrier = SelectDbRow(db.Carriers);
            WriteLine("Выбор места отправки");
            var fromPlace = SelectDbRow(db.Places);
            WriteLine("Выбор пункта назначения");
            var to = SelectDbRow(db.Places);
            var seatClass = ReadLine("Введите класс пассажира ");
            var priceUsd = ReadDouble("Введите стоимость тарифа USD ", 0.01);
            var tariff = new Tariff { FromPlace = fromPlace, ToPlace = to, SeatClass = seatClass, PriceUsd = priceUsd, Carrier = carrier };
            db.Tariffs.Add(tariff);
            db.SaveChanges();
        }

        static void BuyTicket(DataContext db)
        {
            WriteLine("Выберите тариф:");
            SelectDbRow(db.Tariffs);
        }

        static int ShowMainMenu()
        {
            ShowAsMenu("Главное меню", "1. Список тарифов", "2. Добавить тариф", "3. Купить билет");
            return (int)ReadDouble("Введите номер пункта меню ", 1, 1);
        }

        static void Main(string[] args)
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    while (true)
                    {
                        // получаем объекты из бд и выводим на консоль
                        switch (ShowMainMenu())
                        {
                            case 1:
                                ShowTariffList(context);
                                break;
                            case 2:
                                AddTarriff(context);
                                break;
                            case 3:
                                BuyTicket(context);
                                break;
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (ApplicationException e)
            {
                WriteLine(e.Message);
            }
            catch (Exception e)
            {
                WriteLine("!!! Критическая ошибка !!!!");
                WriteLine(e);
            }
            ReadKey(true);
        }

        private static void ShowTariffList(DataContext context)
        {
            WriteLine("Список тарифов:");
            ShowAsMenu(context.Tariffs.ToList().Select(t=>t.ToString()).ToArray());
            //context.Tariffs.ToList().Print();
        }
    }
}

