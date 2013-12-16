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
    public class ContactpersonType
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

        public static ObservableCollection<ContactpersonType> contactpersontypes = new ObservableCollection<ContactpersonType>();

        public static ObservableCollection<ContactpersonType> GetContactpersonTypes()
        {
            string sql = "SELECT * FROM ContactpersonType";
            // DbParameter par1= Database.AddParameter("par1","jan")
            DbDataReader reader = Database.GetData(sql);//,par1);

            

            while (reader.Read())
            {
                contactpersontypes.Add(Create(reader));
            }
            return contactpersontypes;
        }

        private static ContactpersonType Create(IDataRecord record)
        {
            return new ContactpersonType()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString()

            };
        }

        public static int UpdateContactpersonType(ContactpersonType cpt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "UPDATE ContactpersonType SET Name=@name WHERE ID=@ID";

                DbParameter par1 = Database.AddParameter("@ID", cpt.ID);
                DbParameter par2 = Database.AddParameter("@Name", cpt.Name);


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

        public static int InsertContactpersonType(ContactpersonType cpt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "INSERT INTO ContactpersonType (Name) VALUES (@name)";

                //DbParameter par1 = Database.AddParameter("@ID", "4");
                DbParameter par2 = Database.AddParameter("@Name", cpt.Name);


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

        public static int DeleteContactpersonType(ContactpersonType cpt)
        {
            DbTransaction trans = null;

            try
            {
                trans = Database.BeginTransaction();
                string sql = "DELETE FROM ContactpersonType WHERE Name=@name";

                DbParameter par1 = Database.AddParameter("@ID", cpt.ID);
                DbParameter par2 = Database.AddParameter("@Name", cpt.Name);


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
        public static void PrintContactpersonTypes(ObservableCollection<ContactpersonType> Contactpersontypes)
        {

            foreach (ContactpersonType contactpersontype in Contactpersontypes)
            {
                Console.WriteLine(contactpersontype.ToString());
            }
        }
    }
}
