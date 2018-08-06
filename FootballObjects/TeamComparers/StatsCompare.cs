using FootballObjectContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FootballObjects.TeamComparers
{
    /*     
     * This namespace contains the logic for executing
     * the NFL tiebreaker rules given two teams.
     * 
     * Do not change this code!
     * 
     * DivisionCompare performs the comparison for 
     * division tiebreaker rules
     * 
     * ConferenceCompare performs the comparison for
     * conference / wildcard tiebreaker rules
     * 
     */

    public class DivisionCompare : IComparer<ITeam>
    {
        private ILeague league;
        public Hashtable TiebreakDescription;

        private IDivision GetDivision(ITeam team)
        {
            return league.Conferences.SelectMany(c => c.Divisions).Where(d => d.Teams.Contains(team)).FirstOrDefault();
        }

        public DivisionCompare(ILeague league)
        {
            this.league = league;
            TiebreakDescription = new Hashtable();
        }

        public int Compare(ITeam x, ITeam y)
        {
            IDivision divx = GetDivision(x);
            IDivision divy = GetDivision(y);
            if (divx != divy)
            {
                throw new Exception("Cannot compare teams from different divisions");
            }

            IConference conf = league.Conferences.Where(c => c.Divisions.Contains(divx)).FirstOrDefault();
            int result = 0;

            // base win percentage
            result = x.WinPercentage.CompareTo(y.WinPercentage);
            TiebreakDescription[x.ID] = "Win Percentage";
            if (result != 0)
                return result;

            // head to head
            result = x.HeadToHead(y.ID);
            TiebreakDescription[x.ID] = "Head to Head";
            if (result != 0)
                return result;

            // win percentage in games played within the division
            result = divx.WinPercentage(x).CompareTo(divx.WinPercentage(y));
            TiebreakDescription[x.ID] = "Division Win Percentage";
            if (divx.WinPercentage(x) == -1 || divx.WinPercentage(y) == -1) result = 0;
            if (result != 0)
                return result;

            // win percentage in common games
            result = league.CommonGameWinPercentage(x, y, 0).CompareTo(league.CommonGameWinPercentage(y, x, 0));
            TiebreakDescription[x.ID] = "Common Game Win Percentage";
            if (result != 0)
                return result;

            // win percentage in games played within the conference
            result = conf.WinPercentage(x).CompareTo(conf.WinPercentage(y));
            TiebreakDescription[x.ID] = "Conference Win Percentage";
            if (conf.WinPercentage(x) == -1 || conf.WinPercentage(y) == -1) result = 0;
            if (result != 0)
                return result;

            // strength of victory
            result = league.StrengthOfVictory(x).CompareTo(league.StrengthOfVictory(y));
            TiebreakDescription[x.ID] = "Strength of Victory";
            if (result != 0)
                return result;

            // strength of schedule
            result = league.StrengthOfSchedule(x).CompareTo(league.StrengthOfSchedule(y));
            TiebreakDescription[x.ID] = "Strength of Schedule";
            if (result != 0)
                return result;

            // best combined ranking in the conference of points scored and points allowed
            int xnetrank = conf.OffensiveRankings[x.ID] + conf.DefensiveRankings[x.ID];
            int ynetrank = conf.OffensiveRankings[y.ID] + conf.DefensiveRankings[y.ID];
            result = ynetrank.CompareTo(xnetrank); // lower is better
            TiebreakDescription[x.ID] = "Combined Conference Ranking";
            if (result != 0)
                return result;

            // best combined ranking across the nfl in points scored and points allowed
            xnetrank = league.OffensiveRankings[x.ID] + league.DefensiveRankings[x.ID];
            ynetrank = league.OffensiveRankings[y.ID] + league.DefensiveRankings[y.ID];
            result = ynetrank.CompareTo(xnetrank); // lower is better
            TiebreakDescription[x.ID] = "Combined League Ranking";
            if (result != 0)
                return result;

            // best net points in common games
            result = league.NetPointsInCommonGames(x, y).CompareTo(league.NetPointsInCommonGames(y, x));
            TiebreakDescription[x.ID] = "Net Points in Common Games";
            if (result != 0)
                return result;

            // best net points in all games
            int xnetpoints = x.PointsScored - x.PointsAllowed;
            int ynetpoints = y.PointsScored - y.PointsAllowed;
            result = xnetpoints.CompareTo(ynetpoints);
            TiebreakDescription[x.ID] = "Net Points in All Games";
            if (result != 0)
                return result;

            // best net touchdowns in all games
            int xnettds = x.TouchdownsScored - x.TouchdownsAllowed;
            int ynettds = y.TouchdownsScored - y.TouchdownsAllowed;
            TiebreakDescription[x.ID] = "Net Touchdowns";
            result = xnettds.CompareTo(ynettds);

            if (result == 0)
            {
                TiebreakDescription[x.ID] = "Coin Flip!";
            }

            return result;
        }
    }

    public class ConferenceCompare : IComparer<ITeam>
    {
        private ILeague league;
        public Hashtable TiebreakDescription;

        public ConferenceCompare(ILeague league)
        {
            this.league = league;
            TiebreakDescription = new Hashtable();
        }

        private IConference GetConference(ITeam team)
        {
            return league.Conferences.Where(c => c.Divisions.SelectMany(d => d.Teams).Contains(team)).FirstOrDefault();
        }

        public int Compare(ITeam x, ITeam y)
        {
            if (GetConference(x) != GetConference(y))
            {
                throw new Exception("Cannot compare teams from different conferences");
            }

            int result = 0;
            IConference conf = GetConference(x);

            // base win percentage
            result = x.WinPercentage.CompareTo(y.WinPercentage);
            TiebreakDescription[x.ID] = "Win Percentage";
            if (result != 0)
                return result;

            // head to head
            result = HeadToHead(x,y);
            TiebreakDescription[x.ID] = "Head to Head";
            if (result != 0)
                return result;

            // win percentage in games played within the conference
            result = conf.WinPercentage(x).CompareTo(conf.WinPercentage(y));
            TiebreakDescription[x.ID] = "Conference Win Percentage";
            if (conf.WinPercentage(x) == -1 || conf.WinPercentage(y) == -1) result = 0;
            if (result != 0)
                return result;

            // win percentage in common games (minimum of 4)
            result = league.CommonGameWinPercentage(x, y, 4).CompareTo(league.CommonGameWinPercentage(y, x, 4));
            TiebreakDescription[x.ID] = "Common Game Win Percentage";
            if (result != 0)
                return result;

            // strength of victory
            result = league.StrengthOfVictory(x).CompareTo(league.StrengthOfVictory(y));
            TiebreakDescription[x.ID] = "Strength of Victory";
            if (result != 0)
                return result;

            // strength of schedule
            result = league.StrengthOfSchedule(x).CompareTo(league.StrengthOfSchedule(y));
            TiebreakDescription[x.ID] = "Strength of Schedule";
            if (result != 0)
                return result;

            // best combined ranking in the conference of points scored and points allowed
            int xnetrank = conf.OffensiveRankings[x.ID] + conf.DefensiveRankings[x.ID];
            int ynetrank = conf.OffensiveRankings[y.ID] + conf.DefensiveRankings[y.ID];
            result = ynetrank.CompareTo(xnetrank); // lower is better
            TiebreakDescription[x.ID] = "Conference Combined Ranking";
            if (result != 0)
                return result;

            // best combined ranking across the nfl in points scored and points allowed
            xnetrank = league.OffensiveRankings[x.ID] + league.DefensiveRankings[x.ID];
            ynetrank = league.OffensiveRankings[y.ID] + league.DefensiveRankings[y.ID];
            result = ynetrank.CompareTo(xnetrank); // lower is better
            TiebreakDescription[x.ID] = "League Combined Ranking";
            if (result != 0)
                return result;

            // best net points in conference games
            result = conf.NetPoints(x).CompareTo(conf.NetPoints(y));
            TiebreakDescription[x.ID] = "Net Points in Conference Games";
            if (result != 0)
                return result;

            // best net points in all games
            int xnetpoints = x.PointsScored - x.PointsAllowed;
            int ynetpoints = y.PointsScored - y.PointsAllowed;
            result = xnetpoints.CompareTo(ynetpoints);
            TiebreakDescription[x.ID] = "Net Points in All Games";
            if (result != 0)
                return result;

            // best net touchdowns in all games
            int xnettds = x.TouchdownsScored - x.TouchdownsAllowed;
            int ynettds = y.TouchdownsScored - y.TouchdownsAllowed;
            TiebreakDescription[x.ID] = "Net Touchdowns in Conference Games";
            result = xnettds.CompareTo(ynettds);

            if (result == 0)
            {
                TiebreakDescription[x.ID] = "Coin Flip!";
            }

            return result;
        }

        private int HeadToHead(ITeam x, ITeam y)
        {
            IConference conf = GetConference(x);
            List<ITeam> tiedTeams = conf.Divisions.SelectMany(d => d.Teams).Where(t => t.WinPercentage == x.WinPercentage).ToList();
            int returnValue = 0;

            // Remove team X
            tiedTeams.Remove(x);

            // Remove any division champions from this list
            tiedTeams.RemoveAll(t => IsDivisionChampion(t));

            if (tiedTeams.Count > 2)  // If there is a 3 (or more) way tie
            {
                bool isVictor = true;
                foreach (ITeam t in tiedTeams)
                {
                    // Basically, only return 1 if team X has beaten all other teams. 
                    // if it has not, returnValue remains 0, tiebreaker continues down the list
                    if (x.HeadToHead(t.ID) != 1) isVictor = false;
                }
                if (isVictor) returnValue = 1;
            }
            else // if there is a 2 way tie
            {
                returnValue = x.HeadToHead(y.ID);
            }

            return returnValue;
        }

        private bool IsDivisionChampion(ITeam team)
        {
            IDivision div = GetConference(team).Divisions.Where(d => d.Teams.Contains(team)).FirstOrDefault();
            ITeam[] teams = div.Teams.ToArray();
            Array.Sort(teams, new DivisionCompare(league));
            return (teams[3] == team);
        }

        private List<string> GetOpponents(ITeam team)
        {
            List<string> opponents = new List<string>();
            foreach (Game g in team.Games)
            {
                if (g != null)
                {
                    if (g.HomeTeam != team.ID)
                    {
                        if (!opponents.Contains(g.HomeTeam)) opponents.Add(g.HomeTeam);
                    }
                    if (g.AwayTeam != team.ID)
                    {
                        if (!opponents.Contains(g.AwayTeam)) opponents.Add(g.AwayTeam);
                    }
                }
            }
            return opponents;
        }
    }

    public class R
    {
        public static int CoinFlip()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int x = rnd.Next();
            int y = rnd.Next();
            return y.CompareTo(x);
        }
    }
}

