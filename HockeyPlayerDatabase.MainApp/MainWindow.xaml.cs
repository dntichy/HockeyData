using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HockeyContext Context { get; }
        public ObservableCollection<Object> Players { get; }

        public MainWindow()
        {
            InitializeComponent();
            Context = new HockeyContext();

            Players = new ObservableCollection<Object>(
                Context
                    .GetPlayers()
                    .Select(
                        n => new {n.KrpId, n.FirstName, n.LastName, n.YearOfBirth, n.AgeCategory, n.ClubId})
                    .ToArray());
            DataContext = this;
        }

        private void ApplyClicked(object sender, RoutedEventArgs e)
        {
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenClubURLClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExportToXMLClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}