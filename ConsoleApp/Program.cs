using System;
using System.Linq;
using FootballObjects;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LeagueBuilder builder = new LeagueBuilder();

            League NFL = builder.BuildNFLLeague("../../NFLData/NFLStats_2018.xml", "../../NFLData/NFL_2018.xml");

            Team[] AllTeams = new Team[0];
            foreach (Conference conf in NFL.Conferences)
            {
                foreach (Division div in conf.Divisions)
                {
                    foreach (Team team in div.Teams)
                    {
                        Array.Resize(ref AllTeams, AllTeams.Length + 1); // increase the array size by one
                        AllTeams[AllTeams.Length - 1] = team; // add the team to the last slot of the array
                    }
                }
            }

            // Sort the array of teams by name
            Array.Sort(AllTeams, delegate (Team t1, Team t2) {
                return t1.ToString().CompareTo(t2.ToString());
            });
            // Display the teams
            PrintTeams(AllTeams);

            // Sort the array of teams by win percentage
            Array.Sort(AllTeams, delegate (Team t1, Team t2) {
                return t2.WinPercentage.CompareTo(t1.WinPercentage); // t2 is first because we want descending order (highest win % first)
            });
            PrintTeams(AllTeams);

            // Sort the array of teams by points scored
            Array.Sort(AllTeams, delegate (Team t1, Team t2) {
                return t2.PointsScored.CompareTo(t1.PointsScored); // t2 is first because we want descending order (highest points scored first)
            });
            PrintTeams(AllTeams);

            // Compare New Orleans to Tampa Bay
            string teamX = "NO";
            string teamY = "TB";
            int result = NFL.GetTeam(teamX).HeadToHead(teamY);
            if (result == 1)
                Console.WriteLine("{0} has a winning record against {1}", NFL.GetTeam(teamX), NFL.GetTeam(teamY));
            else
                Console.WriteLine("{0} does not have a winning record against {1}", NFL.GetTeam(teamX), NFL.GetTeam(teamY));

            Pause();
        }

        static void PrintTeams(Team[] Teams)
        {
            foreach (Team t in Teams)
            {
                Console.WriteLine("{0} {1} have won {2} games, lost {3} games, scored {4} points and {5} touchdowns",
                    t.City.PadRight(13),
                    t.NickName.PadRight(10),
                    t.Wins,
                    t.Losses,
                    t.PointsScored,
                    t.TouchdownsScored);
            }
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("--Press Enter--");
            Console.ReadLine();
        }
    }
}
