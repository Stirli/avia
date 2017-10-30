using SimpleAirline.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline
{
    class Program
    {
        static void Main(string[] args)
        {

            using (DataContext context = new DataContext())
            {
                // получаем объекты из бд и выводим на консоль
                Console.WriteLine("Список объектов:");
                foreach (Carrier carrier in context.Carriers)
                {
                    Console.WriteLine(carrier);
                    foreach (var tariff in carrier.Tariffs)
                    {
                        Console.WriteLine(tariff);
                    }
                }
            }
            Console.ReadKey(true);
        }
    }
}

