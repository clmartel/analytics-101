using FootballObjectContracts;
using System;
using System.Collections.Generic;

namespace FootballObjects
{
    public class Conference : IConference
    {
        public string Name { get; }
        public IDivision[] Divisions { get; }

        Dictionary<string, int> IConference.OffensiveRankings => throw new NotImplementedException();

        Dictionary<string, int> IConference.DefensiveRankings => throw new NotImplementedException();

        public Conference(string name, IDivision[] divisions)
        {
            this.Name = name;
            this.Divisions = divisions;
        }


        private ITeam[] GetAllTeams()
        {
            ITeam[] teams = new ITeam[0];
            foreach (IDivision div in this.Divisions)
            {
                foreach (ITeam team in div.Teams)
                {
                    Array.Resize(ref teams, teams.Length + 1);
                    teams[teams.Length - 1] = team;
                }
            }
            return teams;
        }


        public override string ToString()
        {
            return this.Name;
        }

        public double WinPercentage(ITeam team)
        {
            throw new NotImplementedException();
        }

        public int NetPoints(ITeam team)
        {
            throw new NotImplementedException();
        }
    }
}
