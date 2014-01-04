using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private int _aantalVorig;

        public int AantalVorig
        {
            get { return _aantalVorig; }
            set { _aantalVorig = value; }
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

        public static ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
        public static int aantal = 1;
        public static ObservableCollection<Ticket> GetTicket()
        {
            string sql = "SELECT * FROM Tickets";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Ticket> ticket = new ObservableCollection<Ticket>();
            TicketTypesList = TicketType.GetTicketTypes();

            while (reader.Read())
            {
                tickets.Add(Create(reader));
                aantal++;
            }
            return tickets;
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
                 AantalVorig = (int)record["AantalVorig"],
                TicketTypeID = new TicketType
                {
                    ID = (int)record["TicketTypeID"],
                    Name = TicketTypesList[(int)record["TicketTypeID"]-1].Name,
                    Price = TicketTypesList[(int)record["TicketTypeID"]-1].Price
                }
                

            };
        }

        public static int UpdateTicket(Ticket ticket)
        {
           
            DbTransaction trans = null;

            ObservableCollection<TicketType> ticketType = TicketType.ticketType;
            ObservableCollection<Ticket> ticketVorig = Ticket.GetTicket();
            int aantaltickets = ticketType[ticket.TicketTypeID.ID - 1].AvailableTickets;
            int aantalNu = ticket.Amount;
            int aantalVorig = ticket.AantalVorig;

            if (aantalNu > aantalVorig)
            {
                if (aantaltickets - (aantalNu - aantalVorig) >= 0)
                {
                    return UpdateTicket(trans, ticket, aantaltickets);
                }
                else
                {
                    
                    return 0;
                }
            }
            else if (aantalNu < aantalVorig)
            {
                if (aantalNu >= 0)
                {
                    return UpdateTicket(trans, ticket, aantaltickets);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                int idVorig = ticketVorig[ticket.ID -1].TicketTypeID.ID;
                if (idVorig != ticket.TicketTypeID.ID)
                {
                    if (aantaltickets >= aantalNu)
                    {
                        int rowsaffected = 0;
                        rowsaffected += UpdateTicketType(idVorig, -aantalVorig, ticketType[idVorig - 1].AvailableTickets);
                        rowsaffected += UpdateTicketType(ticket.TicketTypeID.ID, aantalVorig, aantaltickets);
                        try
                        {
                            trans = Database.BeginTransaction();

                            string sql = "UPDATE Tickets SET Ticketholder=@TicketHolder,TicketholderEmail=@TicketHolderEmail,TicketTypeID=@TicketTypeID,Amount=@Amount,AantalVorig=@AantalVorig WHERE ID=@ID";
                            DbParameter par1 = Database.AddParameter("@TicketHolder", ticket.Ticketholder);
                            DbParameter par2 = Database.AddParameter("@ID", ticket.ID);
                            DbParameter par3 = Database.AddParameter("@TicketHolderEmail", ticket.TicketholderEmail);
                            DbParameter par4 = Database.AddParameter("@TicketTypeID", ticket.TicketTypeID.ID);
                            DbParameter par5 = Database.AddParameter("@Amount", ticket.Amount);
                            DbParameter par6 = Database.AddParameter("@AantalVorig", ticket.Amount);

                            rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5,par6);

                            trans.Commit();
                            
                            return rowsaffected;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de reservatie wilt opslaan.");
                            //trans.Rollback();
                            return 0;
                        }
                    }
                    else
                    {
                        
                        return 0;
                    }
                }
                else
                {
                    
                    return 0;
                }
            }
        }

        public static int UpdateTicket(DbTransaction trans, Ticket ticket, int aantaltickets)
        {
            ObservableCollection<TicketType> types = new ObservableCollection<TicketType>();
            types = TicketType.ticketType;
            int vorig = tickets[ticket.ID].AantalVorig;

            UpdateTicketType(ticket.TicketTypeID.ID, ticket.Amount - vorig, aantaltickets);

            try
            {
                trans = Database.BeginTransaction();

                string sql = "UPDATE Tickets SET Ticketholder=@TicketHolder,TicketholderEmail=@TicketHolderEmail,TicketTypeID=@TicketTypeID,Amount=@Amount,AantalVorig=@AantalVorig WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("@TicketHolder", ticket.Ticketholder);
                DbParameter par2 = Database.AddParameter("@ID", ticket.ID);
                DbParameter par3 = Database.AddParameter("@TicketHolderEmail", ticket.TicketholderEmail);
                DbParameter par4 = Database.AddParameter("@TicketTypeID", ticket.TicketTypeID.ID);
                DbParameter par5 = Database.AddParameter("@Amount", ticket.Amount);
                DbParameter par6 = Database.AddParameter("@AantalVorig", ticket.Amount);

                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5, par6);

                trans.Commit();
                
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de reservatie wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }

        public static int UpdateTicketType(int id, int tickets, int vorig)
        {
            
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();

                string sql = "UPDATE TicketTypes SET AvailableTickets=@AvailableTickets WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("@ID", id);
                DbParameter par2 = Database.AddParameter("@AvailableTickets", vorig - tickets);

                int rowsaffected = 0;
                rowsaffected += Database.ModifyData(trans, sql, par1, par2);

                trans.Commit();
                
                return rowsaffected;
            }
            catch (Exception)
            {
                
                trans.Rollback();
                return 0;
            }
        }

        public static int InsertTicket(Ticket ticket)
        {
            
            DbTransaction trans = null;

            int aantaltickets = TicketType.ticketType[ticket.TicketTypeID.ID - 1].AvailableTickets;
            ObservableCollection<TicketType> types = TicketType.ticketType;
            int vorig = types[ticket.TicketTypeID.ID - 1].AvailableTickets;

            if (vorig - ticket.Amount >= 0)
            {
                UpdateTicketType(ticket.TicketTypeID.ID, ticket.Amount, vorig);

                try
                {
                    trans = Database.BeginTransaction();

                    string sql = "INSERT INTO Tickets (Ticketholder,TicketholderEmail,TicketTypeID,Amount,AantalVorig)VALUES(@TicketHolder,@TicketHolderEmail,@TicketTypeID,@Amount,@AantalVorig)";
                    DbParameter par1 = Database.AddParameter("@TicketHolder", ticket.Ticketholder);
                    //DbParameter par2 = Database.AddParameter("@ID", aantal);
                    DbParameter par3 = Database.AddParameter("@TicketHolderEmail", ticket.TicketholderEmail);
                    DbParameter par4 = Database.AddParameter("@TicketTypeID", ticket.TicketTypeID.ID);
                    DbParameter par5 = Database.AddParameter("@Amount", ticket.Amount);
                    DbParameter par6 = Database.AddParameter("@AantalVorig", ticket.Amount);

                    int rowsaffected = 0;
                    rowsaffected += Database.ModifyData(trans, sql, par1, par3, par4, par5,par6);

                    trans.Commit();
                    
                    return rowsaffected;
                }
                catch (Exception)
                {
                    MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de reservatie wilt opslaan.");
                    //trans.Rollback();
                    return 0;
                }
            }
            else
            {
                
                return 0;
            }
        }

        public static int DeleteTicket(Ticket tkt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Tickets WHERE Ticketholder=@Ticketholder";

                DbParameter par1 = Database.AddParameter("@ID", tkt.ID);
                DbParameter par2 = Database.AddParameter("@Ticketholder", tkt.Ticketholder);


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


        public static ObservableCollection<Ticket> GetTicketsByString(string search)
        {
            ObservableCollection<Ticket> lstGevondenTickets = new ObservableCollection<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                if (ticket.Ticketholder.ToUpper().Contains(search.ToUpper()) || ticket.TicketholderEmail.ToUpper().Contains(search.ToUpper()) )
                {
                    lstGevondenTickets.Add(ticket);
                }
            }
            return lstGevondenTickets;
        }


        public override string ToString()
        {
            return ID + " " + Ticketholder + " " + TicketholderEmail;
        }
        public static void PrintTicketjes(ObservableCollection<Ticket> Tickets)
        {

            foreach (Ticket ticket in Tickets)
            {
                Console.WriteLine(ticket.ToString());
            }
        }

        public static void PrintTickets(Ticket tkt)
        {
            foreach (Ticket ssc in tickets)
            {
                string filename = "Ticket" + "_" + ssc.Ticketholder +".docx";
                File.Copy("template.docx", filename, true);
                WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true);
                IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
                foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                {
                    bookmarks[bms.Name] = bms;
                }

                double prijs = ssc.TicketTypeID.Price * ssc.Amount;

                bookmarks["Date"].Parent.InsertAfter<Run>(new Run(new Text(DateTime.Today.ToString())), bookmarks["Date"]);
                bookmarks["Name"].Parent.InsertAfter<Run>(new Run(new Text(ssc.Ticketholder)), bookmarks["Name"]);
                bookmarks["Type"].Parent.InsertAfter<Run>(new Run(new Text(ssc.TicketTypeID.Name)), bookmarks["Type"]);
                bookmarks["Amount"].Parent.InsertAfter<Run>(new Run(new Text(ssc.Amount.ToString())), bookmarks["Amount"]);
                bookmarks["Price"].Parent.InsertAfter<Run>(new Run(new Text(ssc.TicketTypeID.Price.ToString())), bookmarks["Price"]);
                bookmarks["Total"].Parent.InsertAfter<Run>(new Run(new Text(prijs.ToString())), bookmarks["Total"]);
                Run run = new Run(new Text("111000111"));
                RunProperties prop = new RunProperties();
                RunFonts font = new RunFonts() { Ascii = "Free 3 of 9 Extended", HighAnsi = "Free 3 of 9 Extended" };
                FontSize size = new FontSize() { Val = "96" };
                prop.Append(font);
                prop.Append(size);
                run.PrependChild<RunProperties>(prop);
                bookmarks["Barcode"].Parent.InsertAfter<Run>(run, bookmarks["Barcode"]);
                newdoc.Close();
            }
        }

    }
}
