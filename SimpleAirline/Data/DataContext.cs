using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAirline.Data
{
    class DataContext : DbContext
    {
        public DataContext()
            : base("DbConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
    }
}
