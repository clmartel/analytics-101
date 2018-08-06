using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class DivisionTests : UnitTestBase
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Division_Constructor()
        {
            IDivision nfcNorth = this.NFL.GetDivision("NFC", "North");
            IDivision nfcSouth = this.NFL.GetDivision("NFC", "South");
            IDivision nfcEast = this.NFL.GetDivision("NFC", "East");
            IDivision nfcWest = this.NFL.GetDivision("NFC", "West");
            IDivision afcNorth = this.NFL.GetDivision("AFC", "North");
            IDivision afcSouth = this.NFL.GetDivision("AFC", "South");
            IDivision afcEast = this.NFL.GetDivision("AFC", "East");
            IDivision afcWest = this.NFL.GetDivision("AFC", "West");

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                case "NFL_2017.xml":
                    Assert.AreEqual("North", nfcNorth.Name);
                    Assert.AreEqual("South", nfcSouth.Name);
                    Assert.AreEqual("East", nfcEast.Name);
                    Assert.AreEqual("West", nfcWest.Name);
                    Assert.AreEqual("North", afcNorth.Name);
                    Assert.AreEqual("South", afcSouth.Name);
                    Assert.AreEqual("East", afcEast.Name);
                    Assert.AreEqual("West", afcWest.Name);

                    Assert.AreEqual(4, nfcNorth.Teams.Count());
                    Assert.AreEqual(4, nfcSouth.Teams.Count());
                    Assert.AreEqual(4, nfcEast.Teams.Count());
                    Assert.AreEqual(4, nfcWest.Teams.Count());
                    Assert.AreEqual(4, afcNorth.Teams.Count());
                    Assert.AreEqual(4, afcSouth.Teams.Count());
                    Assert.AreEqual(4, afcEast.Teams.Count());
                    Assert.AreEqual(4, afcWest.Teams.Count());
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void Division_WinPercentage()
        {
            IDivision nfcSouth = this.NFL.GetDivision("NFC", "South");
            ITeam saints = this.NFL.GetTeam("NO");

            IDivision nfcWest = this.NFL.GetDivision("NFC", "West");
            ITeam seahawks = this.NFL.GetTeam("SEA");

            switch (this.StatsDataFile.StripPath())
            {
                case "NFLStats_2016.xml":
                    Assert.AreEqual((2.0 / 6.0), nfcSouth.WinPercentage(saints));
                    Assert.AreEqual(((3.0 + (1.0 * 0.5)) / 6.0), nfcWest.WinPercentage(seahawks));
                    break;
                case "NFLStats_2017.xml":
                    Assert.AreEqual((4.0 / 6.0), nfcSouth.WinPercentage(saints));
                    break;
                default:
                    break;
            }
        }
    }
}
