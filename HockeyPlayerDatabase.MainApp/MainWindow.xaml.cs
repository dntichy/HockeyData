using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HockeyContext Context { get; }
        public ObservableCollection<WrapperClubPlayer> Players { get; }

        public MainWindow()
        {
            InitializeComponent();
            Context = new HockeyContext();

            var Data =
                (from players in Context.Players
                    join clubs in Context.Clubs on players.ClubId equals clubs.Id
                    select new {players, clubs}).ToArray();


            Players = new ObservableCollection<WrapperClubPlayer>(
                Data
                    .Select(
                        n =>
                            new WrapperClubPlayer()
                            {
                                KrpId = n.players.KrpId,
                                FirstName = n.players.FirstName,
                                LastName = n.players.LastName,
                                YearOfBirth = n.players.YearOfBirth,
                                AgeCategory = n.players.AgeCategory,
                                ClubName = n.clubs.Name,
                                Url = n.clubs.Url
                            }
                    )
                    .ToArray());


            DataContext = this;
        }

        private void ApplyClicked(object sender, RoutedEventArgs e)
        {
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            AddWindowDialog window = new AddWindowDialog(Context);
            window.DataContext = this;
            window.Show();
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenClubUrlClicked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var a = (WrapperClubPlayer) DataGrid.SelectedItem;
                if (a.Url != "") System.Diagnostics.Process.Start(a.Url);
            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            //https://stackoverflow.com/questions/2820357/how-do-i-exit-a-wpf-application-programmatically
        }

        private void ExportToXMLClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}