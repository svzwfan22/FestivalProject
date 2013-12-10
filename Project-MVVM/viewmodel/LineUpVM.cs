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

        public LineUpVM()
        {
            _stageList = Stage.GetStages();
            _lineUpList = LineUp.GetLineUp();
        }

        private LineUp _selectedLineUp;

        public LineUp SelectedLineUp
        {
            get { return _selectedLineUp; }
            set { _selectedLineUp = value; OnPropertyChanged("SelectedLineUp"); }
        }

        public ICommand UpdateLineUpCommand
        {
            get { return new RelayCommand<LineUp>(UpdateLineUp); }
        }

        public void UpdateLineUp(LineUp lnp)
        {
            LineUp.UpdateLineUp(SelectedLineUp);
        }

        public ICommand DeleteLineUpCommand
        {
            get { return new RelayCommand(DeleteLineUp); }
        }


        public void DeleteLineUp()
        {
            if (SelectedLineUp != null)
                LineUpList.Remove(SelectedLineUp);
            Console.WriteLine("delete command");
            LineUp.PrintLineUps(LineUpList);
        }

        private ObservableCollection<Stage> _stageList;

        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }

        
    }
}
