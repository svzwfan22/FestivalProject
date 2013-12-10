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
    public class LineUp
    {
        private string _ID;

        public string ID
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
        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }
        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }

        public static ObservableCollection<LineUp> GetLineUp()
        {
            string sql = "SELECT * FROM LineUp";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            ObservableCollection<LineUp> lineup = new ObservableCollection<LineUp>();

            while (reader.Read())
            {
                lineup.Add(Create(reader));
            }
            return lineup;
        }

        private static LineUp Create(IDataRecord record)
        {
            return new LineUp()
            {
                ID = record["ID"].ToString(),
                //Band = record["Band"].ToString(),
                From = record["From"].ToString(),
                Until = record["Until"].ToString(),
                // Stage = record["Stage"].ToString(),
                //Date = record["Date"].ToString()
                Band = new Band
                {
                    Name = record["Band"].ToString()
                },
                Stage = new Stage
                {
                    Name = record["Stage"].ToString()
                },
                

            };
        }
        public static int UpdateLineUp(LineUp lnp)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE LineUp SET From=@From, Until=@Until, Band=@Band, Stage=@Stage WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", lnp.ID);
                DbParameter par2 = Database.AddParameter("@From", lnp.From);
                DbParameter par3 = Database.AddParameter("@Until", lnp.Until);
                DbParameter par4 = Database.AddParameter("@Band", lnp.Band);
                DbParameter par5 = Database.AddParameter("@Stage", lnp.Stage);

                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans,sql, par2, par3, par4, par5, par1);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception){
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

    }
}
