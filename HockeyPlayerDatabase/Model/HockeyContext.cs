using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return Players.Select(n => n).Average(n => DateTime.Now.Year - n.YearOfBirth);
            //https://stackoverflow.com/questions/4694352/using-only-the-year-part-of-a-date-for-a-where-condition
        }

        public IQueryable<Player> GetPlayers()
        {
            return Players.Select(n => n);
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
            var dictionary = new Dictionary<AgeCategory, ReportResult>();

            foreach (var category in Enum.GetValues(typeof(AgeCategory)).Cast<AgeCategory>())
            {
                var players = Players.Where(p => p.AgeCategory == category);
                dictionary.Add(category, GetReportResult(players));
            }

            return dictionary;
        }

        public ReportResult GetReportByClub(int clubId)
        {
            var players = Players.Where(n => n.ClubId == clubId);
            return GetReportResult(players);
        }

        public IEnumerable<Club> GetSortedClubs(int maxResultCount)
        {
            return Clubs.OrderByDescending(m => Players.Count(n => n.ClubId == m.Id)).Take(maxResultCount);
        }

        public IEnumerable<Player> GetSortedPlayers(int maxResultCount)
        {
            return Players.OrderBy(n => n.LastName).ThenByDescending(n => n.FirstName).Take(maxResultCount);
        }

        public Player GetYoungestPlayer()
        {
            return Players.Select(n => n).OrderByDescending(n => n.YearOfBirth).First();
        }

        public void SaveToXml(string fileName)
        {
            throw new NotImplementedException();
        }

        private ReportResult GetReportResult(IQueryable<Player> players)
        {
            var average = players.Average(n => DateTime.Now.Year - n.YearOfBirth);
            var youngestPlayer = players.OrderByDescending(n => n.YearOfBirth).First();
            var oldestPlayer = players.OrderBy(n => n.YearOfBirth).First();

            return new ReportResult(players.Count(), average,
                youngestPlayer.FullName, oldestPlayer.FullName,
                DateTime.Now.Year - youngestPlayer.YearOfBirth, DateTime.Now.Year - oldestPlayer.YearOfBirth);
        }
    }
}