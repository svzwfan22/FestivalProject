﻿using GalaSoft.MvvmLight.Command;
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
    class AlgemeenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Algemeen"; }
        }

        private ObservableCollection<Festival> _datumList;

        public ObservableCollection<Festival> DatumList
        {
            get { return _datumList; }
            set { _datumList = value; OnPropertyChanged("DatumList"); }
        }

        private ObservableCollection<Contactperson> _contactpersonList;

        public ObservableCollection<Contactperson> ContactpersonList
        {
            get { return _contactpersonList; }
            set { _contactpersonList = value; OnPropertyChanged("ContactpersonList"); }
        }

        private ObservableCollection<ContactpersonType> _contactpersonTypeList;

        public ObservableCollection<ContactpersonType> ContactpersonTypeList
        {
            get { return _contactpersonTypeList; }
            set { _contactpersonTypeList = value; OnPropertyChanged("ContactpersonTypeList"); }
        }
        private ObservableCollection<Genre> _genreList;

        public ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged("GenreList"); }
        }
        private ObservableCollection<Stage> _stageList;

        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }

        //de lijsten opvullen die getoond zullen worden
        public AlgemeenVM()
        {
           // _contactpersonList = Contactperson.GetContactpersons();
            _datumList = Festival.GetDatums();
            _contactpersonTypeList = ContactpersonType.contactpersontypes;
            _genreList = Genre.GetGenres();
            _stageList = Stage.stages;
        }

        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }

        private Festival _selectedDatum;

        public Festival SelectedDatum
        {
            get { return _selectedDatum; }
            set { _selectedDatum = value; OnPropertyChanged("SelectedDatum"); }
        }

        public ICommand UpdateDatumCommand
        {
            get { return new RelayCommand<Festival>(UpdateDatum); }
        }


        public void UpdateDatum(Festival fst)
        {
            Festival.UpdateDatum(SelectedDatum);

        }

        public ICommand UpdateGenreCommand
        {
            get { return new RelayCommand(UpdateGenre); }
        }
        //genre toevoegen
        public void UpdateGenre()
        {
            

            Genre gnr = new Genre();

            gnr.ID = Genre.aantal;
            //gnr.Name = new Genre();
            Genre.genres.Add(gnr);

            //Genre.UpdateGenre(SelectedGenre);
        }
        //genre verwijderen
        public ICommand DeleteGenreCommand
        {
            get { return new RelayCommand<Genre>(DeleteGenre); }
        }


        public void DeleteGenre(Genre gnr)
        {
            if (SelectedGenre == null) {
                MessageBox.Show("Gelieve het genre te selecteren dat u wilt verwijderen.");
            }
            else
            {
                Genre.DeleteGenre(SelectedGenre);
                if (SelectedGenre != null)
                    GenreList.Remove(SelectedGenre);
                Console.WriteLine("delete command");
                Genre.PrintGenres(GenreList);
            }
        }

        private Genre _newGenre;

        public Genre NewGenre
        {
            get { return _newGenre; }
            set { _newGenre = value; OnPropertyChanged("NewGenre"); }
        }


        //genre toevoegen of updaten
        public ICommand AddGenreCommand
        {
            get { return new RelayCommand<Genre>(AddGenre); }
        }


        public void AddGenre(Genre gnr)
        {
            if (SelectedGenre == null) {
                MessageBox.Show("Gelieve het genre te selecteren dat u wilt opslaan.");
            }
            else
            {

                Genre genre = SelectedGenre;
                int id = (int)genre.ID;

                if (id != Genre.aantal)
                {
                    Genre.UpdateGenre(SelectedGenre);
                }
                else
                {
                    Genre.InsertGenre(SelectedGenre);
                }
            }
            
        }

        private ContactpersonType _selectedContactpersonType;

        public ContactpersonType SelectedContactpersonType
        {
            get { return _selectedContactpersonType; }
            set { _selectedContactpersonType = value; OnPropertyChanged("SelectedContactpersonType"); }
        }

        public ICommand UpdateContactpersonTypeCommand
        {
            get { return new RelayCommand(UpdateContactpersonType); }
        }
        //contactpersoontype toevoegen
        public void UpdateContactpersonType()
        {
            ContactpersonType cpt = new ContactpersonType();

            cpt.ID = ContactpersonType.aantal;
            
            ContactpersonType.contactpersontypes.Add(cpt);
        }
        //contactpersoontype verwijderen
        public ICommand DeleteContactpersonTypeCommand
        {
            get { return new RelayCommand<ContactpersonType>(DeleteContactpersonType); }
        }


        public void DeleteContactpersonType(ContactpersonType cpt)
        {
            if (SelectedContactpersonType == null) {
                MessageBox.Show("Gelieve het contactpersoonstype die u wilt verwijderen te selecteren.");
            }
            else
            {
                ContactpersonType.DeleteContactpersonType(SelectedContactpersonType);
                if (SelectedContactpersonType != null)
                    ContactpersonTypeList.Remove(SelectedContactpersonType);
                Console.WriteLine("delete command");
                ContactpersonType.PrintContactpersonTypes(ContactpersonTypeList);
            }
        }

        private ContactpersonType _newContactpersonType;

        public ContactpersonType NewContactpersonType
        {
            get { return _newContactpersonType; }
            set { _newContactpersonType = value; OnPropertyChanged("NewContactpersonType"); }
        }

        //contactpersoontype toevoegen of updaten

        public ICommand AddContactpersonTypeCommand
        {
            get { return new RelayCommand<ContactpersonType>(AddContactpersonType); }
        }


        public void AddContactpersonType(ContactpersonType cpt)
        {
            if (SelectedContactpersonType == null) {
                MessageBox.Show("Gelieve het contactpersoonstype die u wilt opslaan te selecteren.");
            }
            else
            {
                ContactpersonType contactpersooon = SelectedContactpersonType;
                int id = (int)contactpersooon.ID;

                if (id != ContactpersonType.aantal)
                {
                    ContactpersonType.UpdateContactpersonType(SelectedContactpersonType);
                }
                else
                {
                    ContactpersonType.InsertContactpersonType(SelectedContactpersonType);
                }
            }
        }

        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); }
        }
        //stage toevoegen
        public ICommand UpdateStageCommand
        {
            get { return new RelayCommand(UpdateStage); }
        }

        public void UpdateStage()
        {
            Stage stg = new Stage();

            stg.ID = Stage.aantal;

            Stage.stages.Add(stg);
        }

        //stage verwijderen
        public ICommand DeleteStageCommand
        {
            get { return new RelayCommand<Stage>(DeleteStage); }
        }


        public void DeleteStage(Stage stg)
        {
            if (SelectedStage == null) {
                MessageBox.Show("Gelieve een stage te selecteren die u wilt verwijderen.");
            }
            else
            {
                Stage.DeleteStage(SelectedStage);
                if (SelectedStage != null)
                    StageList.Remove(SelectedStage);
                Console.WriteLine("delete command");
                Stage.PrintStages(StageList);
            }
        }

        private Stage _newStage;

        public Stage NewStage
        {
            get { return _newStage; }
            set { _newStage = value; OnPropertyChanged("NewStage"); }
        }


        //stage toevoegen of updaten
        public ICommand AddStageCommand
        {
            get { return new RelayCommand<Stage>(AddStage); }
        }


        public void AddStage(Stage stg)
        {
            if (SelectedStage == null) {
                MessageBox.Show("Gelieve een stage te selecteren die u wilt opslaan.");
            }
            else
            {
                Stage stagee = SelectedStage;
                int id = (int)stagee.ID;

                if (id != Stage.aantal)
                {
                    Stage.UpdateStage(SelectedStage);
                }
                else
                {
                    Stage.InsertStage(SelectedStage);
                }
            }
        }
    }
}
