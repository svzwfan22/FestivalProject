﻿using GalaSoft.MvvmLight.Command;
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

        private Ticket _selectedTicket;

        public Ticket SelectedTicket
        {
            get { return _selectedTicket; }
            set { _selectedTicket = value; OnPropertyChanged("SelectedTicket"); }
        }

        private ObservableCollection<TicketType> _ticketTypeList;

        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return _ticketTypeList; }
            set { _ticketTypeList = value; OnPropertyChanged("TicketTypeList"); }
        }

        private TicketType _selectedTicketType;

        public TicketType SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value; OnPropertyChanged("SelectedTicketType"); }
        }

        public TicketingVM()
        {
            _ticketList = Ticket.GetTicket();
            _ticketTypeList = TicketType.ticketType;
            
        }

        public ICommand AddTicketTypeCommand
        {
            get { return new RelayCommand(AddTicketType); }
        }
        public void AddTicketType()
        {


            TicketType tkt = new TicketType();

            tkt.ID = TicketType.aantalType;
            
            TicketType.ticketType.Add(tkt);

            
        }

        public ICommand SaveTicketTypeCommand
        {
            get { return new RelayCommand<TicketType>(SaveTicketType); }
        }

        public void SaveTicketType(TicketType tkt)
        {
            TicketType tickettype = SelectedTicketType;
            int id = (int)tickettype.ID;

            if (id != TicketType.aantalType)
            {
                TicketType.UpdateTicketType(SelectedTicketType);
            }
            else
            {
                TicketType.InsertTicketType(SelectedTicketType);
            }
        }

        public ICommand DeleteTicketTypeCommand
        {
            get { return new RelayCommand<TicketType>(DeleteTicketType); }
        }

        public void DeleteTicketType(TicketType tkt)
        {
            TicketType.DeleteTicketType(SelectedTicketType);
            if (SelectedTicketType != null)
                TicketTypeList.Remove(SelectedTicketType);
            Console.WriteLine("delete command");
            TicketType.PrintTickets(TicketTypeList);
        }

        public ICommand AddTicketCommand
        {
            get { return new RelayCommand(AddTicket); }
        }
        public void AddTicket()
        {


            Ticket tkt = new Ticket();
            tkt.TicketTypeID = new TicketType();
            tkt.ID = Ticket.aantal;

            Ticket.tickets.Add(tkt);
        }

        public ICommand SaveTicketCommand
        {
            get { return new RelayCommand<Ticket>(SaveTicket); }
        }

        public void SaveTicket(Ticket tkt)
        {
            Ticket ticket = SelectedTicket;
            int id = (int)ticket.ID;

            if (id != Ticket.aantal)
            {
                Ticket.UpdateTicket(SelectedTicket);
                
            }
            else
            {
                Ticket.InsertTicket(SelectedTicket);
                
            }
        }

        public ICommand DeleteTicketCommand
        {
            get { return new RelayCommand<Ticket>(DeleteTicket); }
        }

        public void DeleteTicket(Ticket tkt)
        {
            Ticket.DeleteTicket(SelectedTicket);
            if (SelectedTicket != null)
                TicketList.Remove(SelectedTicket);
            Console.WriteLine("delete command");
            Ticket.PrintTicketjes(TicketList);
        }

        public ICommand PrintTicketCommand
        {
            get { return new RelayCommand<Ticket>(PrintTicket); }
        }

        public void PrintTicket(Ticket tkt)
        {
            Ticket.PrintTickets(SelectedTicket);
        }
    }
}
