using FootballObjectContracts;
using System;
using System.Collections.Generic;

namespace FootballObjects
{
    public class League : ILeague
    {
        public string Name { get; }
        public IConference[] Conferences { get; }

        public Dictionary<string, int> OffensiveRankings => throw new NotImplementedException();
        public Dictionary<string, int> DefensiveRankings => throw new NotImplementedException();

        public League(string name, IConference[] conferences)
        {
            this.Name = name;
            this.Conferences = conferences;
        }


        private Team[] GetAllTeams()
        {
            Team[] teams = new Team[0];
            foreach (Conference conf in Conferences)
            {
                foreach (Division div in conf.Divisions)
                {
                    foreach (Team team in div.Teams)
                    {
                        Array.Resize(ref teams, teams.Length + 1);
                        teams[teams.Length - 1] = team;
                    }
                }
            }
            return teams;
        }

        public double CommonGameWinPercentage(ITeam team, ITeam opponent, int minGames)
        {
            throw new NotImplementedException();
        }

        public double StrengthOfVictory(ITeam team)
        {
            throw new NotImplementedException();
        }

        public double StrengthOfSchedule(ITeam team)
        {
            throw new NotImplementedException();
        }

        public int NetTouchdowns(ITeam team)
        {
            throw new NotImplementedException();
        }

        public int NetPointsInCommonGames(ITeam team, ITeam opponent)
        {
            throw new NotImplementedException();
        }
    }
}
