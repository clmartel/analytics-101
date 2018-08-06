using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Testing.UnitTests
{
    [TestClass]
    public class ComparerTests : UnitTestBase
    {
        private IComparer<ITeam> GetDivisionComparer()
        {
            return (IComparer<ITeam>)GetClassInstance("FootballObjects.TeamComparers.DivisionCompare", new object[] { this.NFL });
        }

        private IComparer<ITeam> GetConferenceComparer()
        {
            return (IComparer<ITeam>)GetClassInstance("FootballObjects.TeamComparers.ConferenceCompare", new object[] { this.NFL });
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void ConferenceCoinFlips()
        {
            IComparer<ITeam> c = GetConferenceComparer();

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("WAS")));
                    Assert.AreNotEqual(0, c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("WAS")));
                    Assert.ThrowsException<Exception>(() => c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("IND")));
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("WAS")));
                    Assert.AreNotEqual(0, c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("WAS")));
                    break;
                case "NFLStats_2018_Week_0.xml":
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("SEA")));
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("BUF"), this.NFL.GetTeam("NE")));
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("CAR")));
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("HOU"), this.NFL.GetTeam("IND")));
                    Assert.AreEqual(0, c.Compare(this.NFL.GetTeam("LA"), this.NFL.GetTeam("TB")));
                    break;
                default:
                    break;
            }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\InjectedAssemblies.csv", "InjectedAssemblies#csv", DataAccessMethod.Sequential)]
        public void DivisionCoinFlips()
        {
            IComparer<ITeam> c = GetDivisionComparer();

            switch (this.LeagueDataFile.StripPath())
            {
                case "NFL_2016.xml":
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("WAS")), 0);
                    Assert.AreNotEqual(c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("CAR")), 0);
                    Assert.ThrowsException<Exception>(() => c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("IND")));
                    Assert.ThrowsException<Exception>(() => c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("WAS")));
                    break;
                case "NFL_2017.xml":
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("WAS")), 0);
                    Assert.AreNotEqual(c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("CAR")), 0);
                    break;
                case "NFLStats_2018_Week_0.xml":
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("LA"), this.NFL.GetTeam("SEA")), 0);
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("BUF"), this.NFL.GetTeam("NE")), 0);
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("NO"), this.NFL.GetTeam("CAR")), 0);
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("HOU"), this.NFL.GetTeam("IND")), 0);
                    Assert.AreEqual(c.Compare(this.NFL.GetTeam("WAS"), this.NFL.GetTeam("PHI")), 0);
                    break;
                default:
                    break;
            }
        }
    }
}
