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
        private int _ID;

        public int ID
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
        private TicketType _ticketTypeID;

        public TicketType TicketTypeID
        {
            get { return _ticketTypeID; }
            set { _ticketTypeID = value; }
        }


        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private static ObservableCollection<TicketType> _ticketTypesList;

        public static ObservableCollection<TicketType> TicketTypesList
        {
            get { return _ticketTypesList; }
            set { _ticketTypesList = value; }
        }

        public static ObservableCollection<Ticket> ticket = new ObservableCollection<Ticket>();
        public static int aantal = 1;
        public static ObservableCollection<Ticket> GetTicket()
        {
            string sql = "SELECT * FROM Ticketten";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Ticket> ticket = new ObservableCollection<Ticket>();
            TicketTypesList = TicketType.GetTicketTypes();

            while (reader.Read())
            {
                ticket.Add(Create(reader));
                aantal++;
            }
            return ticket;
        }

        private static Ticket Create(IDataRecord record)
        {
            return new Ticket()
            {
                ID = (int)record["ID"],
                Ticketholder = record["Ticketholder"].ToString(),
                TicketholderEmail = record["TicketholderEmail"].ToString(),
                //TicketType = record["TicketType"].ToString(),
                 Amount = (int)record["Amount"],
                TicketTypeID = new TicketType
                {
                    ID = (int)record["TicketTypeID"],
                    Name = TicketTypesList[(int)record["TicketTypeID"]-1].Name
                }
                

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
