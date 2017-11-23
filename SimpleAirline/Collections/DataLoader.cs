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
        private const string PassangersTxt = "Passangers.txt";
        public ICollection<Tariff> Tariffs { get; private set; }
        public ICollection<Passanger> Passangers { get; private set; }

        public DataLoader()
        {
            Tariffs = new HashSet<Tariff>();
            Passangers = new HashSet<Passanger>();
            try
            {
                IEnumerable<string> lines = File.ReadLines(TariffsTxt);
                foreach (string line in lines)
                {
                    Tariffs.Add((Tariff)line);
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException("Tariff", e);
            }

            try
            {
                IEnumerable<string> lines2 = File.ReadLines(PassangersTxt);
                foreach (string line in lines2)
                {
                    Passanger passanger = (Passanger)line;
                    Passangers.Add(passanger);

                    if (!File.Exists(passanger.Passport + ".txt"))
                    {
                        continue;
                    }
                    List<Ticket> tickets = new List<Ticket>();
                    IEnumerable<string> linest = File.ReadLines(passanger.Passport + ".txt");
                    foreach (string linet in linest)
                    {
                        string[] linetArr = linet.Split(';');
                        tickets.Add(new Ticket(Tariffs.First(tariff => tariff.Id == linetArr[0]), passanger, int.Parse(linetArr[2]), double.Parse(linetArr[3])));
                    }
                    passanger.Tickets = tickets;
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException("Passanger", e);
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
            foreach (Passanger pass in Passangers)
            {
                passangersSw.WriteLine((string)pass);
                StreamWriter ticketsSw = new StreamWriter(pass.Passport + ".txt");
                foreach (Ticket ticket in pass.Tickets)
                {
                    ticketsSw.WriteLine((string)ticket);
                }

                ticketsSw.Close();
            }

            passangersSw.Close();
        }
    }
}
