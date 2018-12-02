using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.Model
{
    public class HockeyContext : DbContext, IHockeyReport<Club, Player>
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
            return Clubs;
        }

        public IEnumerable<IGrouping<int, Player>> GetGroupedPlayersByYearOfBirth()
        {
            return Players.Select(n => n).GroupBy(n => n.YearOfBirth);
        }

        public Player GetOldestPlayer()
        {
            return Players.Select(n => n).OrderBy(n => n.YearOfBirth).First();
        }

        public double GetPlayerAverageAge()
        {
            return Players.Select(n => n).Average(n =>
                DateTime.Parse(DateTime.Now.ToString()).Year - n.YearOfBirth);
        }

        public IQueryable<Player> GetPlayers()
        {
            return Players.Select(n=>n);
        }

        public IEnumerable<Player> GetPlayersByAge(int minAge, int maxAge)
        {
            return Players
                .Where(n => DateTime.Parse(DateTime.Now.ToString()).Year - n.YearOfBirth > minAge
                            && DateTime.Parse(DateTime.Now.ToString()).Year - n.YearOfBirth < maxAge)
                .Select(n => n);
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
            return Clubs.Select(n => n).OrderBy(n => n.Name).Take(maxResultCount);
        }

        public IEnumerable<Player> GetSortedPlayers(int maxResultCount)
        {
            return Players.Select(n => n).OrderBy(n => n.FullName).Take(maxResultCount);
        }

        public Player GetYoungestPlayer()
        {
            return Players.Select(n => n).OrderByDescending(n => n.YearOfBirth).First();
        }

        public void SaveToXml(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}