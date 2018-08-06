using FootballObjectContracts;
using System;

namespace FootballObjects
{
    public class Game : IGame
    {
        public DateTime Date { get; }
        public int Week { get; }
        public string HomeTeam { get; }
        public string AwayTeam { get; }
        public int HomePoints { get; }
        public int AwayPoints { get; }
        public string Winner => GetWinner();
        public IGameStats HomeStats { get; }
        public IGameStats AwayStats { get; }

        public Game(DateTime date, int week, string homeTeam, string awayTeam, int homePoints, int awayPoints, IGameStats homeStats, IGameStats awayStats)
        {
            this.Date = date;
            this.Week = week;
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.HomePoints = homePoints;
            this.AwayPoints = awayPoints;
            this.HomeStats = homeStats;
            this.AwayStats = awayStats;
        }

        private string GetWinner()
        {
            if (this.HomePoints == this.AwayPoints)
                return null;

            return (this.HomePoints > this.AwayPoints) ? this.HomeTeam : this.AwayTeam;
        }
    }
}
