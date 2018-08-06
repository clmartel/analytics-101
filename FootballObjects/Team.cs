using FootballObjectContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballObjects
{
    public class Team : ITeam
    {
        public string City { get; }
        public string NickName { get; }
        public string ID { get; }
        public IGame[] Games { get; }

        public int Wins => GetWins();

        public int Losses => throw new NotImplementedException();

        public int Ties => throw new NotImplementedException();

        public int PointsScored => throw new NotImplementedException();

        public int PointsAllowed => throw new NotImplementedException();

        public int TouchdownsScored => throw new NotImplementedException();

        public int TouchdownsAllowed => throw new NotImplementedException();

        public double WinPercentage => throw new NotImplementedException();

        public Team(string city, string nickName, string id, IGame[] games)
        {
            this.City = city;
            this.NickName = nickName;
            this.ID = id;
            this.Games = games;
        }

  
        /// <summary>
        /// Returns 1 if this team won
        /// Returns -1 if this team lost
        /// Returns 0 if tie 
        /// Returns -999 if game is null
        /// </summary>
        public int WonLostTied(IGame g)
        {
            int returnValue = -999;
            if (g != null)
            {
                if (IsHomeGame(g)) // if this is the home team
                {
                    if (g.HomePoints > g.AwayPoints) returnValue = 1;
                    else if (g.HomePoints < g.AwayPoints) returnValue = -1;
                    else returnValue = 0;
                }
                else if (!IsHomeGame(g))
                {
                    if (g.AwayPoints > g.HomePoints) returnValue = 1;
                    else if (g.AwayPoints < g.HomePoints) returnValue = -1;
                    else returnValue = 0;
                }
            }
            return returnValue;
        }

        private int GetWins()
        {
            int wins = 0;
            for (int i = 0; i < Games.Length; i++)
            {
                if (WonLostTied(Games[i]) == 1) wins++;
            }
            return wins;
        }

        // returns true if provided game is a home game for this team
        private bool IsHomeGame(IGame game)
        {
            return game.HomeTeam == this.ID;
        }
        
        public override string ToString()
        {
            return String.Format("{0} {1}", this.City, this.NickName);
        }

        public int HeadToHead(string opponentID)
        {
            throw new NotImplementedException();
        }
    }
}
