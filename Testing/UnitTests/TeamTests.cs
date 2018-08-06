using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class TeamTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_Constructor()
        {
            ITeam team = this.NFL.GetTeam("NO");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual("New Orleans", team.City);
                    Assert.AreEqual("Saints", team.NickName);
                    Assert.AreEqual("NO", team.ID);
                    Assert.AreEqual(16, team.Games.Count());
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual("New Orleans", team.City);
                    Assert.AreEqual("Saints", team.NickName);
                    Assert.AreEqual("NO", team.ID);
                    Assert.AreEqual(16, team.Games.Count());
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_WinsLossesTies()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(10, seahawks.Wins);
                    Assert.AreEqual(5, seahawks.Losses);
                    Assert.AreEqual(1, seahawks.Ties);
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(11, saints.Wins);
                    Assert.AreEqual(5, saints.Losses);
                    Assert.AreEqual(0, saints.Ties);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_Points()
        {
            ITeam team = this.NFL.GetTeam("NO");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(469, team.PointsScored);
                    Assert.AreEqual(454, team.PointsAllowed);
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(448, team.PointsScored);
                    Assert.AreEqual(326, team.PointsAllowed);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_Touchdowns()
        {
            ITeam team = this.NFL.GetTeam("NO");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(55, team.TouchdownsScored);
                    Assert.AreEqual(51, team.TouchdownsAllowed);
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(51, team.TouchdownsScored);
                    Assert.AreEqual(36, team.TouchdownsAllowed);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_WinPercentage()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(((10.0 + (1.0 * 0.5)) / 16.0), seahawks.WinPercentage);
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual((11.0/16.0), saints.WinPercentage);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_WonLossTied()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    IGame laGame = seahawks.Games.Single(g => g.HomeTeam == "LA");
                    IGame sfGame = seahawks.Games.Single(g => g.HomeTeam == "SF");
                    IGame ariGame = seahawks.Games.Single(g => g.HomeTeam == "ARI");

                    Assert.AreEqual(-1, seahawks.WonLostTied(laGame));
                    Assert.AreEqual(1, seahawks.WonLostTied(sfGame));
                    Assert.AreEqual(0, seahawks.WonLostTied(ariGame));
                    break;
                case "NFL_2017.xml":
                    IGame minGame = saints.Games.Single(g => g.HomeTeam == "MIN");
                    IGame miaGame = saints.Games.Single(g => g.HomeTeam == "MIA");

                    Assert.AreEqual(-1, saints.WonLostTied(minGame));
                    Assert.AreEqual(1, saints.WonLostTied(miaGame));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Team_HeadToHead()
        {
            ITeam team = this.NFL.GetTeam("NO");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(1, team.HeadToHead("ARI"));
                    Assert.AreEqual(0, team.HeadToHead("TB"));
                    Assert.AreEqual(-1, team.HeadToHead("ATL"));
                    Assert.AreEqual(0, team.HeadToHead(team.ID));
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(1, team.HeadToHead("CAR"));
                    Assert.AreEqual(0, team.HeadToHead("ATL"));
                    Assert.AreEqual(-1, team.HeadToHead("MIN"));
                    Assert.AreEqual(0, team.HeadToHead(team.ID));
                    break;
                default:
                    break;
            }
        }
    }
}
