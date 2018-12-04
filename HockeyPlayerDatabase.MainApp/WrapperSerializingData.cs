using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
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
