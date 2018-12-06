using System.Collections.Generic;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase
{
    public class WrapperSerializingData
    {
        public List<Player> Players;
        public List<Club> Clubs;

        public WrapperSerializingData()
        {
        }

        public WrapperSerializingData(List<Player> players, List<Club> clubs)
        {
            Players = players;
            Clubs = clubs;
        }
    }
}
