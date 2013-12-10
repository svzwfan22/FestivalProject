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
    public class Ticket
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _ticketholder;

        public string Ticketholder
        {
            get { return _ticketholder; }
            set { _ticketholder = value; }
        }
        private string _ticketholderEmail;

        public string TicketholderEmail
        {
            get { return _ticketholderEmail; }
            set { _ticketholderEmail = value; }
        }
        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }
        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public static ObservableCollection<Ticket> GetTicket()
        {
            string sql = "SELECT * FROM Ticket";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            ObservableCollection<Ticket> ticket = new ObservableCollection<Ticket>();

            while (reader.Read())
            {
                ticket.Add(Create(reader));
            }
            return ticket;
        }

        private static Ticket Create(IDataRecord record)
        {
            return new Ticket()
            {
                ID = record["ID"].ToString(),
                Ticketholder = record["Ticketholder"].ToString(),
                TicketholderEmail = record["TicketholderEmail"].ToString(),
                //TicketType = record["TicketType"].ToString(),
                 //Amount = record["Amount"].ToString,
                TicketType = new TicketType
                {
                    Name = record["TicketType"].ToString()
                },
                

            };
        }

        public override string ToString()
        {
            return ID + " " + Ticketholder + " " + TicketholderEmail;
        }
        public static void PrintTickets(ObservableCollection<Ticket> Tickets)
        {

            foreach (Ticket ticket in Tickets)
            {
                Console.WriteLine(ticket.ToString());
            }
        }

    }
}
