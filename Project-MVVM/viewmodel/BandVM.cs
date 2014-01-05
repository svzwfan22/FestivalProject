using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Project_MVVM.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_MVVM.viewmodel
{
    class BandVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Band"; }
        }

        private ObservableCollection<Band> _bandList;

        public ObservableCollection<Band> BandList
        {
            get { return _bandList; }
            set { _bandList = value; OnPropertyChanged("BandList"); }
        }

        private ObservableCollection<Genre> _genreList;
        public ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged("GenreList"); }
        }


        public BandVM()
        {
            _bandList = Band.band;
            
           
        }

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand");
            
            
            }
        }

        
       
        //band verwijderen
        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand(DeleteBand); }
        }


        public void DeleteBand()
        {
            if (SelectedBand == null) {
                MessageBox.Show("Gelieve de band te selecteren die u wilt verwijderen.");
            }
            else
            {
                Band.DeleteBand(SelectedBand);
                if (SelectedBand != null)
                    BandList.Remove(SelectedBand);
                Console.WriteLine("delete command");
                Band.PrintBands(BandList);
            }
        }
        //foto selecteren
        public ICommand SelectImageCommand
        {
            get { return new RelayCommand<DragEventArgs>(SelectImage); }
        }
        private void SelectImage(DragEventArgs obj)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "jpg Files (.jpg)|*.jpg|jpeg Files (.jpeg)|jpeg|png Files (.png)|*.png";

            if (ofd.ShowDialog() == true)
            {
                SelectedBand.Picture = GetPhoto(ofd.FileName);
                OnPropertyChanged("SelectedBand");
            }
            }

        private void SelectImage(DataObject e)
        {
            var data = e as DataObject;
            if (data.ContainsFileDropList())
            {
                var files = data.GetFileDropList();
                SelectedBand.Picture = GetPhoto(files[0]);
                OnPropertyChanged("SelectedBand");
            }
        }

        private byte[] GetPhoto(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            return data;
        }
        //band toevoegen of updaten
        public ICommand SaveBandCommand
        {
            get { return new RelayCommand<Band>(UpdateContactperson); }
        }

        public void UpdateContactperson(Band bnd)
        {
            if (SelectedBand == null)
            {
                MessageBox.Show("Gelieve de band te selecteren die u wilt opslaan.");
            }
            else
            {
                //Contactperson.UpdateContactperson(SelectedContactperson);
                Band contactpersoon = SelectedBand;
                int id = (int)contactpersoon.ID;

                if (id != Band.aantal)
                {
                    Band.UpdateBand(SelectedBand);
                }
                else
                {
                    Band.SaveBand(SelectedBand);
                }
            }
        }
        //band toevoegen
        public ICommand AddCommand
        {
            get { return new RelayCommand(AddBand); }
        }

        public void AddBand()
        {
            Band bnd = new Band();



            bnd.ID = Band.aantal;
            bnd.Name = "Nieuwe band";
            Band.band.Add(bnd);

            
        }





        
    }

        
    }

