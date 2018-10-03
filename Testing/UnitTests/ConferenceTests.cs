using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class ConferenceTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV","|DataDirectory|\\TestData\\InjectedAssemblies.csv","InjectedAssemblies#csv",DataAccessMethod.Sequential)]
        public void Conference_Constructor()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                case "NFL_2017.xml":
                    Assert.AreEqual("NFC", nfc.Name);
                    Assert.AreEqual("AFC", afc.Name);
                    Assert.AreEqual(4, nfc.Divisions.Count());
                    Assert.AreEqual(4, afc.Divisions.Count());
                    Assert.AreEqual(1, nfc.Divisions.Count(d => d.Name == "North"));
                    Assert.AreEqual(1, nfc.Divisions.Count(d => d.Name == "South"));
                    Assert.AreEqual(1, nfc.Divisions.Count(d => d.Name == "East"));
                    Assert.AreEqual(1, nfc.Divisions.Count(d => d.Name == "West"));
                    Assert.AreEqual(1, afc.Divisions.Count(d => d.Name == "North"));
                    Assert.AreEqual(1, afc.Divisions.Count(c => c.Name == "South"));
                    Assert.AreEqual(1, afc.Divisions.Count(c => c.Name == "East"));
                    Assert.AreEqual(1, afc.Divisions.Count(c => c.Name == "West"));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_WinPercentage()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual((6.0 / 12.0), nfc.WinPercentage(saints));
                    Assert.AreEqual(((6.0 + (1.0 * 0.5)) / 12.0), nfc.WinPercentage(seahawks));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual((8.0 / 12.0), nfc.WinPercentage(saints));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_NetPoints()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");
            ITeam saints = this.NFL.GetTeam("NO");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(23, nfc.NetPoints(saints));
                    //Assert.AreEqual(-8, afc.NetPoints(saints));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(69, nfc.NetPoints(saints));
                    //Assert.AreEqual(53, afc.NetPoints(saints));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_OffensiveRank()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");
            ITeam saints = this.NFL.GetTeam("NO");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(2, nfc.OffensiveRankings["NO"]);
                    Assert.ThrowsException<KeyNotFoundException>(() => afc.OffensiveRankings["NO"]);
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(3, nfc.OffensiveRankings["NO"]);
                    Assert.ThrowsException<KeyNotFoundException>(() => afc.OffensiveRankings["NO"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_DefensiveRank()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual(15, nfc.DefensiveRankings["NO"]);
                    Assert.ThrowsException<KeyNotFoundException>(() => afc.DefensiveRankings["NO"]);
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual(5, nfc.DefensiveRankings["NO"]);
                    Assert.ThrowsException<KeyNotFoundException>(() => afc.DefensiveRankings["NO"]);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_OffensiveRankings()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");
            Dictionary<string, int> ranks = new Dictionary<string, int>();

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    ranks = nfc.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["ATL"]);
                    Assert.AreEqual(2,  ranks["NO" ]);
                    Assert.AreEqual(3,  ranks["GB" ]);
                    Assert.AreEqual(4,  ranks["DAL"]);
                    Assert.AreEqual(5,  ranks["ARI"]);
                    Assert.AreEqual(6,  ranks["WAS"]);
                    Assert.AreEqual(7,  ranks["CAR"]);
                    Assert.AreEqual(8,  ranks["PHI"]);
                    Assert.AreEqual(9,  ranks["SEA"]);
                    Assert.AreEqual(9,  ranks["TB" ]);
                    Assert.AreEqual(11, ranks["DET"]);
                    Assert.AreEqual(12, ranks["MIN"]);
                    Assert.AreEqual(13, ranks["NYG"]);
                    Assert.AreEqual(14, ranks["SF" ]);
                    Assert.AreEqual(15, ranks["CHI"]);
                    Assert.AreEqual(16, ranks["LA" ]);

                    ranks = afc.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["NE" ]);
                    Assert.AreEqual(2,  ranks["OAK"]);
                    Assert.AreEqual(3,  ranks["IND"]);
                    Assert.AreEqual(4,  ranks["SD" ]);
                    Assert.AreEqual(5,  ranks["BUF"]);
                    Assert.AreEqual(5,  ranks["PIT"]);
                    Assert.AreEqual(7,  ranks["KC" ]);
                    Assert.AreEqual(8,  ranks["TEN"]);
                    Assert.AreEqual(9,  ranks["MIA"]);
                    Assert.AreEqual(10, ranks["BAL"]);
                    Assert.AreEqual(11, ranks["DEN"]);
                    Assert.AreEqual(12, ranks["CIN"]);
                    Assert.AreEqual(13, ranks["JAX"]);
                    Assert.AreEqual(14, ranks["HOU"]);
                    Assert.AreEqual(15, ranks["NYJ"]);
                    Assert.AreEqual(16, ranks["CLE"]);
                    break;
                case "NFLStats_2017.xml":
                    ranks = nfc.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["LA" ]);
                    Assert.AreEqual(2,  ranks["PHI"]);
                    Assert.AreEqual(3,  ranks["NO" ]);
                    Assert.AreEqual(4,  ranks["DET"]);
                    Assert.AreEqual(5,  ranks["MIN"]);
                    Assert.AreEqual(6,  ranks["SEA"]);
                    Assert.AreEqual(7,  ranks["CAR"]);
                    Assert.AreEqual(8,  ranks["DAL"]);
                    Assert.AreEqual(9,  ranks["ATL"]);
                    Assert.AreEqual(10, ranks["WAS"]);
                    Assert.AreEqual(11, ranks["TB" ]);
                    Assert.AreEqual(12, ranks["SF" ]);
                    Assert.AreEqual(13, ranks["GB" ]);
                    Assert.AreEqual(14, ranks["ARI"]);
                    Assert.AreEqual(15, ranks["CHI"]);
                    Assert.AreEqual(16, ranks["NYG"]);

                    ranks = afc.OffensiveRankings;
                    Assert.AreEqual(1,  ranks["NE" ]);
                    Assert.AreEqual(2,  ranks["JAX"]);
                    Assert.AreEqual(3,  ranks["KC" ]);
                    Assert.AreEqual(4,  ranks["PIT"]);
                    Assert.AreEqual(5,  ranks["BAL"]);
                    Assert.AreEqual(6,  ranks["LAC"]);
                    Assert.AreEqual(7,  ranks["HOU"]);
                    Assert.AreEqual(8,  ranks["TEN"]);
                    Assert.AreEqual(9,  ranks["BUF"]);
                    Assert.AreEqual(10, ranks["OAK"]);
                    Assert.AreEqual(11, ranks["NYJ"]);
                    Assert.AreEqual(12, ranks["CIN"]);
                    Assert.AreEqual(13, ranks["DEN"]);
                    Assert.AreEqual(14, ranks["MIA"]);
                    Assert.AreEqual(15, ranks["IND"]);
                    Assert.AreEqual(16, ranks["CLE"]);
                    break;
                default:
                    break;
            }
        }

            [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Conference_DefensiveRankings()
        {
            IConference nfc = this.NFL.GetConference("NFC");
            IConference afc = this.NFL.GetConference("AFC");
            Dictionary<string, int> ranks = new Dictionary<string, int>();

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    ranks = nfc.DefensiveRankings;
                    Assert.AreEqual(1,  ranks["NYG"]);
                    Assert.AreEqual(2,  ranks["SEA"]);
                    Assert.AreEqual(3,  ranks["DAL"]);
                    Assert.AreEqual(4,  ranks["MIN"]);
                    Assert.AreEqual(5,  ranks["PHI"]);
                    Assert.AreEqual(6,  ranks["DET"]);
                    Assert.AreEqual(7,  ranks["ARI"]);
                    Assert.AreEqual(8,  ranks["TB" ]);
                    Assert.AreEqual(9,  ranks["WAS"]);
                    Assert.AreEqual(10, ranks["GB" ]);
                    Assert.AreEqual(11, ranks["LA" ]);
                    Assert.AreEqual(12, ranks["CHI"]);
                    Assert.AreEqual(13, ranks["CAR"]);
                    Assert.AreEqual(14, ranks["ATL"]);
                    Assert.AreEqual(15, ranks["NO" ]);
                    Assert.AreEqual(16, ranks["SF" ]);

                    ranks = afc.DefensiveRankings;
                    Assert.AreEqual(1,  ranks["NE" ]);
                    Assert.AreEqual(2,  ranks["DEN"]);
                    Assert.AreEqual(3,  ranks["KC" ]);
                    Assert.AreEqual(4,  ranks["CIN"]);
                    Assert.AreEqual(5,  ranks["BAL"]);
                    Assert.AreEqual(6,  ranks["PIT"]);
                    Assert.AreEqual(7,  ranks["HOU"]);
                    Assert.AreEqual(8,  ranks["BUF"]);
                    Assert.AreEqual(8,  ranks["TEN"]);
                    Assert.AreEqual(10, ranks["MIA"]);
                    Assert.AreEqual(11, ranks["OAK"]);
                    Assert.AreEqual(12, ranks["IND"]);
                    Assert.AreEqual(13, ranks["JAX"]);
                    Assert.AreEqual(14, ranks["NYJ"]);
                    Assert.AreEqual(15, ranks["SD" ]);
                    Assert.AreEqual(16, ranks["CLE"]);
                    break;
                case "NFLStats_2017.xml":
                    ranks = nfc.DefensiveRankings;
                    Assert.AreEqual(1,  ranks["MIN"]);
                    Assert.AreEqual(2,  ranks["PHI"]);
                    Assert.AreEqual(3,  ranks["ATL"]);
                    Assert.AreEqual(4,  ranks["CHI"]);
                    Assert.AreEqual(5,  ranks["NO" ]);
                    Assert.AreEqual(6,  ranks["CAR"]);
                    Assert.AreEqual(7,  ranks["LA" ]);
                    Assert.AreEqual(8,  ranks["DAL"]);
                    Assert.AreEqual(8,  ranks["SEA"]);
                    Assert.AreEqual(10, ranks["ARI"]);
                    Assert.AreEqual(11, ranks["DET"]);
                    Assert.AreEqual(12, ranks["TB" ]);
                    Assert.AreEqual(13, ranks["SF" ]);
                    Assert.AreEqual(14, ranks["GB" ]);
                    Assert.AreEqual(15, ranks["WAS"]);
                    Assert.AreEqual(15, ranks["NYG"]);

                    ranks = afc.DefensiveRankings;
                    Assert.AreEqual(1,  ranks["JAX"]);
                    Assert.AreEqual(2,  ranks["LAC"]);
                    Assert.AreEqual(3,  ranks["NE" ]);
                    Assert.AreEqual(4,  ranks["BAL"]);
                    Assert.AreEqual(5,  ranks["PIT"]);
                    Assert.AreEqual(6,  ranks["KC" ]);
                    Assert.AreEqual(7,  ranks["CIN"]);
                    Assert.AreEqual(8,  ranks["TEN"]);
                    Assert.AreEqual(9,  ranks["BUF"]);
                    Assert.AreEqual(10, ranks["OAK"]);
                    Assert.AreEqual(11, ranks["DEN"]);
                    Assert.AreEqual(11, ranks["NYJ"]);
                    Assert.AreEqual(13, ranks["MIA"]);
                    Assert.AreEqual(14, ranks["IND"]);
                    Assert.AreEqual(15, ranks["CLE"]);
                    Assert.AreEqual(16, ranks["HOU"]);
                    break;
                default:
                    break;
            }
        }
    }
}
