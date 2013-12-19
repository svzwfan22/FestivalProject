using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project_MVVM.model
{
    public class Genre
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

        //public static ObservableCollection<Genre> GetGenres()
        //{
        //    ObservableCollection<Genre> Genres = new ObservableCollection<Genre>();
        //    //Create the XmlDocument.
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("Genres.xml");

        //    //Display all the controles.
        //    XmlNodeList elemList = doc.GetElementsByTagName("genre");
        //    for (int i = 1; i < elemList.Count; i++)
        //    {
        //        Genre sc = new Genre();
        //        sc.Name = elemList[i]["naam"].InnerText;
        //        sc.ID = elemList[i]["id"].InnerText;
                

        //        Genres.Add(sc);
        //    }
        //    return Genres;
        //}


        //public override string ToString()
        //{
        //    return Name + "" + ID;
        //}
        //public static void PrintGenres(ObservableCollection<Genre> Genres)
        //{

        //    foreach (Genre genre in Genres)
        //    {
        //        Console.WriteLine(genre.ToString());
        //    }
        //}
        public static ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
        public static int aantal = 1;
        public static ObservableCollection<Genre> GetGenres()
        {
            string sql = "SELECT * FROM Genre";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

            while (reader.Read())
            {
                genres.Add(Create(reader));
                aantal++;
            }
            return genres;
        }

        private static Genre Create(IDataRecord record)
        {
            return new Genre()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString()

            };
        }

        public static int UpdateGenre(Genre gnr)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE Genre SET Name=@name WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", gnr.ID);
                DbParameter par2 = Database.AddParameter("@Name", gnr.Name);
                

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

        public static int InsertGenre(Genre gnr)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO Genre (Name) VALUES (@name)";

                //DbParameter par1 = Database.AddParameter("@ID", "4");
                DbParameter par2 = Database.AddParameter("@Name", gnr.Name);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2);
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

        public static int DeleteGenre(Genre gnr)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Genre WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", gnr.ID);
                DbParameter par2 = Database.AddParameter("@Name", gnr.Name);


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
        public static void PrintGenres(ObservableCollection<Genre> Genres)
        {

            foreach (Genre genre in Genres)
            {
                Console.WriteLine(genre.ToString());
            }
        }
        
    }
}
