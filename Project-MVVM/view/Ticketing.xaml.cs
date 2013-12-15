using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_MVVM.view
{
    /// <summary>
    /// Interaction logic for PartFour.xaml
    /// </summary>
    public partial class Ticketing : UserControl
    {
        public Ticketing()
        {
            InitializeComponent();
        }

        private void TicketToevoegenClick(object sender, RoutedEventArgs e)
        {
            lstTickets.Visibility = Visibility.Hidden;
           

            
            btnToevoegen.Visibility = Visibility.Hidden;

            lblTicketNaam.Visibility = Visibility.Visible;
            lblKostprijs.Visibility = Visibility.Visible;
            lblAantal.Visibility = Visibility.Visible;
            btnToevoegen2.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
            txtAantal.Visibility = Visibility.Visible;
            txtKostprijs.Visibility = Visibility.Visible;
            txtTicketNaam.Visibility = Visibility.Visible;

        }

        private void AnnulerenClick(object sender, RoutedEventArgs e)
        {
            lblTicketNaam.Visibility = Visibility.Hidden;
            lblKostprijs.Visibility = Visibility.Hidden;
            lblAantal.Visibility = Visibility.Hidden;
            btnToevoegen2.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
            txtAantal.Visibility = Visibility.Hidden;
            txtKostprijs.Visibility = Visibility.Hidden;
            txtTicketNaam.Visibility = Visibility.Hidden;

           lstTickets.Visibility = Visibility.Visible;
           
            btnToevoegen.Visibility = Visibility.Visible;

            lstReservaties.Visibility = Visibility.Visible;
            btnReserveren2.Visibility = Visibility.Visible;

            lblNaam.Visibility = Visibility.Hidden;
            lblVoornaam.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblTicket.Visibility = Visibility.Hidden;
            txtNaam.Visibility = Visibility.Hidden;
            txtVoornaam.Visibility = Visibility.Hidden;
            txtEmail.Visibility = Visibility.Hidden;
            cboTicket.Visibility = Visibility.Hidden;
            btnReserveren.Visibility = Visibility.Hidden;
            btnAnnuleren_Copy.Visibility = Visibility.Hidden;
        }

        private void TicketReserverenClick(object sender, RoutedEventArgs e)
        {
            lblNaam.Visibility = Visibility.Visible;
            lblVoornaam.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblTicket.Visibility = Visibility.Visible;
            txtNaam.Visibility = Visibility.Visible;
            txtVoornaam.Visibility = Visibility.Visible;
            txtEmail.Visibility = Visibility.Visible;
            cboTicket.Visibility = Visibility.Visible;
            btnReserveren.Visibility = Visibility.Visible;
            btnAnnuleren_Copy.Visibility = Visibility.Visible;

            lstReservaties.Visibility = Visibility.Hidden;
            btnReserveren2.Visibility = Visibility.Hidden;
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            lblTicketNaam.Visibility = Visibility.Hidden;
            lblKostprijs.Visibility = Visibility.Hidden;
            lblAantal.Visibility = Visibility.Hidden;
            btnToevoegen2.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
            txtAantal.Visibility = Visibility.Hidden;
            txtKostprijs.Visibility = Visibility.Hidden;
            txtTicketNaam.Visibility = Visibility.Hidden;

            lblNaam.Visibility = Visibility.Hidden;
            lblVoornaam.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblTicket.Visibility = Visibility.Hidden;
            txtNaam.Visibility = Visibility.Hidden;
            txtVoornaam.Visibility = Visibility.Hidden;
            txtEmail.Visibility = Visibility.Hidden;
            cboTicket.Visibility = Visibility.Hidden;
            btnReserveren.Visibility = Visibility.Hidden;
            btnAnnuleren_Copy.Visibility = Visibility.Hidden;
        }
    }
}
