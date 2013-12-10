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

        public BandVM()
        {
            _bandList = Band.GetBand();
        }

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }



        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand(DeleteBand); }
        }


        public void DeleteBand()
        {
            if (SelectedBand != null)
                BandList.Remove(SelectedBand);
            Console.WriteLine("delete command");
            Band.PrintBands(BandList);
        }
    }
}
