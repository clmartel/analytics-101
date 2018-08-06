using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class LeagueTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_Constructor()
        {
            IConference[] conferences = this.NFL.Conferences;

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                case "NFL_2017.xml":
                    Assert.AreEqual("NFL", this.NFL.Name);
                    Assert.AreEqual(2, conferences.Count());
                    Assert.AreEqual(1, conferences.Count(c => c.Name == "NFC"));
                    Assert.AreEqual(1, conferences.Count(c => c.Name == "AFC"));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_OffensiveRank()
        {
            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(2, this.NFL.OffensiveRankings["NO"]);
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(4, this.NFL.OffensiveRankings["NO"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_DefensiveRank()
        {
            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(31, this.NFL.DefensiveRankings["NO"]);
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(10, this.NFL.DefensiveRankings["NO"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_OffensiveRankings()
        {
            Dictionary<string, int> ranks = new Dictionary<string, int>();
            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    ranks = NFL.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["ATL"]);
                    Assert.AreEqual(2,  ranks["NO" ]);
                    Assert.AreEqual(3,  ranks["NE" ]);
                    Assert.AreEqual(4,  ranks["GB" ]);
                    Assert.AreEqual(5,  ranks["DAL"]);
                    Assert.AreEqual(6,  ranks["ARI"]);
                    Assert.AreEqual(7,  ranks["OAK"]);
                    Assert.AreEqual(8,  ranks["IND"]);
                    Assert.AreEqual(9,  ranks["SD" ]);
                    Assert.AreEqual(10, ranks["BUF"]);
                    Assert.AreEqual(10, ranks["PIT"]);
                    Assert.AreEqual(12, ranks["WAS"]);
                    Assert.AreEqual(13, ranks["KC" ]);
                    Assert.AreEqual(14, ranks["TEN"]);
                    Assert.AreEqual(15, ranks["CAR"]);
                    Assert.AreEqual(16, ranks["PHI"]);
                    Assert.AreEqual(17, ranks["MIA"]);
                    Assert.AreEqual(18, ranks["SEA"]);
                    Assert.AreEqual(18, ranks["TB" ]);
                    Assert.AreEqual(20, ranks["DET"]);
                    Assert.AreEqual(21, ranks["BAL"]);
                    Assert.AreEqual(22, ranks["DEN"]);
                    Assert.AreEqual(23, ranks["MIN"]);
                    Assert.AreEqual(24, ranks["CIN"]);
                    Assert.AreEqual(25, ranks["JAX"]);
                    Assert.AreEqual(26, ranks["NYG"]);
                    Assert.AreEqual(27, ranks["SF" ]);
                    Assert.AreEqual(28, ranks["CHI"]);
                    Assert.AreEqual(28, ranks["HOU"]);
                    Assert.AreEqual(30, ranks["NYJ"]);
                    Assert.AreEqual(31, ranks["CLE"]);
                    Assert.AreEqual(32, ranks["LA" ]);
                    break;
                case "NFLStats_2017.xml":
                    ranks = NFL.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["LA" ]);
                    Assert.AreEqual(2,  ranks["NE" ]);
                    Assert.AreEqual(3,  ranks["PHI"]);
                    Assert.AreEqual(4,  ranks["NO" ]);
                    Assert.AreEqual(5,  ranks["JAX"]);
                    Assert.AreEqual(6,  ranks["KC" ]);
                    Assert.AreEqual(7,  ranks["DET"]);
                    Assert.AreEqual(8,  ranks["PIT"]);
                    Assert.AreEqual(9,  ranks["BAL"]);
                    Assert.AreEqual(10, ranks["MIN"]);
                    Assert.AreEqual(11, ranks["SEA"]);
                    Assert.AreEqual(12, ranks["CAR"]);
                    Assert.AreEqual(13, ranks["LAC"]);
                    Assert.AreEqual(14, ranks["DAL"]);
                    Assert.AreEqual(15, ranks["ATL"]);
                    Assert.AreEqual(16, ranks["WAS"]);
                    Assert.AreEqual(17, ranks["HOU"]);
                    Assert.AreEqual(18, ranks["TB" ]);
                    Assert.AreEqual(19, ranks["TEN"]);
                    Assert.AreEqual(20, ranks["SF" ]);
                    Assert.AreEqual(21, ranks["GB" ]);
                    Assert.AreEqual(22, ranks["BUF"]);
                    Assert.AreEqual(23, ranks["OAK"]);
                    Assert.AreEqual(24, ranks["NYJ"]);
                    Assert.AreEqual(25, ranks["ARI"]);
                    Assert.AreEqual(26, ranks["CIN"]);
                    Assert.AreEqual(27, ranks["DEN"]);
                    Assert.AreEqual(28, ranks["MIA"]);
                    Assert.AreEqual(29, ranks["CHI"]);
                    Assert.AreEqual(30, ranks["IND"]);
                    Assert.AreEqual(31, ranks["NYG"]);
                    Assert.AreEqual(32, ranks["CLE"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_DefensiveRankings()
        {
            Dictionary<string, int> ranks = new Dictionary<string, int>();

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    ranks = NFL.DefensiveRankings;
                    Assert.AreEqual(1,  ranks["NE" ]);
                    Assert.AreEqual(2,  ranks["NYG"]);
                    Assert.AreEqual(3,  ranks["SEA"]);
                    Assert.AreEqual(4,  ranks["DEN"]);
                    Assert.AreEqual(5,  ranks["DAL"]);
                    Assert.AreEqual(6,  ranks["MIN"]);
                    Assert.AreEqual(7,  ranks["KC" ]);
                    Assert.AreEqual(8,  ranks["CIN"]);
                    Assert.AreEqual(9,  ranks["BAL"]);
                    Assert.AreEqual(10, ranks["PIT"]);
                    Assert.AreEqual(11, ranks["HOU"]);
                    Assert.AreEqual(12, ranks["PHI"]);
                    Assert.AreEqual(13, ranks["DET"]);
                    Assert.AreEqual(14, ranks["ARI"]);
                    Assert.AreEqual(15, ranks["TB" ]);
                    Assert.AreEqual(16, ranks["BUF"]);
                    Assert.AreEqual(16, ranks["TEN"]);
                    Assert.AreEqual(18, ranks["MIA"]);
                    Assert.AreEqual(19, ranks["WAS"]);
                    Assert.AreEqual(20, ranks["OAK"]);
                    Assert.AreEqual(21, ranks["GB" ]);
                    Assert.AreEqual(22, ranks["IND"]);
                    Assert.AreEqual(23, ranks["LA" ]);
                    Assert.AreEqual(24, ranks["CHI"]);
                    Assert.AreEqual(25, ranks["JAX"]);
                    Assert.AreEqual(26, ranks["CAR"]);
                    Assert.AreEqual(27, ranks["ATL"]);
                    Assert.AreEqual(28, ranks["NYJ"]);
                    Assert.AreEqual(29, ranks["SD" ]);
                    Assert.AreEqual(30, ranks["CLE"]);
                    Assert.AreEqual(31, ranks["NO" ]);
                    Assert.AreEqual(32, ranks["SF" ]);
                    break;
                case "NFLStats_2017.xml":
                    ranks = NFL.DefensiveRankings;
                    Assert.AreEqual(1, ranks["MIN"]);
                    Assert.AreEqual(2, ranks["JAX"]);
                    Assert.AreEqual(3, ranks["LAC"]);
                    Assert.AreEqual(4, ranks["PHI"]);
                    Assert.AreEqual(5, ranks["NE" ]);
                    Assert.AreEqual(6, ranks["BAL"]);
                    Assert.AreEqual(7, ranks["PIT"]);
                    Assert.AreEqual(8, ranks["ATL"]);
                    Assert.AreEqual(9, ranks["CHI"]);
                    Assert.AreEqual(10, ranks["NO" ]);
                    Assert.AreEqual(11, ranks["CAR"]);
                    Assert.AreEqual(12, ranks["LA" ]);
                    Assert.AreEqual(13, ranks["DAL"]);
                    Assert.AreEqual(13, ranks["SEA"]);
                    Assert.AreEqual(15, ranks["KC" ]);
                    Assert.AreEqual(16, ranks["CIN"]);
                    Assert.AreEqual(17, ranks["TEN"]);
                    Assert.AreEqual(18, ranks["BUF"]);
                    Assert.AreEqual(19, ranks["ARI"]);
                    Assert.AreEqual(20, ranks["OAK"]);
                    Assert.AreEqual(21, ranks["DET"]);
                    Assert.AreEqual(22, ranks["DEN"]);
                    Assert.AreEqual(22, ranks["NYJ"]);
                    Assert.AreEqual(22, ranks["TB" ]);
                    Assert.AreEqual(25, ranks["SF" ]);
                    Assert.AreEqual(26, ranks["GB" ]);
                    Assert.AreEqual(27, ranks["WAS"]);
                    Assert.AreEqual(27, ranks["NYG"]);
                    Assert.AreEqual(29, ranks["MIA"]);
                    Assert.AreEqual(30, ranks["IND"]);
                    Assert.AreEqual(31, ranks["CLE"]);
                    Assert.AreEqual(32, ranks["HOU"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_NetTouchdowns()
        {
            ITeam saints = this.NFL.GetTeam("NO");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(4, this.NFL.NetTouchdowns(saints));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(15, this.NFL.NetTouchdowns(saints));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_CommonGameWinPercentage()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam panthers = this.NFL.GetTeam("CAR");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual((6.0 / 12.0), this.NFL.CommonGameWinPercentage(saints, panthers, 4));
                    Assert.AreEqual((4.0 / 12.0), this.NFL.CommonGameWinPercentage(panthers, saints, 4));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual((8.0 / 12.0), this.NFL.CommonGameWinPercentage(saints, panthers, 4));
                    Assert.AreEqual((10.0 / 12.0), this.NFL.CommonGameWinPercentage(panthers, saints, 4));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_CommonGameNetPoints()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam panthers = this.NFL.GetTeam("CAR");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(33, this.NFL.NetPointsInCommonGames(saints, panthers));
                    Assert.AreEqual(-32, this.NFL.NetPointsInCommonGames(panthers, saints));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(94, this.NFL.NetPointsInCommonGames(saints, panthers));
                    Assert.AreEqual(52, this.NFL.NetPointsInCommonGames(panthers, saints));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_StrengthOfVictory()
        {
            ITeam saints = this.NFL.GetTeam("NO");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual((44.0 / 112.0), this.NFL.StrengthOfVictory(saints));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual((85.0 / 176.0), this.NFL.StrengthOfVictory(saints));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void League_StrengthOfSchedule()
        {
            ITeam saints = this.NFL.GetTeam("NO");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual((134.0 / 256.0), this.NFL.StrengthOfSchedule(saints));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual((137.0 / 256.0), this.NFL.StrengthOfSchedule(saints));
                    break;
                default:
                    break;
            }
        }
    }
}
