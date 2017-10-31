using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline.Data
{
    class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var b2 = new Carrier { Name = "Belavia - Belarusian Airlines", ShortName = "B2" };
            var afl = new Carrier { Name = "Aeroflot Russian Airlines", ShortName = "AFL" };

            context.Carriers.Local.Add(b2);
            context.Carriers.Local.Add(afl);
            
            var minsk = new Place { Name = "Минск" };
            var moscow = new Place { Name = "Москва" };
            var london = new Place { Name = "Лондон" };

            context.Places.Local.Add(minsk);
            context.Places.Local.Add(moscow);
            context.Places.Local.Add(london);
            
            // создаем два объекта Tariff
            Tariff tariff1 = new Tariff {FromPlace = minsk, ToPlace = moscow, SeatClass = "Y", PriceUsd = 149, Carrier = b2 };
            Tariff tariff2 = new Tariff { FromPlace = moscow, ToPlace = london, SeatClass = "P", PriceUsd = 799, Carrier = afl };
            context.Tariffs.Local.Add(tariff1);
            context.Tariffs.Local.Add(tariff2);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
