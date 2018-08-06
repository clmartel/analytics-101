using System.Collections.Generic;

namespace FootballObjectContracts
{
    public interface IConference
    {
        IDivision[] Divisions { get; }
        string Name { get; }
        Dictionary<string,int> OffensiveRankings { get; }
        Dictionary<string, int> DefensiveRankings { get; }

        double WinPercentage(ITeam team);
        int NetPoints(ITeam team);

    }
}
