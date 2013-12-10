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
        private string _ID;

        public string ID
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
        private string _picture;

        public string Picture
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
        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; }
        }
        public static ObservableCollection<Band> GetBand()
        {
            string sql = "SELECT * FROM Band";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            ObservableCollection<Band> band = new ObservableCollection<Band>();

            while (reader.Read())
            {
                band.Add(Create(reader));
            }
            return band;
        }

        private static Band Create(IDataRecord record)
        {
            return new Band()
            {
                ID = record["ID"].ToString(),
                Name = record["Name"].ToString(),
                Description = record["Description"].ToString(),
                Facebook = record["Facebook"].ToString(),
                Twitter = record["Twitter"].ToString()

            };
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
