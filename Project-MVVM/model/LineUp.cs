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
        public static ObservableCollection<LineUp> lineup = new ObservableCollection<LineUp>();
        public static int aantal = 1;
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
            return lineup;
        }

        private static LineUp Create(IDataRecord record)
        {
            return new LineUp()
            {
                ID = (int)record["ID"],
                
                From = record["From"].ToString(),
                Until = record["Until"].ToString(),
                
                Date = (DateTime)record["Date"],
                Bands = new Band
                {
                    ID = (int)record["Band"],
                    Name = BandjesList[(int)record["Band"]-1].Name
                },
                Stages = new Stage
                {
                    ID = (int)record["Stage"],
                    Name = StagekesList[(int)record["Stage"]-1].Name
                }
                

            };
        }
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

                rowsaffected += Database.ModifyData(trans,sql, par2, par3, par4, par5,par6, par1);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception){
                 trans.Rollback();
                return 0;
            }
        }

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
                trans.Rollback();
                return 0;
            }
        }

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

    }
}
