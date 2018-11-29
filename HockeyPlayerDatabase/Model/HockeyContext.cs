using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    class HockeyContext : DbContext, IHockeyReport<Club, Player>
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Club> Clubs { get; set; }

        public HockeyContext() : base("name=myContext")
        {
        }

        public HockeyContext(string nameOrConnectionString) : base("name=" + nameOrConnectionString)
        {
        }

        public IEnumerable<int> GetBiggestClubPlayerAges()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Club> GetClubs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGrouping<int, Player>> GetGroupedPlayersByYearOfBirth()
        {
            throw new NotImplementedException();
        }

        public Player GetOldestPlayer()
        {
            throw new NotImplementedException();
        }

        public double GetPlayerAverageAge()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Player> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetPlayersByAge(int minAge, int maxAge)
        {
            throw new NotImplementedException();
        }

        public IDictionary<AgeCategory, ReportResult> GetReportByAgeCategory()
        {
            throw new NotImplementedException();
        }

        public ReportResult GetReportByClub(int clubId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Club> GetSortedClubs(int maxResultCount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetSortedPlayers(int maxResultCount)
        {
            throw new NotImplementedException();
        }

        public Player GetYoungestPlayer()
        {
            throw new NotImplementedException();
        }

        public void SaveToXml(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}