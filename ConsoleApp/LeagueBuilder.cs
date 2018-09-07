using FootballObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ConsoleApp
{
    public class LeagueBuilder : IDisposable
    {
        public LeagueBuilder()
        {
        }

        public League BuildNFLLeague(string statsFilePath, string leagueFilePath)
        {
            return BuildNFLLeague(statsFilePath, leagueFilePath, DateTime.Now);
        }

        public League BuildNFLLeague(string statsFilePath, string leagueFilePath, DateTime onDate)
        {
            XmlDocument data = new XmlDocument();
            data.Load(statsFilePath);
            List<Game> allGames = PreLoadGames(data, onDate);

            data.Load(leagueFilePath);
            var nflStructure = data["NFLLeague"]["Conferences"].ChildNodes.Cast<XmlNode>();

            League league = new League(
                "NFL",
                nflStructure.Select(c =>
                    new Conference(
                        c["ConferenceName"].InnerText,
                        c["Divisions"].Cast<XmlNode>().Select(d => new Division(
                            d["DivisionName"].InnerText,
                            d["Teams"].Cast<XmlNode>().Select(t => new Team(
                                t["City"].InnerText,
                                t["NickName"].InnerText,
                                t["ID"].InnerText,
                                allGames.Where(g => g.HomeTeam == t["ID"].InnerText || g.AwayTeam == t["ID"].InnerText).Select(g => new Game(
                                    g.Date,
                                    g.Week,
                                    g.HomeTeam,
                                    g.AwayTeam,
                                    g.HomePoints,
                                    g.AwayPoints,
                                    new GameStats(
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
                                        g.HomeStats.TimeOfPosession),
                                    new GameStats(
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
                                        g.AwayStats.TimeOfPosession)
                                )).ToArray()
                            )).ToArray()
                        )).ToArray()
                    )).ToArray());

            return league;
        }

        private Dictionary<string, GameStats[]> PreLoadStats(XmlDocument data)
        {
            Dictionary<string, GameStats[]> gameStats = new Dictionary<string, GameStats[]>();
            foreach (XmlElement stat in data["Bundle"]["Stats"].ChildNodes)
            {
                string team = stat["Team"].InnerText;
                int week = int.Parse(stat["Week"].InnerText);
                GameStats gameStat = new GameStats(
                    int.Parse(stat["Touchdowns"].InnerText),
                    int.Parse(stat["TouchdownsAllowed"].InnerText),
                    int.Parse(stat["FieldGoals"].InnerText),
                    int.Parse(stat["FirstDowns"].InnerText),
                    int.Parse(stat["PassingYards"].InnerText),
                    int.Parse(stat["RushingYards"].InnerText),
                    int.Parse(stat["Penalties"].InnerText),
                    int.Parse(stat["PenaltyYards"].InnerText),
                    int.Parse(stat["Turnovers"].InnerText),
                    int.Parse(stat["Punts"].InnerText),
                    int.Parse(stat["PuntYards"].InnerText),
                    stat["TimeOfPosession"].InnerText.ToTimeSpan()
                );

                if (!gameStats.ContainsKey(team))
                {
                    gameStats.Add(team, new GameStats[17]);
                }
                gameStats[team][week - 1] = gameStat;
            }

            return gameStats;
        }

        private List<Game> PreLoadGames(XmlDocument data, DateTime onDate)
        {
            Dictionary<string, GameStats[]> gameStats = PreLoadStats(data);

            List<Game> games = new List<Game>();
            foreach (XmlElement game in data["Bundle"]["Games"].ChildNodes)
            {
                int week = int.Parse(game["Week"].InnerText);
                string homeTeam = game["HomeTeam"].InnerText;
                string awayTeam = game["AwayTeam"].InnerText;
                DateTime date = DateTime.Parse(game["Date"].InnerText);
                if (date < onDate)
                {
                    games.Add(new Game(
                        date,
                        int.Parse(game["Week"].InnerText),
                        homeTeam,
                        awayTeam,
                        int.Parse(game["HomePoints"].InnerText),
                        int.Parse(game["AwayPoints"].InnerText),
                        gameStats[homeTeam][week - 1],
                        gameStats[awayTeam][week - 1]
                    ));
                }
            }

            return games;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}