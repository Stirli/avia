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

            context.Carriers.Add(b2);
            context.Carriers.Add(afl);

            context.SaveChanges();

            var minsk = new Place { Name = "Минск" };
            var moscow = new Place { Name = "Москва" };
            var london = new Place { Name = "Лондон" };

            context.Places.Add(minsk);
            context.Places.Add(moscow);
            context.Places.Add(london);

            context.SaveChanges();

            // создаем два объекта Tariff
            Tariff tariff1 = new Tariff { SeatClass = 'Y', PriceUsd = 149, Carrier = b2 };
            Tariff tariff2 = new Tariff { SeatClass = 'P', PriceUsd = 799, Carrier = afl };
            context.Tariffs.AddRange(new List<Tariff>() { tariff1, tariff2 });

            context.SaveChanges();
            //base.Seed(context);
        }
    }
}
