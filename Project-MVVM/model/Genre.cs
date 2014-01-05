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

        //alle genres uit de database ophalen die gekoppeld zijn met een band, op basis van het band id en het genre id
        public static ObservableCollection<Genre> GetGenresByBandID(int id)
        {
            try
            {
                string query = "SELECT * FROM Genre INNER JOIN BandGenre on BandGenre.GenreID = Genre.ID WHERE BandID = " + id + ";";

                ObservableCollection<Genre> gevondenGenres = new ObservableCollection<Genre>();
                DbDataReader reader = Database.GetData(query);
                while (reader.Read())
                {
                    Genre genre = new Genre();
                    genre.Name = Convert.ToString(reader["Name"]);
                    genre.ID = (int)(reader["ID"]);
                    gevondenGenres.Add(genre);
                }
                if (reader != null)
                    reader.Close();
                return gevondenGenres;
            }
            catch (Exception ex)
            {
                Console.WriteLine("get genres by band id: " + ex.Message);
                return null;
            }
        }


        public static ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
        public static int aantal = 1;

        //alle genres uit de database ophalen
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
        //de genres aanmaken voor in de lijst
        private static Genre Create(IDataRecord record)
        {
            return new Genre()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString()

            };
        }
        //een genre updaten
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
                MessageBox.Show("gelieve alles correct in te vullen");
                //trans.Rollback();
                return 0;
            }
        }
        //een genre toevoegen
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
                MessageBox.Show("gelieve alles correct in te vullen");
                //trans.Rollback();
                return 0;
            }
        }
        //een genre verwijderen
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
