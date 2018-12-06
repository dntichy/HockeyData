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
        private MainWindow _parent;
        private WrapperClubPlayer _player;

        public AddWindowDialog(HockeyContext context, MainWindow parent, WrapperClubPlayer player)
        {
            this._context = context;
            this._parent = parent;
            _player = player;
            InitializeComponent();

            var clubs = context.Clubs.Select(n => n.Name);
            ClubComboBox.ItemsSource = clubs.ToArray();
            AgeCategoryComboBox.ItemsSource =
                Enum.GetValues(typeof(AgeCategory))
                    .Cast<AgeCategory
                    >(); //https://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c

            if (_player != null)
            {
                //edit pressed, set values 
                FirstName.Text = player.FirstName;
                LastName.Text = player.LastName;
                KrpId.Text = player.KrpId.ToString();
                TitleBefore.Text = player.TitleBefore;
                YearOfBirth.Text = player.YearOfBirth.ToString();
                AgeCategoryComboBox.SelectedValue = player.AgeCategory.Value;
                ClubComboBox.SelectedValue = player.ClubName;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Enum.TryParse(AgeCategoryComboBox.Text, true, out AgeCategory category);
                var clubIds = _context.Clubs.Select(n => new { n.Id, n.Name });
                var id = clubIds.Where(n => n.Name.Equals(ClubComboBox.Text)).Select(n => n.Id).First();

                if (_player != null)
                {
                    //edit pressed, set values 
                    Player player = _context.Players.Find(_player.Id);

                    player.YearOfBirth = Int32.Parse(YearOfBirth.Text);
                    player.FirstName = FirstName.Text;
                    player.LastName = LastName.Text;
                    player.TitleBefore = TitleBefore.Text;
                    player.KrpId = Int32.Parse(KrpId.Text);
                    player.AgeCategory = category;
                    player.ClubId = id;
                }
                else
                {
                    //add new item
                    if (_context.Players.Find(Int32.Parse(KrpId.Text)) != null)
                    {
                        MessageBox.Show("V databáze existuje záznam s týmto krp id", "Pridanie");
                        return;
                    }

                    Player newPlayer = new Player()
                    {
                        YearOfBirth = Int32.Parse(YearOfBirth.Text),
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        TitleBefore = TitleBefore.Text,
                        KrpId = Int32.Parse(KrpId.Text),
                        AgeCategory = category,
                        ClubId = id
                    };
                    _context.Players.Add(newPlayer);
                }

                _context.SaveChanges();
                this.Close();
                _parent.RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Prosím zadajte validné vstupy", "status");
            }
        }

        private bool ValidateFields()
        {
            bool isAgeCateg = Enum.TryParse(AgeCategoryComboBox.Text, true, out AgeCategory category);
            bool isYearNumeric = int.TryParse(YearOfBirth.Text, out int year);
            bool isKrpNumeric = int.TryParse(KrpId.Text, out int krp);
            bool isClubId = ClubComboBox.Text != "";

            return
                (FirstName.Text.Length > 0
                 && LastName.Text.Length > 0
                 && isYearNumeric && isKrpNumeric && isAgeCateg && isClubId
                 && KrpId.Text.Length > 0);
        }
    }
}