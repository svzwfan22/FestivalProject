using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_MVVM.model
{
    public class TicketType
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private Double _price;

        public Double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private int _availableTickets;

        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }
        public override string ToString()
        {
            return ID + " " + Name;
        }

        public static ObservableCollection<TicketType> GetTicket()
        {
            string sql = "SELECT * FROM TicketType";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            ObservableCollection<Ticket> ticket = new ObservableCollection<Ticket>();

            while (reader.Read())
            {
                ticket.Add(Create(reader));
            }
            return ticket;
        }

        private static TicketType Create(IDataRecord record)
        {
            return new TicketType()
            {
                ID = record["ID"].ToString(),
                Name = record["Name"].ToString(),
                //Price = record["Price"].ToString(),

                //AvailableTickets = record["AvailableTickets"].ToString()


            };
        }

        public override string ToString()
        {
            return Name ;
        }
        public static void PrintTickets(ObservableCollection<TicketType> Tickets)
        {

            foreach (TicketType ticket in Tickets)
            {
                Console.WriteLine(ticket.ToString());
            }
        }
    }
}
