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
    class TimeLineVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "TimeLine"; }
        }

    }
}
