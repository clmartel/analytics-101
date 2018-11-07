using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballObjectContracts;
using FootballObjects;

namespace PredictorConsole
{
    class Program
    {
        static League NFL;
        static LeagueBuilder lb;
        const string DATAPATH = "../../../ConsoleApp/NFLData/";
        static DateTime START_DATE = new DateTime(2018,9,4);

        public static void Main(string[] args)
        {
            lb = new LeagueBuilder();
            NFL = lb.BuildNFLLeague($"{DATAPATH}NFLStats_2018.xml", $"{DATAPATH}/NFL_2018.xml", DateTime.MaxValue);

            Console.Write("Enter the week to test: ");
            string strWeek = Console.ReadLine();
            Console.WriteLine();

            int week;
            
            if (!int.TryParse(strWeek, out week))
            {
                if (strWeek.StartsWith("q"))
                    return;
                else if (strWeek == "all")
                {
                    int lastweek = (DateTime.Now - START_DATE).Days / 7;
                    double avg = 0; int weeks = 0;
                    for (int i = 1; i <= lastweek; i++) {
                        double record = ShowWeek(i);
                        if (!double.IsNaN(record))
                        {
                            avg += record; weeks++;
                        }
                    }
                    Console.WriteLine(avg / weeks);
                }
                /*
                else if (strWeek == "all bias")
                {
                    for (int bias = -10; bias < 11; bias++)
                    {
                        double avg = 0; int weeks = 0;
                        TextWriter stdOut = Console.Out;
                        Console.SetOut(TextWriter.Null);
                        for (int i = 1; i < 18; i++)
                        {
                            double record = ShowWeek(i, bias);
                            if (!double.IsNaN(record))
                            {
                                avg += record; weeks++;
                            }
                            Console.SetOut(TextWriter.Null);
                        }
                        Console.SetOut(stdOut);
                        Console.WriteLine(bias + ": " + avg / weeks);
                    }
                }*/
                else
                    Console.WriteLine("Not a valid number");
            }
            else
            {
                ShowWeek(week);
            }

            Console.WriteLine();
            Main(null);
        }


        private static double ShowWeek(int week, int bias = int.MinValue)
        {
            int correctPredictions = 0;

            League NFLPreviousWeek = lb.BuildNFLLeague($"{DATAPATH}NFLStats_2018.xml", $"{DATAPATH}NFL_2018.xml", START_DATE.AddDays((week - 1) * 7));

            List<ITeam> allTeams = NFL.Conferences.SelectMany(c => c.Divisions).SelectMany(d => d.Teams).ToList();
            List<IGame> weekGames = new List<IGame>();

            foreach(Team t in allTeams)
            {
                foreach (Game g in t.Games)
                {
                    if (!weekGames.Exists(gg => gg.HomeTeam == g.HomeTeam)) {
                        if (g.Week == week)
                            weekGames.Add(g);
                    }
                }
            }

            foreach(Game g in weekGames)
            {
                ITeam hometeam = NFLPreviousWeek.GetTeam(g.HomeTeam);
                ITeam awayteam = NFLPreviousWeek.GetTeam(g.AwayTeam);
                Predictor P = new Predictor(NFL);
                int prediction = P.Predict(hometeam, awayteam);

                string predictedWinner;
                if (prediction > 0)
                    predictedWinner = hometeam.NickName;
                else if (prediction < 0)
                    predictedWinner = awayteam.NickName;
                else
                    predictedWinner = PickRandom(hometeam, awayteam);

                if (g.Date < DateTime.Now)
                {
                    string winner;
                    if (g.AwayPoints > g.HomePoints) winner = awayteam.NickName;
                    else if (g.AwayPoints < g.HomePoints) winner = hometeam.NickName;
                    else winner = "TIE";

                    string correct = (winner == predictedWinner) ? "YES" : "NO ";
                    if (correct == "YES") correctPredictions++;
                    Console.WriteLine("{4}: {0} {2} at {1} {3}", awayteam.NickName, hometeam.NickName, g.AwayPoints, g.HomePoints, correct);
                }
                else
                {
                    Console.WriteLine("{0,10} at {1,10}: {2}", awayteam.NickName, hometeam.NickName, predictedWinner);
                }
            }
            if (correctPredictions > 0)
                Console.WriteLine("\nCorrectly predicted {0} out of {1} games", correctPredictions, weekGames.Count);

            return (correctPredictions * 1.0 / weekGames.Count);
        }

        private static string PickRandom(ITeam team1, ITeam team2)
        {
            return (new Random(DateTime.Now.Millisecond).Next(0, 10000) > 5000) ? team1.NickName : team2.NickName;
        }
    }
}
