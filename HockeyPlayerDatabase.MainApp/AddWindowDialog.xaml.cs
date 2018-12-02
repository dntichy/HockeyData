using System;
using System.Linq;
using System.Windows;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for AddWindowDialog.xaml
    /// </summary>
    public partial class AddWindowDialog : Window
    {
        private readonly HockeyContext _context;

        public AddWindowDialog(HockeyContext context)
        {
            this._context = context;
            InitializeComponent();

            var clubs = context.Clubs.Select(n => n.Name);
            ClubComboBox.ItemsSource = clubs.ToArray();
            AgeCategoryComboBox.ItemsSource = Enum.GetValues(typeof(AgeCategory)).Cast<AgeCategory>();
            //https://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text.Length > 0
                && LastName.Text.Length > 0
                && Int32.Parse(YearOfBirth.Text) > 1000
                && AgeCategoryComboBox.Text.Length > 0
                && ClubComboBox.Text.Length > 0
                && TitleBefore.Text.Length > 0
                && KrpId.Text.Length > 0)
            {
                Enum.TryParse(AgeCategoryComboBox.Text, true, out AgeCategory category);
                var clubIds = _context.Clubs.Select(n => new {n.Id, n.Name});
                var id = clubIds.Where(n => n.Name.Equals(ClubComboBox.Text)).Select(n => n.Id).First();

                Player p = new Player()
                {
                    YearOfBirth = Int32.Parse(YearOfBirth.Text),
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    TitleBefore = TitleBefore.Text,
                    KrpId = Int32.Parse(KrpId.Text),
                    AgeCategory = category,
                    ClubId = id
                };
                _context.Players.Add(p);
                _context.SaveChanges();
                this.Close();
            }
        }
    }
}