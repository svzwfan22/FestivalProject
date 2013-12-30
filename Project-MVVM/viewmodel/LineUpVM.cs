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
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line-Up"; }
        }

        private ObservableCollection<LineUp> _lineUpList;

        public ObservableCollection<LineUp> LineUpList
        {
            get { return _lineUpList; }
            set { _lineUpList = value; OnPropertyChanged("LineUpList"); }
        }

        private LineUp _selectedLineUp;

        public LineUp SelectedLineUp
        {
            get { return _selectedLineUp; }
            set { _selectedLineUp = value; OnPropertyChanged("SelectedLineUp"); }
        }

        private ObservableCollection<Stage> _stageList;

        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }

        private ObservableCollection<Band> _bandList;

        public ObservableCollection<Band> BandList
        {
            get { return _bandList; }
            set { _bandList = value; OnPropertyChanged("BandList"); }
        }
        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); }
        }
        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        

        public LineUpVM()
        {
            //_stageList = Stage.GetStages();
            _lineUpList = LineUp.GetLineUp();
        }

        public ICommand VoegToeCommand
        {
            get { return new RelayCommand(VoegToeLineUp); }
        }

        private void VoegToeLineUp()
        {
            LineUp lnp = new LineUp();
            
            lnp.Bands = new Band();
            lnp.Stages = new Stage();
            lnp.ID = LineUp.aantal;

            LineUp.lineup.Add(lnp);
        }

        public ICommand UpdateLineUpCommand
        {
            get { return new RelayCommand<LineUp>(UpdateLineUp); }
        }

        public void UpdateLineUp(LineUp lnp)
        {



            LineUp lineup = SelectedLineUp;
            int id = (int)lineup.ID;

            if (id != LineUp.aantal)
            {
                LineUp.UpdateLineUp(SelectedLineUp);

            }
            else
            {
                LineUp.InsertLineUp(SelectedLineUp);

            }
        }

        public ICommand DeleteLineUpCommand
        {
            get { return new RelayCommand(DeleteLineUp); }
        }


        public void DeleteLineUp()
        {
            LineUp.DeleteLineUp(SelectedLineUp);
            if (SelectedLineUp != null)
                LineUpList.Remove(SelectedLineUp);
            Console.WriteLine("delete command");
            LineUp.PrintLineUps(LineUpList);
        }

        
    }
}
