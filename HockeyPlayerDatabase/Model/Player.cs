using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    public class Player : IPlayer
    {
        public Player()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; }
        public string TitleBefore { get; set; }
        public int YearOfBirth { get; set; }
        public int KrpId { get; set; }
        public AgeCategory? AgeCategory { get; set; }
        public int? ClubId { get; set; }


    }
}
