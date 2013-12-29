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
    public class Festival
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public static ObservableCollection<Festival> datums = new ObservableCollection<Festival>();
        public static int aantal = 1;
        public static ObservableCollection<Festival> GetDatums()
        {
            string sql = "SELECT * FROM Festivaldagen";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            
           

            while (reader.Read())
            {
                datums.Add(Create(reader));
                aantal++;
            }
            return datums;
        }

        private static Festival Create(IDataRecord record)
        {
            return new Festival()
            {
                ID = (int)record["ID"],
                StartDate = (DateTime)record["StartDate"],
                EndDate = (DateTime)record["EndDate"]
                
            };
        }

        public static int UpdateDatum(Festival fst)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE Festivaldagen SET StartDate=@StartDate,EndDate=@EndDate WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", aantal);
                DbParameter par2 = Database.AddParameter("@Name", fst.StartDate);
                DbParameter par3 = Database.AddParameter("@Price", fst.EndDate);
                


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par1, par3);
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
        
    }
}
