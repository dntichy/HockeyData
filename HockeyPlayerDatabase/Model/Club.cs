﻿using System.Data.Entity;
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
        public virtual DbSet<Player> Players { get; set; }

        public override string ToString()
        {
            return Name + " " + "["+Address+"]";
        }
    }
}
