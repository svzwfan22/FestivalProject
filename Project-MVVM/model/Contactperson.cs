using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Project_MVVM.model
{
    public class Contactperson 
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
            set { _name = value;  }
        }

        //private string _voornaam;

        //public string Voornaam
        //{
        //    get { return _voornaam; }
        //    set { _voornaam = value;  }
        //}
        private string _company;

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        private ContactpersonType _jobRole;

        public ContactpersonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value;  }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value;  }
        }
        private string _cellphone;

        public string Cellphone
        {
            get { return _cellphone; }
            set { _cellphone = value; }
        }

        //private string _type;

        //public string Type
        //{
        //    get { return _type; }
        //    set { _type = value;  }
        //}

        //public static ObservableCollection<Contactperson> GetContactpersons()
        //{
        //    ObservableCollection<Contactperson> Contactpersons = new ObservableCollection<Contactperson>();
        //    //Create the XmlDocument.
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("Contactpersonen.xml");

        //    //Display all the controles.
        //    XmlNodeList elemList = doc.GetElementsByTagName("contactpersoon");
        //    for (int i = 1; i < elemList.Count; i++)
        //    {
        //        Contactperson sc = new Contactperson();
        //        sc.Name = elemList[i]["naam"].InnerText;
        //        sc.Voornaam = elemList[i]["voornaam"].InnerText;
        //        sc.Phone = elemList[i]["telefoon"].InnerText;
        //        sc.Email = elemList[i]["email"].InnerText;
        //        sc.Type = elemList[i]["type"].InnerText;

        //        Contactpersons.Add(sc);
        //    }
        //    return Contactpersons;
        //}


        //public override string ToString()
        //{
        //    return Name + "" + Voornaam + " " + Phone + "" + Email + "" + Type;
        //}
        //public static void PrintContactpersons(ObservableCollection<Contactperson> Contactpersons)
        //{

        //    foreach (Contactperson contactperson in Contactpersons)
        //    {
        //        Console.WriteLine(contactperson.ToString());
        //    }
        //}

        private static ObservableCollection<ContactpersonType> _jobRoleList;

        public static ObservableCollection<ContactpersonType> JobRoleList
        {
            get { return  _jobRoleList; }
            set {  _jobRoleList = value; }
        }
        public static ObservableCollection<Contactperson> contactpersons = new ObservableCollection<Contactperson>();
        public static int aantal = 1;
        public static ObservableCollection<Contactperson> GetContactpersons()
        {
            string sql = "SELECT * FROM ContactPerson";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            //ObservableCollection<Contactperson> contactpersons = new ObservableCollection<Contactperson>();
            JobRoleList = ContactpersonType.GetContactpersonTypes();

            while (reader.Read())
            {
                contactpersons.Add(Create(reader));
                aantal++;
            }
            return contactpersons;
        }

        private static Contactperson Create(IDataRecord record)
        {
            //Contactperson contactpersoon = new Contactperson();
            return new Contactperson()
            {
            ID = (int)record["ID"],
                Name = record["Name"].ToString(),
                Company = record["Company"].ToString(),
                //JobRole = record["JobRole"].ToString(),
                City = record["City"].ToString(),
                Email = record["Email"].ToString(),
                Cellphone = record["Cellphone"].ToString(),
                Phone = record["Phone"].ToString(),
                JobRole = new ContactpersonType {
                    ID = (int)record["JobRole"],
                    Name = JobRoleList[(int)record["JobRole"]-1].Name

                    
                    
                }
            };
                //return contactpersoon;
        }

        public static int DeleteContactperson(Contactperson cpn)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM ContactPerson WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", cpn.ID);
                DbParameter par2 = Database.AddParameter("@Name", cpn.Name);
                DbParameter par3 = Database.AddParameter("@Company", cpn.Company);
                DbParameter par4 = Database.AddParameter("@City", cpn.City);
                DbParameter par5 = Database.AddParameter("@Email", cpn.Email);
                DbParameter par6 = Database.AddParameter("@Cellphone", cpn.Cellphone);
                DbParameter par7 = Database.AddParameter("@Phone", cpn.Phone);
                DbParameter par8 = Database.AddParameter("@JobRole", cpn.JobRole);


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

        public static int SaveContactperson(Contactperson cpn)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO ContactPerson(Name,Company,City,Email,Cellphone,Phone,JobRole) VALUES (@Name,@Company,@City,@Email,@Cellphone,@Phone,@JobRole);";

                //DbParameter par1 = Database.AddParameter("@ID", cpn.ID);
                DbParameter par2 = Database.AddParameter("@Name", cpn.Name);
                DbParameter par3 = Database.AddParameter("@Company", cpn.Company);
                DbParameter par4 = Database.AddParameter("@City", cpn.City);
                DbParameter par5 = Database.AddParameter("@Email", cpn.Email);
                DbParameter par6 = Database.AddParameter("@Cellphone", cpn.Cellphone);
                DbParameter par7 = Database.AddParameter("@Phone", cpn.Phone);
                DbParameter par8 = Database.AddParameter("@JobRole", cpn.JobRole.ID);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par2, par3, par4, par5, par6,  par7, par8);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de contactpersoon wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }

        public static int UpdateContactperson(Contactperson cpn)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE ContactPerson SET Name=@name, Company=@Company, City=@City, Email=@Email, Cellphone=@Cellphone, Phone=@Phone, JobRole=@JobRole WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", cpn.ID);
                DbParameter par2 = Database.AddParameter("@Name", cpn.Name);
                DbParameter par3 = Database.AddParameter("@Company", cpn.Company);
                DbParameter par4 = Database.AddParameter("@City", cpn.City);
                DbParameter par5 = Database.AddParameter("@Email", cpn.Email);
                DbParameter par6 = Database.AddParameter("@Cellphone", cpn.Cellphone);
                DbParameter par7 = Database.AddParameter("@Phone", cpn.Phone);
                DbParameter par8 = Database.AddParameter("@JobRole", cpn.JobRole.ID);


                int rowsaffected = 0;

                rowsaffected += Database.ModifyData(trans, sql, par1, par2, par3, par4, par5, par6, par7, par8);
                Console.WriteLine(rowsaffected + " row(s) are affected");
                trans.Commit();
                return rowsaffected;
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve alle velden in te vullen vooraleer u de contactpersoon wilt opslaan.");
                //trans.Rollback();
                return 0;
            }
        }

        public static ObservableCollection<Contactperson> GetContactsByString(string search)
        {
            ObservableCollection<Contactperson> lstGevondenContacts = new ObservableCollection<Contactperson>();
            foreach (Contactperson contactperson in contactpersons)
            {
                if (contactperson.Name.ToUpper().Contains(search.ToUpper()) || contactperson.Phone.ToUpper().Contains(search.ToUpper()) || contactperson.Email.ToUpper().Contains(search.ToUpper()) || contactperson.Company.ToUpper().Contains(search.ToUpper()) || contactperson.City.ToUpper().Contains(search.ToUpper()) || contactperson.Cellphone.ToUpper().Contains(search.ToUpper()))
                {
                    lstGevondenContacts.Add(contactperson);
                }
            }
            return lstGevondenContacts;
        }

        public override string ToString()
        {
            return ID + " " + Name;
        }
        public static void PrintContactpersons(ObservableCollection<Contactperson> Contactperons)
        {

            foreach (Contactperson contactperson in Contactperons)
            {
                Console.WriteLine(contactperson.ToString());
            }
        }

        
        
    }
}
