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
        private int _ID;

        public int ID
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

        public static ObservableCollection<TicketType> ticketType = new ObservableCollection<TicketType>();
        public static int aantalType = 1;
        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            string sql = "SELECT * FROM TicketTypes";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<TicketType> ticketType = new ObservableCollection<TicketType>();

            while (reader.Read())
            {
                ticketType.Add(Create(reader));
                aantalType++;
            }
            return ticketType;
            
        }

        private static TicketType Create(IDataRecord record)
        {
            return new TicketType()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString(),
                Price = (Double)record["Price"],
                AvailableTickets = (int)record["AvailableTickets"]
            };
        }

        public static int UpdateTicketType(TicketType tkt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE TicketTypes SET Name=@name,Price=@Price,AvailableTickets=@AvailableTickets WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", tkt.ID);
                DbParameter par2 = Database.AddParameter("@Name", tkt.Name);
                DbParameter par3 = Database.AddParameter("@Price", tkt.Price);
                DbParameter par4 = Database.AddParameter("@AvailableTickets", tkt.AvailableTickets);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par1, par3,par4);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                trans.Rollback();
                return 0;
            }
        }

        public static int InsertTicketType(TicketType tkt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO TicketTypes (Name,Price,AvailableTickets) VALUES (@name,@Price,@AvailableTickets)";

                //DbParameter par1 = Database.AddParameter("@ID", "4");
                DbParameter par2 = Database.AddParameter("@Name", tkt.Name);
                DbParameter par3 = Database.AddParameter("@Price", tkt.Price);
                DbParameter par4 = Database.AddParameter("@AvailableTickets", tkt.AvailableTickets);

                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2,par3,par4);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                trans.Rollback();
                return 0;
            }
        }

        public static int DeleteTicketType(TicketType tkt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM TicketTypes WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", tkt.ID);
                DbParameter par2 = Database.AddParameter("@Name", tkt.Name);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par1);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                trans.Rollback();
                return 0;
            }
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
