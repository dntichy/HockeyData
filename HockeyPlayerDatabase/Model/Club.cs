using System.Collections.Generic;
using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    public class Club : IClub
    {
        public Club()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
    }
}
