using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyPlayerDatabase.Model;

namespace TestAppLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            HockeyContext context = new HockeyContext();

            var z = context.Clubs.OrderByDescending(c => context.Players.Count(p => p.ClubId == c.Id)).Take(5);

            var utriedenyHraci = context.Players.OrderBy(n => n.LastName).ThenByDescending(n => n.FirstName).Take(30);

            var test = context.GetGroupedPlayersByYearOfBirth();
            //IEnumerable<IGrouping<int, Player>>

            //DisplayAllLines(test);




            //Console.WriteLine(context.GetPlayerAverageAge());
            Console.ReadLine();
        }


        static void DisplayAllLines<T>(IEnumerable<T> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        static void DisplayAllLinesGrouped<T>(IEnumerable<IGrouping<int, T>> lines)
        {
            //https://stackoverflow.com/questions/9723627/how-do-i-iterate-an-igroupingt-interface
            foreach (var group in lines)
            {
                var groupKey = group.Key;
                Console.WriteLine(groupKey);

                foreach (var groupedItem in group)
                {
                    Console.WriteLine(groupedItem);
                }
            }
        }
    }
}
