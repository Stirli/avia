using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleAirline
{
    public class DataLoader
    {
        private const string TariffsTxt = "Tariffs.txt";
        private const string PassangersTicketsTxt = "PassangersTickets.txt";
        private const string PassangersTxt = "Passangers.txt";
        public List<Tariff> Tariffs { get; private set; }
        public Dictionary<string, List<Ticket>> PassangersTickets { get; set; }
        public Dictionary<string, Passanger> Passangers { get; set; }

        public DataLoader()
        {
            Tariffs = new List<Tariff>();
            PassangersTickets = new Dictionary<string, List<Ticket>>();
            Passangers = new Dictionary<string, Passanger>();
            string entityName = "Tariffs";
            try
            {
                if (File.Exists(TariffsTxt))
                {
                    IEnumerable<string> lines = File.ReadLines(TariffsTxt);
                    foreach (string line in lines)
                    {
                        Tariffs.Add((Tariff)line);
                    }
                }

                entityName = "Passangers";
                if (File.Exists(PassangersTxt))
                {
                    IEnumerable<string> lines = File.ReadLines(PassangersTxt);
                    foreach (string line in lines)
                    {
                        Passanger passanger = (Passanger)line;
                        Passangers.Add(passanger.Passport, passanger);
                    }
                }

                entityName = "PassangersTickets";
                if (File.Exists(PassangersTicketsTxt))
                {
                    IEnumerable<string> lines2 = File.ReadLines(PassangersTicketsTxt);
                    foreach (string line in lines2)
                    {
                        string[] arr = line.Split(';');
                        Passanger passanger =
                            new Passanger() { Passport = arr[0], Name = arr[1], Discount = (Discount)arr[2] };
                        List<Ticket> ticketList;
                        if (PassangersTickets.ContainsKey(passanger.Passport))
                        {
                            ticketList = PassangersTickets[passanger.Passport];
                        }
                        else
                        {
                            PassangersTickets.Add(passanger.Passport, ticketList = new List<Ticket>());
                        }

                        Tariff tariff = new Tariff(arr[3], arr[4], DateTime.Parse(arr[5]), double.Parse(arr[7]));
                        int seatNo = int.Parse(arr[6]);
                        ticketList.Add(new Ticket(passanger, tariff, seatNo));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException(entityName, e);
            }
        }

        public void Save()
        {
            StreamWriter tariffsSw = new StreamWriter(TariffsTxt);
            foreach (Tariff tariff in Tariffs)
            {
                tariffsSw.WriteLine((string)tariff);
            }

            tariffsSw.Close();
            StreamWriter passangersSw = new StreamWriter(PassangersTxt);
            foreach (Passanger passanger in Passangers.Values)
            {
                passangersSw.WriteLine((string)passanger);
            }
            passangersSw.Close();
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
