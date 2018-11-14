using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using NFLScraper.NFLLeagueStructure;

namespace NFLScraper
{
    [Serializable]
    public class ScraperGame
    {
        public DateTime Date;
        public int Week;
        public string HomeTeam;
        public string AwayTeam;
        public int HomePoints;
        public int AwayPoints;

        public ScraperGame() { HomePoints = -1; AwayPoints = -1; }
    }

    [Serializable]
    public class ScraperStat
    {
        public string Team;
        public int Week;
        public int Touchdowns;
        public int TouchdownsAllowed;
        public int FieldGoals;
        public int FirstDowns;
        public int PassingYards;
        public int RushingYards;
        public int Penalties;
        public int PenaltyYards;
        public int Turnovers;
        public int Punts;
        public int PuntYards;
        public string TimeOfPosession;

        public ScraperStat() { }
    }

    [Serializable]
    public class Bundle
    {
        public List<ScraperGame> Games;
        public List<ScraperStat> Stats;
    }

    class Program
    {
        const string NFLScoreStrip = "http://www.nfl.com/ajax/scorestrip?season={0}&seasonType={1}&week={2}";
        const string NFLGameStats = "http://www.nfl.com/liveupdate/game-center/{0}/{0}_gtd.json";
        const int YEAR = 2018;

        static List<ScraperGame> games = new List<ScraperGame>();
        static List<ScraperStat> stats = new List<ScraperStat>();

        static ScraperStat LoadTeamStats(dynamic s, int week)
        {
            ScraperStat Stat = new ScraperStat();
            Stat.Touchdowns = 0;
            Stat.TouchdownsAllowed =0;
            Stat.FieldGoals = 0;
            Stat.Week = week;
            Stat.Team            = s.abbr;
            Stat.FirstDowns      = s.stats.team.totfd;
            Stat.PassingYards    = s.stats.team.pyds;
            Stat.Penalties       = s.stats.team.pen;
            Stat.PenaltyYards    = s.stats.team.penyds;
            Stat.Punts           = s.stats.team.pt;
            Stat.PuntYards       = s.stats.team.ptyds;
            Stat.RushingYards    = s.stats.team.ryds;
            Stat.TimeOfPosession = s.stats.team.top;
            Stat.Turnovers       = s.stats.team.trnovr;

            return Stat;
        }

        static void LoadTouchdowns(ScraperStat Stat, dynamic s)
        {
            foreach (dynamic d in s.scrsummary)
            {
                if (d.First.type == "TD" && d.First.team == Stat.Team)
                {
                    Stat.Touchdowns++;
                }
                if (d.First.type == "FG" && d.First.team == Stat.Team)
                {
                    Stat.FieldGoals++;
                }
            }
        }

        static void LoadGameStats(string eid, int week)
        {
            string gsUri = string.Format(NFLGameStats, eid);
            string json = new WebClient().DownloadString(gsUri);
            dynamic gs = JsonConvert.DeserializeObject(json);
            dynamic s = gs[eid];
            var HomeStat = LoadTeamStats(s.home,week);
            var AwayStat = LoadTeamStats(s.away,week);
            LoadTouchdowns(HomeStat,s);
            LoadTouchdowns(AwayStat,s);
            HomeStat.TouchdownsAllowed = AwayStat.Touchdowns;
            AwayStat.TouchdownsAllowed = HomeStat.Touchdowns;

            Console.WriteLine(gs[eid].away.abbr + " " + gs[eid].away.score.T + " at " + gs[eid].home.abbr + " " + gs[eid].home.score.T);

            stats.Add(HomeStat);
            stats.Add(AwayStat);

            System.Threading.Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            //To create a league structure, create a new instance of NFLLeague and pass in the year you want to create.  If the structure is different than what
            //is currently in the classes, you will need to manually modify the data in the NFLLeague class to match the structure of the NFL for the year you are creating.
            NFLLeague nfl = new NFLLeague(YEAR);
            FileStream fsLeagueStructure = new FileStream($"NFL_{nfl.Year}.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NFLLeague));
            xmlSerializer.Serialize(fsLeagueStructure, nfl);






            for (int i = 1; i <= 17; i++)
            {
                string ssUri = string.Format(NFLScoreStrip, YEAR, "REG", i);
                XmlDocument scoreStrip = new XmlDocument();
                scoreStrip.Load(ssUri);

                foreach (XmlElement e in scoreStrip.FirstChild.NextSibling.FirstChild)
                {
                    ScraperGame g = new ScraperGame();
                    string strDate = e.GetAttribute("eid").Substring(0, 8);
                    strDate = strDate.Insert(6, "-").Insert(4, "-") + " " + e.GetAttribute("t") + " PM";

                    g.Date = DateTime.Parse(strDate);
                    g.Week = i;
                    g.HomeTeam = e.GetAttribute("h");
                    g.AwayTeam = e.GetAttribute("v");
                    int.TryParse(e.GetAttribute("hs"), out g.HomePoints);
                    int.TryParse(e.GetAttribute("vs"), out g.AwayPoints);

                    games.Add(g);

                    if (g.Date < DateTime.Today)
                        LoadGameStats(e.GetAttribute("eid"), i);
                    else
                    {
                        ScraperStat home = new ScraperStat
                        {
                            Week = i, Team = g.HomeTeam, TimeOfPosession = "00:00"
                        };
                        ScraperStat away = new ScraperStat
                        {
                            Week = i, Team = g.AwayTeam, TimeOfPosession = "00:00"
                        };
                        stats.Add(home);
                        stats.Add(away);
                    }
                }
                Console.WriteLine("Loaded week {0}", i);
            }

            Bundle b = new Bundle();
            b.Games = games;
            b.Stats = stats;

            FileStream fs = new FileStream($"NFLStats_{nfl.Year}.xml", FileMode.Create);
            xmlSerializer = new XmlSerializer(typeof(Bundle));
            xmlSerializer.Serialize(fs, b);


            Console.ReadLine();
        }
    }
}
