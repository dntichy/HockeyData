using System;
using System.Collections.Generic;
using System.Linq;
using HockeyPlayerDatabase.Model;

namespace TestAppLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            HockeyContext context = new HockeyContext();
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
