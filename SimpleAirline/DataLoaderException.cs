using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    public class DataLoaderException : Exception
    {
        public string EntityName { get; private set; }

        public DataLoaderException(string entityName, Exception exception) : base("Error while loading data: " + exception.Message + "\nEntity name: " + entityName, exception)
        {
            EntityName = entityName;
        }
    }
}
