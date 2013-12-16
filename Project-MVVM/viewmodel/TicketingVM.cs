using Project_MVVM.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_MVVM.viewmodel
{
    class TicketingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Ticketing"; }
        }

        private ObservableCollection<Ticket> _ticketList;

        public ObservableCollection<Ticket> TicketList
        {
            get { return _ticketList; }
            set { _ticketList = value; OnPropertyChanged("TicketList"); }
        }

        private ObservableCollection<TicketType> _ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return _ticketTypeList; }
            set { _ticketTypeList = value; OnPropertyChanged("TicketTypeList"); }
        }

        public TicketingVM()
        {
            _ticketList = Ticket.GetTicket();
            _ticketTypeList = TicketType.GetTicketTypes();
            
        }
    }
}
