using System;

namespace FootballObjectContracts
{
    public interface IGameStats
    {
        int Touchdowns { get; }
        int TouchdownsAllowed { get; }
        int FieldGoals { get; }
        int FirstDowns { get; }
        int PassingYards { get; }
        int RushingYards { get; }
        int Penalties { get; }
        int PenaltyYards { get; }
        int Turnovers { get; }
        int Punts { get; }
        int PuntYards { get; }
        TimeSpan TimeOfPosession { get; }
    }
}
