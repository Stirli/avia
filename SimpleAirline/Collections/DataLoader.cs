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
        private const string TicketsTxt = "Tickets.txt";
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

                    IEnumerable<string> linest = File.ReadLines(TicketsTxt);
                    foreach (string linet  in linest)
                    {
                       string[] linetArr = linet.Split(';');
                        passanger.Tickets.Add(new Ticket(Tariffs.First(tariff=>tariff.Id== linetArr[0]));
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw new DataLoaderException("Passanger", e);
            }
        }

        public void Save()
        {
            StreamWriter swta = new StreamWriter(TariffsTxt);
            foreach (Tariff tariff in Tariffs)
            {
                swta.WriteLine((string)tariff);
            }
            swta.Close();

            StreamWriter swp = new StreamWriter(PassangersTxt);
            StreamWriter swti = new StreamWriter(TicketsTxt);
            foreach (Passanger pass in Passangers)
            {
                swp.WriteLine((string)pass);
                foreach (Ticket ticket in pass.Tickets)
                {
                    swti.WriteLine(ticket);
                }
            }
            swp.Close();
            swti.Close();
        }
    }
}
