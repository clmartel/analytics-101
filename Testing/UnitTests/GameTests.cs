using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class GameTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Game_Constructor()
        {
            ITeam team = this.NFL.GetTeam("NO");
            
            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    IGame oakGame = team.Games.Single(g => g.AwayTeam == "OAK");
                    Assert.AreEqual(35, oakGame.AwayPoints);
                    Assert.AreEqual(34, oakGame.HomePoints);
                    Assert.IsInstanceOfType(oakGame.AwayStats, typeof(IGameStats));
                    Assert.IsInstanceOfType(oakGame.HomeStats, typeof(IGameStats));
                    Assert.AreEqual("OAK", oakGame.AwayTeam);
                    Assert.AreEqual("NO", oakGame.HomeTeam);
                    Assert.AreEqual("9/11/2016 1:00:00 PM", oakGame.Date.ToString());
                    Assert.AreEqual(1, oakGame.Week);
                    break;
                case "NFL_2017.xml":
                    IGame minGame = team.Games.Single(g => g.HomeTeam == "MIN");
                    Assert.AreEqual(19, minGame.AwayPoints);
                    Assert.AreEqual(29, minGame.HomePoints);
                    Assert.IsInstanceOfType(minGame.AwayStats, typeof(IGameStats));
                    Assert.IsInstanceOfType(minGame.HomeStats, typeof(IGameStats));
                    Assert.AreEqual("NO", minGame.AwayTeam);
                    Assert.AreEqual("MIN", minGame.HomeTeam);
                    Assert.AreEqual("9/11/2017 7:10:00 PM", minGame.Date.ToString());
                    Assert.AreEqual(1, minGame.Week);
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Game_Winner()
        {
            ITeam saints = this.NFL.GetTeam("NO");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    IGame oakGame = saints.Games.Single(g => g.AwayTeam == "OAK");
                    Assert.AreEqual("OAK", oakGame.Winner);
                    IGame ariGame = seahawks.Games.Single(g => g.HomeTeam == "ARI" && g.Week == 7);
                    Assert.IsNull(ariGame.Winner);
                    break;
                case "NFL_2017.xml":
                    IGame minGame = saints.Games.Single(g => g.HomeTeam == "MIN");
                    Assert.AreEqual("MIN", minGame.Winner);
                    break;
                default:
                    break;
            }
        }
    }
}
