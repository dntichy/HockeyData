using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;
using NDesk.Options;


namespace HockeyPlayerDatabase.ImportDataApp

{
    partial class Program
    {
        private static HockeyContext _context = new HockeyContext();

        static void Main(string[] args)
        {
            bool clearDatabase = false;
            String clubs = null, players = null;

            //parse arguments
            var p = new OptionSet()
            {
                {"clearDatabase", v => clearDatabase = v != null},
                {"c|clubs=", v => clubs = v},
                {"p|players=", v => players = v},
            };
            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                return;
            }


            //clean if defined
            if (clearDatabase) CleanDb();

            //import data to tables
            ImportClubsIntoDb(ParseClubs(clubs));
            ImportPlayersIntoDb(ParsePlayers(players));
        }

        private static void CleanDb()
        {
            _context.Database.ExecuteSqlCommand("delete from Players");
            _context.Database.ExecuteSqlCommand("delete from Clubs");
        }

        private static Boolean ImportPlayersIntoDb(List<Player> players)
        {
            foreach (var player in players)
            {
                _context.Players.Add(player);
            }

            _context.SaveChanges();
            return true;
        }

        private static Boolean ImportClubsIntoDb(List<Club> clubs)
        {
            foreach (var club in clubs)
            {
                _context.Clubs.Add(club);
            }

            _context.SaveChanges();
            return true;
        }

        private static List<Player> ParsePlayers(string path)
        {
            List<Player> playersList = new List<Player>();

            var clubIds = _context.Clubs.Select(n => new {n.Id, n.Name});

            using (var rd = new StreamReader(path))
            {
                bool skipFirstRow = true;
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(';');
                    if (!skipFirstRow)
                    {
                        Player player = new Player
                        {
                            LastName = splits[0].Substring(0, 1).ToUpper() + splits[0].Substring(1).ToLower(),
                            FirstName = splits[1],
                            TitleBefore = splits[2],
                            YearOfBirth = Convert.ToInt32(splits[3]),
                            KrpId = Convert.ToInt32(splits[4])
                        };
                        string clubId = splits[5];
                        var id = clubIds.Where(n => n.Name.Equals(clubId)).Select(n => n.Id).First();
                        player.ClubId = id;

                        AgeCategory ageCategory = AgeCategory.Cadet;
                        switch (splits[6].ToLower())
                        {
                            case "juniori":
                            {
                                ageCategory = AgeCategory.Junior;
                                break;
                            }
                            case "dorastenci":
                            {
                                ageCategory = AgeCategory.Midgest;
                                break;
                            }
                            case "seniori":
                            {
                                ageCategory = AgeCategory.Senior;
                                break;
                            }
                            case "kadeti":
                            {
                                ageCategory = AgeCategory.Cadet;
                                break;
                            }
                        }

                        player.AgeCategory = ageCategory;
                        playersList.Add(player);
                    }

                    skipFirstRow = false;
                }
            }

            return playersList;
        }

        private static List<Club> ParseClubs(string path)
        {
            try
            {
                List<Club> clubsList = new List<Club>();

                using (var rd = new StreamReader(path))
                {
                    bool skipFirstRow = true;
                    while (!rd.EndOfStream)
                    {
                        var splits = rd.ReadLine().Split(';');
                        if (!skipFirstRow)
                        {
                            Club club = new Club {Name = splits[0], Address = splits[1], Url = splits[2]};
                            clubsList.Add(club);
                        }

                        skipFirstRow = false;
                    }
                }

                return clubsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}