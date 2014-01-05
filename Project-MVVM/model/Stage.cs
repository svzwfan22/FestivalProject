using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Project_MVVM.model
{
    public class Stage
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

        
        public static ObservableCollection<Stage> stages = new ObservableCollection<Stage>();
        public static int aantal = 1;
        //alle data uit de database halen
        public static ObservableCollection<Stage> GetStages()
        {
            string sql = "SELECT * FROM Stage";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Stage> stages = new ObservableCollection<Stage>();

            while (reader.Read())
            {
                stages.Add(Create(reader));
                aantal++;
            }
            return stages;
        }
        //voor iedere lijn een stageitem maken voor ind e lijst
        private static Stage Create(IDataRecord record)
        {
            return new Stage()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString()
                
            };
        }
        //een stage updaten, aanpassen
        public static int UpdateStage(Stage stg)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE Stage SET Name=@name WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", stg.ID);
                DbParameter par2 = Database.AddParameter("@Name", stg.Name);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par1);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("gelieve alles correct in te vullen");
                //trans.Rollback();
                return 0;
            }
        }
        //een stage toevoegen
        public static int InsertStage(Stage stg)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO Stage (Name) VALUES (@name)";

                //DbParameter par1 = Database.AddParameter("@ID", "4");
                DbParameter par2 = Database.AddParameter("@Name", stg.Name);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("gelieve alles correct in te vullen");
               // trans.Rollback();
                return 0;
            }
        }
        //een stage verwijderen
        public static int DeleteStage(Stage stg)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Stage WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", stg.ID);
                DbParameter par2 = Database.AddParameter("@Name", stg.Name);


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
            return ID + " " + Name;
        }
        public static void PrintStages(ObservableCollection<Stage> Stages)
        {

            foreach (Stage stage in Stages)
            {
                Console.WriteLine(stage.ToString());
            }
        }
    }
}
