using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyPlayerDatabase.Model;
using NDesk.Options;
using Excel = Microsoft.Office.Interop.Excel;


namespace HockeyPlayerDatabase.ImportDataApp

{
    partial class Program
    {
        static void Main(string[] args)
        {
            bool clearDatabase = false;
            String clubs = null, players = null;

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

            if (clearDatabase) CleanDb();
            List<Player> playersList = ParsePlayers(players);
            List<Club> clubsList = ParseClubs(clubs);
        }

        private static void CleanDb()
        {
            //TODO how to reference context?
        }

        private static Boolean CreateTables()
        {
            return false;
        }

        private static Boolean ImportIntoDb()
        {
            return false;
        }

        private static List<Player> ParsePlayers(string path)
        {
            List<Player> playersList = new List<Player>();
            using (var rd = new StreamReader(path))
            {
                bool skipFirstRow = true;
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(';');
                    if (!skipFirstRow)
                    {
                        Player player = new Player();
                        player.LastName = splits[0];
                        player.FirstName = splits[1];
                        player.TitleBefore = splits[2];
                        player.YearOfBirth = Convert.ToInt32(splits[3]);
                        player.KrpId = Convert.ToInt32(splits[4]);
                        string clubId = splits[5]; //TODO what to do with this?

                        playersList.Add(player);
                    }

                    skipFirstRow = false;
                }
            }

            return playersList;
        }

        private static List<Club> ParseClubs(string path)
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
                        Club club = new Club();
                        club.Name = splits[0];
                        club.Address = splits[1];
                        club.Url = splits[2];
                        clubsList.Add(club);
                    }

                    skipFirstRow = false;
                }
            }

            return clubsList;
        }
    }
}