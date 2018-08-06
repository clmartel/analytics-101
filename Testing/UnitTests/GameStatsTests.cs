using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class GameStatsTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void GameStats_Constructor()
        {
            ITeam team = this.NFL.GetTeam("NO");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    IGame oakGame = team.Games.Single(g => g.AwayTeam == "OAK");
                    IGameStats nooakStats = oakGame.HomeStats;
                    IGameStats oakStats = oakGame.AwayStats;
                    Assert.AreEqual(2, nooakStats.FieldGoals);
                    Assert.AreEqual(27, nooakStats.FirstDowns);
                    Assert.AreEqual(419, nooakStats.PassingYards);
                    Assert.AreEqual(7, nooakStats.Penalties);
                    Assert.AreEqual(53, nooakStats.PenaltyYards);
                    Assert.AreEqual(2, nooakStats.Punts);
                    Assert.AreEqual(95, nooakStats.PuntYards);
                    Assert.AreEqual(88, nooakStats.RushingYards);
                    Assert.AreEqual("00:29:35", nooakStats.TimeOfPosession.ToString());
                    Assert.AreEqual(4, nooakStats.Touchdowns);
                    Assert.AreEqual(4, nooakStats.TouchdownsAllowed);
                    Assert.AreEqual(1, nooakStats.Turnovers);

                    Assert.AreEqual(2, oakStats.FieldGoals);
                    Assert.AreEqual(25, oakStats.FirstDowns);
                    Assert.AreEqual(319, oakStats.PassingYards);
                    Assert.AreEqual(14, oakStats.Penalties);
                    Assert.AreEqual(141, oakStats.PenaltyYards);
                    Assert.AreEqual(4, oakStats.Punts);
                    Assert.AreEqual(199, oakStats.PuntYards);
                    Assert.AreEqual(167, oakStats.RushingYards);
                    Assert.AreEqual("00:30:25", oakStats.TimeOfPosession.ToString());
                    Assert.AreEqual(4, oakStats.Touchdowns);
                    Assert.AreEqual(4, oakStats.TouchdownsAllowed);
                    Assert.AreEqual(0, oakStats.Turnovers);
                    break;
                case "NFL_2017.xml":
                    IGame minGame = team.Games.Single(g => g.HomeTeam == "MIN");
                    IGameStats minStats = minGame.HomeStats;
                    IGameStats nominStats = minGame.AwayStats;
                    Assert.AreEqual(3, minStats.FieldGoals);
                    Assert.AreEqual(23, minStats.FirstDowns);
                    Assert.AreEqual(341, minStats.PassingYards);
                    Assert.AreEqual(5, minStats.Penalties);
                    Assert.AreEqual(50, minStats.PenaltyYards);
                    Assert.AreEqual(2, minStats.Punts);
                    Assert.AreEqual(87, minStats.PuntYards);
                    Assert.AreEqual(129, minStats.RushingYards);
                    Assert.AreEqual("00:31:16", minStats.TimeOfPosession.ToString());
                    Assert.AreEqual(3, minStats.Touchdowns);
                    Assert.AreEqual(1, minStats.TouchdownsAllowed);
                    Assert.AreEqual(0, minStats.Turnovers);

                    Assert.AreEqual(4, nominStats.FieldGoals);
                    Assert.AreEqual(19, nominStats.FirstDowns);
                    Assert.AreEqual(284, nominStats.PassingYards);
                    Assert.AreEqual(6, nominStats.Penalties);
                    Assert.AreEqual(52, nominStats.PenaltyYards);
                    Assert.AreEqual(3, nominStats.Punts);
                    Assert.AreEqual(171, nominStats.PuntYards);
                    Assert.AreEqual(60, nominStats.RushingYards);
                    Assert.AreEqual("00:28:44", nominStats.TimeOfPosession.ToString());
                    Assert.AreEqual(1, nominStats.Touchdowns);
                    Assert.AreEqual(3, nominStats.TouchdownsAllowed);
                    Assert.AreEqual(0, nominStats.Turnovers);
                    break;
                default:
                    break;
            }
        }
    }
}
