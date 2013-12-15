using GalaSoft.MvvmLight.Command;
using Project_MVVM.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_MVVM.viewmodel
{
    class ContactpersonenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contactpers."; }
        }

        private ObservableCollection<Contactperson> _contactpersonList;

        public ObservableCollection<Contactperson> ContactpersonList
        {
            get { return _contactpersonList; }
            set { _contactpersonList = value; OnPropertyChanged("ContactpersonList"); }
        }

        public ContactpersonenVM()
        {
            _contactpersonList = Contactperson.GetContactpersons();
        }

        private Contactperson _selectedContactperson;

        public Contactperson SelectedContactperson
        {
            get { return _selectedContactperson; }
            set { _selectedContactperson = value; OnPropertyChanged("SelectedContactperson"); }
        }



        public ICommand DeleteContactpersonCommand
        {
            get { return new RelayCommand<Contactperson>(DeleteContactperson); }
        }


        public void DeleteContactperson(Contactperson cpn)
        {
            Contactperson.DeleteContactperson(SelectedContactperson);
            if (SelectedContactperson != null)
                ContactpersonList.Remove(SelectedContactperson);
            Console.WriteLine("delete command");
            Contactperson.PrintContactpersons(ContactpersonList);
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand<Contactperson>(SaveContactperson); }
        }

        public void SaveContactperson(Contactperson cpn)
        {

            Contactperson.SaveContactperson(cpn);
        }

    }
}
