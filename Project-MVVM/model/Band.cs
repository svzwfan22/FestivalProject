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
    public class Band
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
        private Byte[] _picture;

        public Byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _twitter;

        public string Twitter
        {
            get { return _twitter; }
            set { _twitter = value; }
        }
        private string _facebook;

        public string Facebook
        {
            get { return _facebook; }
            set { _facebook = value; }
        }
        private string _genres;
        public string Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; }
        }

        private static ObservableCollection<Genre> _genreList;
        public static ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; }
        }

        private ObservableCollection<Genre> _genreListBand = new ObservableCollection<Genre>();
        public ObservableCollection<Genre> GenreListBand
        {
            get { return _genreListBand; }
            set { _genreListBand = value; }
        }

        public static ObservableCollection<Band> band = new ObservableCollection<Band>();
        public static int aantal = 1;
        public static int aantalgenres = 1;
        public static ObservableCollection<Band> GetBand()
        {
            GenreList = Genre.genres;
            BandGenre.GenreList = GenreList;

            string sql = "SELECT * FROM Bands";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Band> band = new ObservableCollection<Band>();

            while (reader.Read())
            {
                band.Add(Create(reader));
                aantal++;
            }
            return band;
        }

        private static Band Create(IDataRecord record)
        {
            Band band = new Band();
            band.ID = Convert.ToInt32(record["ID"]);
            band.Name = record["Name"].ToString();

            if (!DBNull.Value.Equals(record["Picture"]))
            {
                band.Picture = (byte[])(record["Picture"]);
            }
            else
            {
                Byte[] b = new Byte[0];
                band.Picture = b;
            }

            band.Description = record["Description"].ToString();
            band.Twitter = record["Twitter"].ToString();
            band.Facebook = record["Facebook"].ToString();
            //band.Genres = record["Genres"].ToString();

            
            string[] split = record["Genres"].ToString().Split(new Char[] { ';' });

            for (int i = 0; i < split.Count() - 1; i++)
            {
                Genre genre = new Genre();
                genre.ID = Convert.ToInt32(split[i]);
                genre.Name = GenreList[Convert.ToInt32(split[i]) - 1].Name;
                band.GenreListBand.Add(genre);
                aantalgenres++;
            }
        

            return band;
        }

        public static int SaveBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO Bands(Name,Description,Facebook,Genres,Twitter,Picture) VALUES (@Name,@Description,@Facebook,@Genres,@Twitter,@Picture);";

                //DbParameter par1 = Database.AddParameter("@ID", cpn.ID);
                DbParameter par2 = Database.AddParameter("@Name", bnd.Name);
                DbParameter par3 = Database.AddParameter("@Description", bnd.Description);
                DbParameter par4 = Database.AddParameter("@Facebook", bnd.Facebook);
                DbParameter par5 = Database.AddParameter("@Genres", bnd.Genres);
                DbParameter par6 = Database.AddParameter("@Twitter", bnd.Twitter);
                DbParameter par7 = Database.AddParameter("@Picture", bnd.Picture);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par3, par4, par5, par6, par7);
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

        public static int UpdateBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE Bands SET Name=@name, Description=@Description, Facebook=@Facebook, Genres=@Genres, Twitter=@Twitter, Picture=@Picture WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", bnd.ID);
                DbParameter par2 = Database.AddParameter("@Name", bnd.Name);
                DbParameter par3 = Database.AddParameter("@Description", bnd.Description);
                DbParameter par4 = Database.AddParameter("@Facebook", bnd.Facebook);
                DbParameter par5 = Database.AddParameter("@Genres", bnd.Genres);
                DbParameter par6 = Database.AddParameter("@Twitter", bnd.Twitter);
                DbParameter par7 = Database.AddParameter("@Picture", bnd.Picture);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5, par6, par7);
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

        public static int DeleteBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Bands WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", bnd.ID);
                DbParameter par2 = Database.AddParameter("@Name", bnd.Name);
                DbParameter par3 = Database.AddParameter("@Description", bnd.Description);
                DbParameter par4 = Database.AddParameter("@Facebook", bnd.Facebook);
                DbParameter par5 = Database.AddParameter("@Genres", bnd.Genres);
                DbParameter par6 = Database.AddParameter("@Twitter", bnd.Twitter);
                DbParameter par7 = Database.AddParameter("@Picture", bnd.Picture);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par1, par2);
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
            return Name + " " + ID + " " + Description + " " + Facebook + " " + Twitter;
        }

        public static void PrintBands(ObservableCollection<Band> Bands)
        {

            foreach (Band band in Bands)
            {
                Console.WriteLine(band.ToString());
            }
        }
    }
}
