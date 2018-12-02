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
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HockeyContext context = new HockeyContext();
            
            Player p = new Player();
            p.Id = 1;
            p.FirstName = "fweqfwefx";
            p.LastName = "asdfasfasd";
            context.Players.Add(p);
            int i = context.SaveChanges();
            
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
