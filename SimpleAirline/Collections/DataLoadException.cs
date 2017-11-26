using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    /*
     * Представляет ошибку загрузки данных.
     */
    public class DataLoadException : Exception
    {
        // Хранит имя сущности, которую не удалось загрузить.
        public string EntityName { get; private set; }

        // Конструктор, инициализирует EntityName и базовое свойство Message через конструктор базового класса
        public DataLoadException(string entityName, Exception exception) 
            : base("Ошибка во время загрузки даных: " + exception.Message + "\nИмя сущности: " + entityName, exception)
        {
            EntityName = entityName;
        }
    }
}
