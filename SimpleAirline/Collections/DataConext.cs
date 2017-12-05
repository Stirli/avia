using System;
using System.Collections.Generic;
using System.IO;

// !!! "Airline" - "Авиакомпания".
namespace SimpleAirline
{
    /*
     * Нижний слой
     * Загружает данные в память и сохраняет в файл
     */

    // ??? (ПОЯСНИТЬ.) Класс "DataConext" ("???"). ??? Нижний слой. Загружает данные в память и сохраняет в файл.
    public class DataConext
    {
        // определяет путь к файлу с тарифами
        private const string TariffsTxt = "Tariffs.txt";
        // определяет путь к файлу с билетам
        private const string PassangersTicketsTxt = "PassangersTickets.txt";
        // определяет путь к файлу с пассажирами
        private const string PassangersTxt = "Passangers.txt";

        // Коллекция с тарифами
        public ICollection<Tariff> Tariffs { get; private set; }

        // Словарь с билетами. на ключ-(номер паспорта) значение - список билетов пассажира
        public IDictionary<string, List<Ticket>> PassangersTickets { get; set; }

        // Словарь пассажиров. ключ - номер паспорта. значение - информация о пассажире
        public IDictionary<string, Passanger> Passangers { get; set; }

        // Конструктор. Заполняет списки из файлов.
        // Выбрасывает DataLoadException при ошибках
        public DataConext()
        {
            // Инициализируем коллекции
            Tariffs = new HashSet<Tariff>();
            PassangersTickets = new Dictionary<string, List<Ticket>>();
            Passangers = new Dictionary<string, Passanger>();

            // entityName - имя сущности, которую предстоит загрузить
            // Оно вручную меняется, по ходу выполнения загрузки
            string entityName = "Tariffs";

            // Если в этом блоке произойдет ошибка, catch ее перехватит, и бросит свою (DataLoadException)
            // DataLoadException - объеденяет все возможные ошибки в одну,
            // так же сообщая. на каком примерно этапе произошел сбой
            try
            {
                // Загружает из файла тарифы
                LoadTariffs();

                entityName = "Passangers";
                // Загружает из файла пассажиров
                LoadPassangers();

                entityName = "PassangersTickets";
                // Загружает из файла билеты
                LoadTickets();
            }
            catch (Exception e)
            {
                throw new DataLoadException(entityName, e);
            }
        }

        /*
         * Заполняет словарь PassangersTickets
         * IndexOutOfRangeException
         * FormatException
         * KeyNotFoundException
         * ArgumentNullException
         */
        private void LoadTickets()
        {
            // Если файл не существует - данных нет, ничего не делаем
            if (!File.Exists(PassangersTicketsTxt)) return;
            // Читаем из него данные по строкам
            IEnumerable<string> lines = File.ReadLines(PassangersTicketsTxt);
            foreach (string line in lines)
            {
                // Делим строку на подстроки
                string[] arr = line.Split(';');
                // Получаем скидку при помощи реализованного оператора явного преобразования
                Discount discount = (Discount)arr[2];
                // Создаем пассажира
                // Объявляем список билетов
                List<Ticket> ticketList;
                // Если в PassangersTickets уже был добавлен текущий паспорт, получаем и используем этот список
                string passport = arr[0];
                if (PassangersTickets.ContainsKey(passport))
                {
                    // получаем список билетов по ключу (номеру паспорта)
                    ticketList = PassangersTickets[passport];
                }
                else
                {
                    // Иначе создаем новый
                    PassangersTickets.Add(passport, ticketList = new List<Ticket>());
                }

                // Создаем тариф на основе
                int seatNo = int.Parse(arr[6]);
                // Создаем билет.
                Ticket ticket = new Ticket(arr[0], arr[1], discount, arr[3], arr[4], DateTime.Parse(arr[5]), seatNo, double.Parse(arr[7]));
                // добавляем билет в список белетов пассаж
                ticketList.Add(ticket);
            }
        }

        /*
         * Загрузка пассажиров
         */
        private void LoadPassangers()
        {
            // Если файл существует,
            if (File.Exists(PassangersTxt))
            {
                // читаем из него данные по строкам
                IEnumerable<string> lines = File.ReadLines(PassangersTxt);
                foreach (string line in lines)
                {
                    // парсим строку
                    Passanger passanger = (Passanger)line;
                    // добавляем информацию о пассажире
                    Passangers.Add(passanger.Passport, passanger);
                    PassangersTickets.Add(passanger.Passport, new List<Ticket>());
                }
            }
        }

        /*
         * Загрузка тарифов
         */
        private void LoadTariffs()
        {
            if (File.Exists(TariffsTxt))
            {
                IEnumerable<string> lines = File.ReadLines(TariffsTxt);
                foreach (string line in lines)
                {
                    Tariffs.Add((Tariff)line);
                }
            }
        }

        /*
         * Сохранение всех данных в файл
         */
        public void Save()
        {
            // Открываем файл (создаем. если не существует) с тарифами для записи.
            StreamWriter tariffsSw = new StreamWriter(TariffsTxt);
            foreach (Tariff tariff in Tariffs)
            {
                // приводит объект Tariff к строке
                tariffsSw.WriteLine((string)tariff);
            }

            // Закрыть файл.
            tariffsSw.Close();

            // Открываем файл с информацией о пассажирах
            StreamWriter passangersSw = new StreamWriter(PassangersTxt);
            foreach (Passanger passanger in Passangers.Values)
            {
                passangersSw.WriteLine((string)passanger);
            }

            passangersSw.Close();

            // Билеты.
            StreamWriter passangersTicketsSw = new StreamWriter(PassangersTicketsTxt);
            foreach (List<Ticket> tickets in PassangersTickets.Values)
            {
                foreach (Ticket ticket in tickets)
                {
                    passangersTicketsSw.WriteLine((string)ticket);
                }
            }

            passangersTicketsSw.Close();
        }
    }
}
