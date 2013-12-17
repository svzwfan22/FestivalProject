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
       

        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            string sql = "SELECT * FROM TicketTypes";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            ObservableCollection<TicketType> ticketType = new ObservableCollection<TicketType>();

            while (reader.Read())
            {
                ticketType.Add(Create(reader));
            }
            return ticketType;
            
        }

        private static TicketType Create(IDataRecord record)
        {
            return new TicketType()
            {
                ID = record["ID"].ToString(),
                Name = record["Name"].ToString(),
                Price = (Double)record["Price"],
                AvailableTickets = (int)record["AvailableTickets"]
            };
        }

        public override string ToString()
        {
            return Name + " " +  "Price: " + Price + " "+" Available Tickets: " + AvailableTickets;
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
