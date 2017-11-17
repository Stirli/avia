using System.Collections.Generic;

namespace SimpleAirline.Data
{
    public class Passanger
    {

        public string Passport
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public List<Ticket> Tickets
        {
            get;
            set;
        }

        public int Property
        {
            get;
            set;
        }
    }
}