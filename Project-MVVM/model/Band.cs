using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_MVVM.model
{
    public class Band : IDataErrorInfo
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _name;
        [Required(ErrorMessage = "U moet een naam invullen")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Een naam moet tussen de 3 en 50 karakters liggen")]
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
        [Required(ErrorMessage = "U moet een beschrijving invullen")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Een omschrijving moet tussen de 5 en 255 karakters liggen")]
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
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
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
            

            string sql = "SELECT * FROM Banden";
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
            band.Genres = Genre.GetGenresByBandID(Convert.ToInt32(record["ID"]));
        

            return band;
        }

        public static int SaveBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO Banden(Name,Description,Facebook,Twitter,Picture) VALUES (@Name,@Description,@Facebook,@Twitter,@Picture);";

                //DbParameter par1 = Database.AddParameter("@ID", cpn.ID);
                DbParameter par2 = Database.AddParameter("@Name", bnd.Name);
                DbParameter par3 = Database.AddParameter("@Description", bnd.Description);
                DbParameter par4 = Database.AddParameter("@Facebook", bnd.Facebook);
                //DbParameter par5 = Database.AddParameter("@Genres", bnd.Genres);
                DbParameter par6 = Database.AddParameter("@Twitter", bnd.Twitter);
                DbParameter par7 = Database.AddParameter("@Picture", bnd.Picture);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par3, par4, par6, par7);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de band wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }

        public static int UpdateBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE Banden SET Name=@name, Description=@Description, Facebook=@Facebook, Twitter=@Twitter, Picture=@Picture WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", bnd.ID);
                DbParameter par2 = Database.AddParameter("@Name", bnd.Name);
                DbParameter par3 = Database.AddParameter("@Description", bnd.Description);
                DbParameter par4 = Database.AddParameter("@Facebook", bnd.Facebook);
                //DbParameter par5 = Database.AddParameter("@Genres", bnd.Genres);
                DbParameter par6 = Database.AddParameter("@Twitter", bnd.Twitter);
                DbParameter par7 = Database.AddParameter("@Picture", bnd.Picture);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par6, par7);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de band wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }

        public static int DeleteBand(Band bnd)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM Banden WHERE Name=@name";

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

        public string Error
        {
            get { return "Het object is niet valid"; }
        }

        public string this[string columName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columName
                    });
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return string.Empty;
            }
        }
    }
}
