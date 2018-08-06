using System;

namespace Testing.UnitTests.DataModels
{
    public class GameStat
    {
        public int Touchdowns { get; set; }
        public int TouchdownsAllowed { get; set; }
        public int FieldGoals { get; set; }
        public int FirstDowns { get; set; }
        public int PassingYards { get; set; }
        public int RushingYards { get; set; }
        public int Penalties { get; set; }
        public int PenaltyYards { get; set; }
        public int Turnovers { get; set; }
        public int Punts { get; set; }
        public int PuntYards { get; set; }
        public TimeSpan TimeOfPosession { get; set; }
    }
}
