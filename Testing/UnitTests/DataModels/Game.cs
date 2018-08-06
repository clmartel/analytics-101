using System;

namespace Testing.UnitTests.DataModels
{
    public class Game
    {
        public DateTime Date { get; set; }
        public int Week { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomePoints { get; set; }
        public int AwayPoints { get; set; }
        public GameStat HomeStats { get; set; }
        public GameStat AwayStats { get; set; }
    }
}
