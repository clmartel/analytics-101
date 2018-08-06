using System.Collections.Generic;

namespace FootballObjectContracts
{
    public interface ILeague
    {
        string Name { get; }
        IConference[] Conferences { get; }
        Dictionary<string,int> OffensiveRankings { get; }
        Dictionary<string,int> DefensiveRankings { get; }

        double CommonGameWinPercentage(ITeam team, ITeam opponent, int minGames);
        double StrengthOfVictory(ITeam team);
        double StrengthOfSchedule(ITeam team);
        int NetTouchdowns(ITeam team);

        int NetPointsInCommonGames(ITeam team, ITeam opponent);
    }
}
