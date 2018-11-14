using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLScraper.NFLLeagueStructure
{
    [Serializable]
    public class NFLLeague
    {
        public int Year { get; set; }
        public NFLConference[] Conferences { get; set; }

        internal NFLLeague() { }
        public NFLLeague(int year)
        {
            this.Year = year;
            this.Conferences = new NFLConference[]
            {
                new NFLConference("NFC", new NFLDivision[]
                    {
                        new NFLDivision("North", new NFLTeam[]
                            {
                                new NFLTeam("Chicago", "Bears", "CHI"),
                                new NFLTeam("Detroit", "Lions", "DET"),
                                new NFLTeam("Minnesota", "Vikings", "MIN"),
                                new NFLTeam("Green Bay", "Packers", "GB")
                            }),
                        new NFLDivision("East", new NFLTeam[]
                            {
                                new NFLTeam("Dallas", "Cowboys", "DAL"),
                                new NFLTeam("New York", "Giants", "NYG"),
                                new NFLTeam("Philadelphia", "Eagles", "PHI"),
                                new NFLTeam("Washington", "Redskins", "WAS")
                            }),
                        new NFLDivision("South", new NFLTeam[]
                            {
                                new NFLTeam("Atlanta", "Falcons", "ATL"),
                                new NFLTeam("Carolina", "Panthers", "CAR"),
                                new NFLTeam("New Orleans", "Saints", "NO"),
                                new NFLTeam("Tampa Bay", "Buccaneers", "TB")
                            }),
                        new NFLDivision("West", new NFLTeam[]
                            {
                                new NFLTeam("Arizona", "Cardinals", "ARI"),
                                new NFLTeam("Los Angeles", "Rams", "LA"),
                                new NFLTeam("Seattle", "Seahawks", "SEA"),
                                new NFLTeam("San Francisco", "49ers", "SF")
                            })
                    }),
                new NFLConference("AFC", new NFLDivision[]
                    {
                        new NFLDivision("North", new NFLTeam[]
                            {
                                new NFLTeam("Baltimore", "Ravens", "BAL"),
                                new NFLTeam("Cleveland", "Browns", "CLE"),
                                new NFLTeam("Cincinatti", "Bengals", "CIN"),
                                new NFLTeam("Pittsburgh", "Steelers", "PIT")
                            }),
                        new NFLDivision("East", new NFLTeam[]
                            {
                                new NFLTeam("Buffalo", "Bills", "BUF"),
                                new NFLTeam("Miami", "Dolphins", "MIA"),
                                new NFLTeam("New England", "Patriots", "NE"),
                                new NFLTeam("New York", "Jets", "NYJ")
                            }),
                        new NFLDivision("South", new NFLTeam[]
                            {
                                new NFLTeam("Houston", "Texans", "HOU"),
                                new NFLTeam("Indianapolis", "Colts", "IND"),
                                new NFLTeam("Jacksonville", "Jaguars", "JAX"),
                                new NFLTeam("Tennessee", "Titans", "TEN")
                            }),
                        new NFLDivision("West", new NFLTeam[]
                            {
                                new NFLTeam("Denver", "Broncos", "DEN"),
                                new NFLTeam("Kansas City", "Chiefs", "KC"),
                                new NFLTeam("Los Angeles", "Chargers", "LAC"),
                                new NFLTeam("Oakland", "Raiders", "OAK")
                            })
                    })
            };
        }
    }
}
