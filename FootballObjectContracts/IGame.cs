using System;

namespace FootballObjectContracts
{
    public interface IGame
    {
        DateTime Date { get; }
        int Week { get; }
        string HomeTeam { get; }
        string AwayTeam { get; }
        int HomePoints { get; }
        int AwayPoints { get; }
        string Winner { get; }
        IGameStats HomeStats { get; }
        IGameStats AwayStats { get; }
    }
}
