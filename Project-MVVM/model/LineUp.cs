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
    public class LineUp
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private string _from;

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }
        private string _until;

        public string Until
        {
            get { return _until; }
            set { _until = value; }
        }
        private Stage _stages;

        public Stage Stages
        {
            get { return _stages; }
            set { _stages = value; }
        }
        private Band _bands;

        public Band Bands
        {
            get { return _bands; }
            set { _bands = value; }
        }

        private string _width;
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private string _margin;
        public string Margin
        {
            get { return _margin; }
            set { _margin = value; }
        }

        private DateTime _startDatum;

        public DateTime StartDatum
        {
            get { return _startDatum; }
            set { _startDatum = value; }
        }

        private DateTime _eindDatum;

        public DateTime EindDatum
        {
            get { return _eindDatum; }
            set { _eindDatum = value; }
        }

        private static ObservableCollection<Stage> _stagekesList;

        public static ObservableCollection<Stage> StagekesList
        {
            get { return _stagekesList; }
            set { _stagekesList = value; }
        }

        private static ObservableCollection<Band> _bandjesList;

        public static ObservableCollection<Band> BandjesList
        {
            get { return _bandjesList; }
            set { _bandjesList = value; }
        }
        //hoogte en breedte nodig om da lineup in de timeline goed te kunnen weergeven
        public static int vorigeWidth;
        public static int Height = 0;
        public static int aantal = 1;
        public static int aantal2 = 1;

        public static ObservableCollection<LineUp> sLineUp = new ObservableCollection<LineUp>();

        public static ObservableCollection<LineUp> lineup = new ObservableCollection<LineUp>();

        public static ObservableCollection<LineUp> festival = new ObservableCollection<LineUp>();

        //public static int aantal = 1;

        public static ObservableCollection<LineUp> GetDatums()
        {
            string sql = "SELECT * FROM Festivaldagen";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);




            while (reader.Read())
            {
                festival.Add(Create2(reader));
                
            }
            return festival;
        }

        private static LineUp Create2(IDataRecord record)
        {
            return new LineUp()
            {
                ID = (int)record["ID"],
                StartDatum = (DateTime)record["StartDate"],
                EindDatum = (DateTime)record["EndDate"]

            };
        }

        //alle data uit de database halen
        public static ObservableCollection<LineUp> GetLineUp()
        {
            string sql = "SELECT * FROM LineUpTable";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            StagekesList = Stage.GetStages();
            BandjesList = Band.GetBand();
            

            while (reader.Read())
            {
                lineup.Add(Create(reader));
                aantal++;
            }

            SortList();
            return lineup;
           
        }

        private static void SortList()
        {
            ObservableCollection<LineUp> temp1 = new ObservableCollection<LineUp>();

            foreach (LineUp l in lineup)
            {
                temp1.Add(l);
            }
            ObservableCollection<LineUp> temp2 = new ObservableCollection<LineUp>();
            int ruimte = 0;
            //sLineUp.Add(lineUp[0]);

            int id = 1;
            while (temp1.Count() > 0)
            {
                bool isToegevoegd = false;
                for (int i = 0; i < temp1.Count(); i++)
                {
                    if (temp1[i].Stages.ID == id)
                    {
                        temp1[i].Margin = GetMargin(temp1[i], GetFrom(temp1[i]), GetUntil(temp1[i]), temp1[i].Stages.ID - ruimte);
                        ruimte = temp1[i].Stages.ID;
                        temp2.Add(temp1[i]);
                        temp1.RemoveAt(i);
                        isToegevoegd = true;
                        aantal2++;
                    }
                }
                if (!isToegevoegd)
                {
                    id++;
                }
            }
            sLineUp = temp2;
        }
        //voor alle data een lineup item maken
        private static LineUp Create(IDataRecord record)
        {
            
            LineUp lineup = new LineUp();
                lineup.ID = (int)record["ID"];
                
                lineup.From = record["From"].ToString();
                lineup.Until = record["Until"].ToString();
                
                lineup.Date = (DateTime)record["Date"];
                lineup.Bands = new Band
                {
                    ID = (int)record["Band"],
                    Name = BandjesList[(int)record["Band"]-1].Name
                };
                lineup.Stages = new Stage
                {
                    ID = (int)record["Stage"],
                    Name = StagekesList[(int)record["Stage"]-1].Name
                };
                lineup.Width = GetWidth(lineup, GetFrom(lineup), GetUntil(lineup));
            return lineup;

            
        }
        //het einduur ophalen
        public static string[] GetUntil(LineUp lineUp)
        {
            String[] until = lineUp.Until.Split(new Char[] { ':' });
            return until;
        }
        //het beginuur ophalen
        public static string[] GetFrom(LineUp lineUp)
        {
            String[] from = lineUp.From.Split(new Char[] { ':' });
            return from;
        }
        //de breedte ophalen en berekenen
        public static string GetWidth(LineUp lineUp, string[] from, string[] until)
        {
            int uurUntil = Convert.ToInt32(until[0]);
            int uurFrom = Convert.ToInt32(from[0]);
            double minutenUntil = Convert.ToDouble(until[1]);
            double minutenFrom = Convert.ToDouble(from[1]);

            double uur = (uurUntil + minutenUntil / 60) - (uurFrom + minutenFrom / 60);

            int width = (int)(100 * uur * 2);

            return width.ToString();
        }
        //de tussenruimte ophalen en berekenen
        public static string GetMargin(LineUp lineUp, string[] from, string[] until, int ruimte)
        {
            int top = 0;
            //top = ((lineUp.Stage.ID - 1) * 60) - Height - ((aantal - 1) * 8); //((lineUp.Stage.ID - 1) * 60)
            top = ((ruimte - 1) * 60);// -((aantal2 - 1) * 8);
            //int height = ((lineUp.Stage.ID) * 60);
            //if (height > Height)
            //{
            //    Height = height+60;
            //}
            //Height = height;
            int uur = Convert.ToInt32(from[0]);
            double minuut = Convert.ToDouble(from[1]) / 60;
            double left = (uur * 200) + (minuut * 200);
            return Convert.ToInt32(left) + "," + top + ",0,0";
        }

        //een item updaten
        public static int UpdateLineUp(LineUp lnp)
        {
            
            
                DbTransaction trans = null;

                try
                {
                    trans = Database.BeginTransaction();
                    string sql = "UPDATE LineUpTable SET Date=@Date, [From]=@From, Until=@Until, Band=@Band, Stage=@Stage WHERE ID=@ID";

                    DbParameter par1 = Database.AddParameter("@ID", lnp.ID);
                    DbParameter par2 = Database.AddParameter("@From", lnp.From);
                    DbParameter par3 = Database.AddParameter("@Until", lnp.Until);
                    DbParameter par4 = Database.AddParameter("@Band", lnp.Bands.ID);
                    DbParameter par5 = Database.AddParameter("@Stage", lnp.Stages.ID);
                    DbParameter par6 = Database.AddParameter("@Date", lnp.Date);

                    int rowsaffected = 0;

                    rowsaffected += Database.ModifyData(trans, sql, par2, par3, par4, par5, par6, par1);
                    Console.WriteLine(rowsaffected + " row(s) are affected");
                    trans.Commit();
                    return rowsaffected;
                }
                catch (Exception)
                {
                    MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de line-up wilt opslaan.");
                    //trans.Rollback();
                    return 0;
                }
            }
        
        //een item toevoegen aan de lineup
        public static int InsertLineUp(LineUp lnp)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO LineUpTable (Date,[From],Until,Band,Stage)VALUES(@Date,@From,@Until,@Band,@Stage)";

                //DbParameter par1 = Database.AddParameter("@ID", lnp.ID);
                DbParameter par1 = Database.AddParameter("@Date", lnp.Date);
                DbParameter par2 = Database.AddParameter("@From", lnp.From);
                DbParameter par3 = Database.AddParameter("@Until", lnp.Until);
                DbParameter par4 = Database.AddParameter("@Band", lnp.Bands.ID);
                DbParameter par5 = Database.AddParameter("@Stage", lnp.Stages.ID);
                

                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de line-up wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }
        //een item verwijderen uit de lineup
        public static int DeleteLineUp(LineUp lnp)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM LineUpTable WHERE Date=@Date AND Band=@Band AND Stage=@Stage";

                DbParameter par1 = Database.AddParameter("@ID", lnp.ID);
                DbParameter par2 = Database.AddParameter("@Date", lnp.Date);
                DbParameter par3 = Database.AddParameter("@Band", lnp.Bands.ID);
                DbParameter par4 = Database.AddParameter("@Stage", lnp.Stages.ID);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par1, par3, par4);
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
            return ID + " " + From + " "+ Until;
        }
        public static void PrintLineUps(ObservableCollection<LineUp> LineUps)
        {

            foreach (LineUp lineup in LineUps)
            {
                Console.WriteLine(lineup.ToString());
            }
        }
        //json aanmaken voor de windows store app
        public static void JsonWegschrijven()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("\\Project-MVVM\\bin\\Debug\\", "\\Festival\\Data\\");
            StreamWriter sw = new StreamWriter(path + "Festivals.txt");

            sw.WriteLine("[ ");
            for (int i = 0; i < lineup.Count(); i++)
            {
                foreach (Band b in Band.band)
                {
                    if (b.Name == lineup[i].Bands.Name)
                    {
                        foreach (Genre g in Band.GenreList)
                        {
                            WegTeSchrijvenItem(sw, i, g, b);
                        }
                    }
                }
            }
            sw.WriteLine("]");
            sw.Close();


            
        }
        

        private static void WegTeSchrijvenItem(StreamWriter sw, int i, Genre g, Band b)
        {
            sw.WriteLine("{ ");
            sw.WriteLine("\"backgroundImage\" : \"images/" + g.Name + "/" + lineup[i].Bands.Name + ".jpg,");
            sw.WriteLine("\"discription\" : \"" + b.Description + "\",");
            sw.WriteLine("\"group\" : { \"backgroundImage\" : \"images/" + g.Name + "/" + g.Name + "_group_detail.jpg\",");
            sw.WriteLine("\"description\" : \"" + b.Description + "\",");
            sw.WriteLine("\"groupImage\" : \"" + b.Picture + "\",");
            sw.WriteLine("\"key\" : \"" + g.Name + "\",");
            sw.WriteLine("\"shortTitle\" : \"" + g.Name + "\",");
            sw.WriteLine("\"title\" : \"" + g.Name + "\"");
            sw.WriteLine("\"},");
            sw.WriteLine("\"genres\" : [\"" + g.Name + "\"],");
            sw.WriteLine("\"facebook\" : \"" + b.Facebook + "\",");
            sw.WriteLine("\"twitter\" : \"" + b.Twitter + "\",");
            sw.WriteLine("\"shortTitle\" : \"" + b.Name + "\",");
            sw.WriteLine("\"tileImage\" : \"" + b.Picture + "\",");
            sw.WriteLine("\"title\" : \"" + b.Name + "\"");
            

            if (lineup.Count() == 1 || lineup.Count() - 1 == i)
            {
                sw.WriteLine("}");
            }
            else
            {
                sw.WriteLine("},");
            }
        }

    }
}
