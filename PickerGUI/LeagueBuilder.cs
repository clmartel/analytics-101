using FootballObjectContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Testing.UnitTests.DataModels;

namespace PickerGUI
{
    public class LeagueBuilder 
    {
        private string _dllPath;

        private object GetClassInstance(string TypeName, object[] args)
        {
            Assembly asm = Assembly.LoadFile(_dllPath);
            Type type = asm.GetType(TypeName);
            return Activator.CreateInstance(type, args);
        }

        public LeagueBuilder(string DllPath)
        {
            _dllPath = DllPath;
        }

        public ILeague BuildNFLLeague(string statsFilePath, string leagueFilePath, int weeks = 17)
        {
            XmlDocument data = new XmlDocument();
            data.Load(statsFilePath);
            List<Game> allGames = PreLoadGames(data, weeks);

            data.Load(leagueFilePath);
            var nflStructure = data["NFLLeague"]["Conferences"].ChildNodes.Cast<XmlNode>();

            ILeague league = (ILeague)GetClassInstance("FootballObjects.League", new object[] {
                "NFL",
                nflStructure.Select(c =>
                    (IConference)GetClassInstance("FootballObjects.Conference", new object[] {
                        c["ConferenceName"].InnerText,
                        c["Divisions"].Cast<XmlNode>().Select(d => (IDivision)GetClassInstance("FootballObjects.Division", new object[] {
                            d["DivisionName"].InnerText,
                            d["Teams"].Cast<XmlNode>().Select(t => (ITeam)GetClassInstance("FootballObjects.Team", new object[] {
                                t["City"].InnerText,
                                t["NickName"].InnerText,
                                t["ID"].InnerText,
                                allGames.Where(g => g.HomeTeam == t["ID"].InnerText || g.AwayTeam == t["ID"].InnerText).Select(g => (IGame)GetClassInstance("FootballObjects.Game", new object[] {
                                    g.Date,
                                    g.Week,
                                    g.HomeTeam,
                                    g.AwayTeam,
                                    g.HomePoints,
                                    g.AwayPoints,
                                    (IGameStats)GetClassInstance("FootballObjects.GameStats", new object[] {
                                        g.HomeStats.Touchdowns,
                                        g.HomeStats.TouchdownsAllowed,
                                        g.HomeStats.FieldGoals,
                                        g.HomeStats.FirstDowns,
                                        g.HomeStats.PassingYards,
                                        g.HomeStats.RushingYards,
                                        g.HomeStats.Penalties,
                                        g.HomeStats.PenaltyYards,
                                        g.HomeStats.Turnovers,
                                        g.HomeStats.Punts,
                                        g.HomeStats.PuntYards,
                                        g.HomeStats.TimeOfPosession}),
                                    (IGameStats)GetClassInstance("FootballObjects.GameStats", new object[] {
                                        g.AwayStats.Touchdowns,
                                        g.AwayStats.TouchdownsAllowed,
                                        g.AwayStats.FieldGoals,
                                        g.AwayStats.FirstDowns,
                                        g.AwayStats.PassingYards,
                                        g.AwayStats.RushingYards,
                                        g.AwayStats.Penalties,
                                        g.AwayStats.PenaltyYards,
                                        g.AwayStats.Turnovers,
                                        g.AwayStats.Punts,
                                        g.AwayStats.PuntYards,
                                        g.AwayStats.TimeOfPosession})
                                })).ToArray()
                            })).ToArray()
                        })).ToArray()
                    })).ToArray() });
            
            return league;
        }

        private Dictionary<string, GameStat[]> PreLoadStats(XmlDocument data)
        {
            Dictionary<string, GameStat[]> gameStats = new Dictionary<string, GameStat[]>();
            foreach (XmlElement stat in data["Bundle"]["Stats"].ChildNodes)
            {
                string team = stat["Team"].InnerText;
                int week = int.Parse(stat["Week"].InnerText);
                GameStat gameStat = new GameStat()
                {
                    Touchdowns = int.Parse(stat["Touchdowns"].InnerText),
                    TouchdownsAllowed = int.Parse(stat["TouchdownsAllowed"].InnerText),
                    FieldGoals = int.Parse(stat["FieldGoals"].InnerText),
                    FirstDowns = int.Parse(stat["FirstDowns"].InnerText),
                    PassingYards = int.Parse(stat["PassingYards"].InnerText),
                    RushingYards = int.Parse(stat["RushingYards"].InnerText),
                    Penalties = int.Parse(stat["Penalties"].InnerText),
                    PenaltyYards = int.Parse(stat["PenaltyYards"].InnerText),
                    Turnovers = int.Parse(stat["Turnovers"].InnerText),
                    Punts = int.Parse(stat["Punts"].InnerText),
                    PuntYards = int.Parse(stat["PuntYards"].InnerText),
                    TimeOfPosession = stat["TimeOfPosession"].InnerText.ToTimeSpan()
                };

                if (!gameStats.ContainsKey(team))
                {
                    gameStats.Add(team, new GameStat[17]);
                }
                gameStats[team][week - 1] = gameStat;
            }

            return gameStats;
        }

        private List<Game> PreLoadGames(XmlDocument data, int weeks)
        {
            Dictionary<string, GameStat[]> gameStats = PreLoadStats(data);

            List<Game> games = new List<Game>();
            foreach (XmlElement game in data["Bundle"]["Games"].ChildNodes)
            {
                int week = int.Parse(game["Week"].InnerText);
                string homeTeam = game["HomeTeam"].InnerText;
                string awayTeam = game["AwayTeam"].InnerText;
                if (week <= weeks)
                {
                    games.Add(new Game()
                    {
                        Date = DateTime.Parse(game["Date"].InnerText),
                        Week = int.Parse(game["Week"].InnerText),
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        HomePoints = int.Parse(game["HomePoints"].InnerText),
                        AwayPoints = int.Parse(game["AwayPoints"].InnerText),
                        HomeStats = gameStats[homeTeam][week-1],
                        AwayStats = gameStats[awayTeam][week-1]
                    });
                }
            }

            return games;
        }
    }
}
