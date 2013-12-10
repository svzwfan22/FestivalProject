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
    /// Interaction logic for PartTwo.xaml
    /// </summary>
    public partial class Contactpersonen : UserControl
    {
        public Contactpersonen()
        {
            InitializeComponent();
        }

        private void AnnulerenClick(object sender, RoutedEventArgs e)
        {
            lblEmail.Visibility = Visibility.Hidden;
            lblNaam.Visibility = Visibility.Hidden;
            lblVoornaam.Visibility = Visibility.Hidden;
            lblType.Visibility = Visibility.Hidden;
            lblTel.Visibility = Visibility.Hidden;
            txtEmail.Visibility = Visibility.Hidden;
            txtNaam.Visibility = Visibility.Hidden;
            txtVoornaam.Visibility = Visibility.Hidden;
            txtTel.Visibility = Visibility.Hidden;
            cboType.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
            btnOpslaan.Visibility = Visibility.Hidden;
        }

        private void ContactpersoonToevoegenClick(object sender, RoutedEventArgs e)
        {
            lblEmail.Visibility = Visibility.Visible;
            lblNaam.Visibility = Visibility.Visible;
            lblVoornaam.Visibility = Visibility.Visible;
            lblType.Visibility = Visibility.Visible;
            lblTel.Visibility = Visibility.Visible;
            txtEmail.Visibility = Visibility.Visible;
            txtNaam.Visibility = Visibility.Visible;
            txtVoornaam.Visibility = Visibility.Visible;
            txtTel.Visibility = Visibility.Visible;
            cboType.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
            btnOpslaan.Visibility = Visibility.Visible;
        }

        private void ContactpersonenLoaded(object sender, RoutedEventArgs e)
        {
            lblEmail.Visibility = Visibility.Hidden;
            lblNaam.Visibility = Visibility.Hidden;
            lblVoornaam.Visibility = Visibility.Hidden;
            lblType.Visibility = Visibility.Hidden;
            lblTel.Visibility = Visibility.Hidden;
            txtEmail.Visibility = Visibility.Hidden;
            txtNaam.Visibility = Visibility.Hidden;
            txtVoornaam.Visibility = Visibility.Hidden;
            txtTel.Visibility = Visibility.Hidden;
            cboType.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
            btnOpslaan.Visibility = Visibility.Hidden;
        }
    }
}
