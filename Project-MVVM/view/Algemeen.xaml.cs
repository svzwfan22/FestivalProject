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
    /// Interaction logic for PartOne.xaml
    /// </summary>
    public partial class Algemeen : UserControl
    {
        public Algemeen()
        {
            InitializeComponent();
        }
        private void StageToevoegenClick(object sender, RoutedEventArgs e)
        {
            lblStageNaam.Visibility = Visibility.Visible;
            //txtStage.Visibility = Visibility.Visible;
            btnStageAnnuleren.Visibility = Visibility.Visible;
            btnStageOpslaan.Visibility = Visibility.Visible;
        }

        private void StageBewerkenClick(object sender, RoutedEventArgs e)
        {
            //var newWindow = new Stage();
            //newWindow.Show();
        }

        private void ContactpersoonTypeToevoegenClick(object sender, RoutedEventArgs e)
        {
            //lblContacttypeNaam.Visibility = Visibility.Visible;
            //txtContacttype.Visibility = Visibility.Visible;
            btnContacttypeAnnuleren.Visibility = Visibility.Visible;
            //btnContacttypeOpslaan.Visibility = Visibility.Visible;
        }

        private void GenreToevoegenClick(object sender, RoutedEventArgs e)
        {
            lblGenreNaam.Visibility = Visibility.Visible;
            //txtGenre.Visibility = Visibility.Visible;
            btnGenreAnnuleren.Visibility = Visibility.Visible;
            btnGenreOpslaan.Visibility = Visibility.Visible;
        }

        private void AlgemeenLoaded(object sender, RoutedEventArgs e)
        {
            lblStageNaam.Visibility = Visibility.Hidden;
           // txtStage.Visibility = Visibility.Hidden;
            btnStageAnnuleren.Visibility = Visibility.Hidden;
            btnStageOpslaan.Visibility = Visibility.Hidden;

            //lblContacttypeNaam.Visibility = Visibility.Hidden;
            //txtContacttype.Visibility = Visibility.Hidden;
            btnContacttypeAnnuleren.Visibility = Visibility.Hidden;
            //btnContacttypeOpslaan.Visibility = Visibility.Hidden;

            lblGenreNaam.Visibility = Visibility.Hidden;
            //txtGenre.Visibility = Visibility.Hidden;
            btnGenreAnnuleren.Visibility = Visibility.Hidden;
            btnGenreOpslaan.Visibility = Visibility.Hidden;
        }

        private void AnnulerenClick(object sender, RoutedEventArgs e)
        {
            lblStageNaam.Visibility = Visibility.Hidden;
            //txtStage.Visibility = Visibility.Hidden;
            btnStageAnnuleren.Visibility = Visibility.Hidden;
            btnStageOpslaan.Visibility = Visibility.Hidden;

            //lblContacttypeNaam.Visibility = Visibility.Hidden;
           // txtContacttype.Visibility = Visibility.Hidden;
            btnContacttypeAnnuleren.Visibility = Visibility.Hidden;
            //btnContacttypeOpslaan.Visibility = Visibility.Hidden;

            lblGenreNaam.Visibility = Visibility.Hidden;
            //txtGenre.Visibility = Visibility.Hidden;
            btnGenreAnnuleren.Visibility = Visibility.Hidden;
            btnGenreOpslaan.Visibility = Visibility.Hidden;
        }
       
    }
}
