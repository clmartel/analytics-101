using FootballObjectContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballObjects
{
    public class GameStats : IGameStats
    {
        public int Touchdowns { get; }
        public int TouchdownsAllowed { get; }
        public int FieldGoals { get; }
        public int FirstDowns { get; }
        public int PassingYards { get; }
        public int RushingYards { get; }
        public int Penalties { get; }
        public int PenaltyYards { get; }
        public int Turnovers { get; }
        public int Punts { get; }
        public int PuntYards { get; }
        public TimeSpan TimeOfPosession { get; }

        public GameStats(int touchdowns, int touchdownsAllowed, int fieldgoals, int firstDowns, int passingYards, int rushingYards, int penalties, int penaltyYards, int turnovers, int punts, int puntYards, TimeSpan timeOfPossession)
        {
            this.Touchdowns = touchdowns;
            this.TouchdownsAllowed = touchdownsAllowed;
            this.FieldGoals = fieldgoals;
            this.FirstDowns = firstDowns;
            this.PassingYards = passingYards;
            this.RushingYards = rushingYards;
            this.Penalties = penalties;
            this.PenaltyYards = penaltyYards;
            this.Turnovers = turnovers;
            this.Punts = punts;
            this.PuntYards = puntYards;
            this.TimeOfPosession = timeOfPossession;
        }
    }
}
