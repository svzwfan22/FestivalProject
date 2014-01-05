using GalaSoft.MvvmLight.Command;
using Project_MVVM.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_MVVM.viewmodel
{
    class ContactpersonenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contactpers."; }
        }

        private ObservableCollection<Contactperson> _gefilterdeContacts;

        public ObservableCollection<Contactperson> GefilterdeContacts
        {
            get { return _gefilterdeContacts; }
            set { _gefilterdeContacts = value; OnPropertyChanged("GefilterdeContacts"); }
        }

        private ObservableCollection<Contactperson> _contactpersonList;

        public ObservableCollection<Contactperson> ContactpersonList
        {
            get { return _contactpersonList; }
            set { _contactpersonList = value; OnPropertyChanged("ContactpersonList"); }
        }

        
        //de lijsten opvullen
        public ContactpersonenVM()
        {
            _contactpersonList = Contactperson.GetContactpersons();
            _gefilterdeContacts = ContactpersonList;
            
        }

        private Contactperson _selectedContactperson;

        public Contactperson SelectedContactperson
        {
            get { return _selectedContactperson; }
            set { _selectedContactperson = value; OnPropertyChanged("SelectedContactperson"); }
        }

        private ContactpersonType _selectedContactpersonType;

        public ContactpersonType SelectedContactpersonType
        {
            get { return _selectedContactpersonType; }
            set { _selectedContactpersonType = value; OnPropertyChanged("SelectedContactpersonType"); }
        }
        //contactpersoon verwijderen
        public ICommand DeleteContactpersonCommand
        {
            get { return new RelayCommand<Contactperson>(DeleteContactperson); }
        }


        public void DeleteContactperson(Contactperson cpn)
        {
            if (SelectedContactperson == null) {
                MessageBox.Show("Gelieve de contactpersoon te selecteren die u wilt verwijderen.");
            }
            else
            {
                Contactperson.DeleteContactperson(SelectedContactperson);
                if (SelectedContactperson != null)
                    ContactpersonList.Remove(SelectedContactperson);
                Console.WriteLine("delete command");
                Contactperson.PrintContactpersons(ContactpersonList);
            }
        }
        //contactpersoon toevoegen
        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveContactperson); }
        }

        public void SaveContactperson()
        {
            Contactperson cp = new Contactperson();
            
            
            cp.JobRole = new ContactpersonType();
            cp.ID = Contactperson.aantal;
            Contactperson.contactpersons.Add(cp);

           
        }
        //contactpersoon toevoegen of updaten
        public ICommand UpdateContactpersonCommand
        {
            get { return new RelayCommand<Contactperson>(UpdateContactperson); }
        }

        public void UpdateContactperson(Contactperson cpn)
        {
            if (SelectedContactperson == null) {
                MessageBox.Show("Gelieve de contactpersoon te selecteren die u wilt opslaan.");
            }
            else
            {
                //Contactperson.UpdateContactperson(SelectedContactperson);
                Contactperson contactpersoon = SelectedContactperson;
                int id = (int)contactpersoon.ID;

                if (id != Contactperson.aantal)
                {
                    Contactperson.UpdateContactperson(SelectedContactperson);
                }
                else
                {
                    Contactperson.SaveContactperson(SelectedContactperson);
                }
            }
        }
        //zoeken naar een contactpersoon
        public ICommand SearchCommand
        {
            get { return new RelayCommand<string>(Search); }
        }
        private void Search(string str)
        {
            Console.WriteLine(str);
            GefilterdeContacts = Contactperson.GetContactsByString(str);
        }
    }
}
