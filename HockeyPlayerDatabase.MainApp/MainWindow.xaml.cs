using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HockeyContext Context { get; }
        public ObservableCollection<WrapperClubPlayer> Players { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Context = new HockeyContext();

            RefreshDataGrid();
            DataContext = this;
        }

        private void ApplyClicked(object sender, RoutedEventArgs e)
        {
           RefreshDataGrid();
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            AddWindowDialog window = new AddWindowDialog(Context, this, null);
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var row = (WrapperClubPlayer) DataGrid.SelectedItem;
                List<Player> p = Context.Players.Select(n => n).Where(n => n.Id == row.Id).ToList();
                Context.Players.RemoveRange(p);
                Context.SaveChanges();
                RefreshDataGrid();
            }
            else MessageBox.Show("Zvoľte riadok na vymazanie", "Zmazanie");
        }

        internal void RefreshDataGrid()
        {
            //DataGrid.ItemsSource = Players;
            //https://stackoverflow.com/questions/11324688/how-to-refresh-datagrid-in-wpf

            var Data =
               (from players in Context.Players
                join clubs in Context.Clubs on players.ClubId equals clubs.Id
                select new { players, clubs }).ToArray();

            Players = new ObservableCollection<WrapperClubPlayer>(
                Data
                    .Select(
                        n =>
                            new WrapperClubPlayer()
                            {
                                Id = n.players.Id,
                                ClubId = n.clubs.Id,
                                KrpId = n.players.KrpId,
                                FirstName = n.players.FirstName,
                                LastName = n.players.LastName,
                                YearOfBirth = n.players.YearOfBirth,
                                AgeCategory = n.players.AgeCategory,
                                ClubName = n.clubs.Name,
                                Url = n.clubs.Url,
                                TitleBefore = n.players.TitleBefore

                            }
                    )
                    .Where(n => n.ClubName.ToLower().Contains(ClubTextBox.Text.ToLower()))
                    .Where(n => n.FirstName.ToLower().Contains(FirstNameTextBox.Text.ToLower()))
                    .Where(n => n.LastName.ToLower().Contains(LastNameTextBox.Text.ToLower()))
                    .Where(n => n.KrpId.ToString().Contains(KrpIdTextBox.Text))
                    .Where(n =>
                    {
                        bool parsedFrom = int.TryParse(YearFromTextBox.Text, out int yearFrom);
                        return parsedFrom ? (n.YearOfBirth >= yearFrom) : (n.YearOfBirth >= 0);
                    })
                    .Where(n =>
                    {
                        bool parsedTo = int.TryParse(YearToTextBox.Text, out int yearTo);
                        return parsedTo ? (n.YearOfBirth <= yearTo) : n.YearOfBirth < int.MaxValue;
                    })
                    .Where(n =>
                    {
                        List<AgeCategory> list = new List<AgeCategory>();
                        if (CadetCheckBox.IsChecked == true) list.Add(AgeCategory.Cadet);
                        if (JuniorCheckBox.IsChecked == true) list.Add(AgeCategory.Junior);
                        if (SeniorCheckBox.IsChecked == true) list.Add(AgeCategory.Senior);
                        if (MidgesCheckBox.IsChecked == true) list.Add(AgeCategory.Midgest);
                        if (list.Count == 0) return true;
                        else return list.Contains(n.AgeCategory.Value);
                    })
                    .ToArray());

            DataGrid.ItemsSource = Players;
            FilteredItemsLabel.Content = "Filtered items: " + Players.Count + " / " + Data.Length;
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var row = (WrapperClubPlayer) DataGrid.SelectedItem;

                AddWindowDialog window = new AddWindowDialog(Context, this, row);
                window.Owner = App.Current.MainWindow;
                window.ShowDialog();
            }
            else MessageBox.Show("Zvoľte riadok na editáciu", "Editácia");
        }

        private void OpenClubUrlClicked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var a = (WrapperClubPlayer) DataGrid.SelectedItem;
                if (a.Url != "") System.Diagnostics.Process.Start(a.Url);
                else MessageBox.Show("URL pre klub nie je definovaná", "URL");
            }
            else MessageBox.Show("Zvoľte klub, ktorého stránku chcete otvoriť", "URL");
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown(); //https://stackoverflow.com/questions/2820357/how-do-i-exit-a-wpf-application-programmatically
        }

        private void ExportToXMLClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}