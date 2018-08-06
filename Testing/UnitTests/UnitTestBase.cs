using FootballObjectContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Testing.UnitTests
{
    [TestClass]
    public class UnitTestBase
    {
        public TestContext TestContext { get; set; }
        public string StatsDataFile { get; set; }
        public string LeagueDataFile { get; set; }
        public ILeague NFL { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this.LeagueDataFile = TestContext.DataRow["LeagueDataFile"].ToString();
            this.StatsDataFile = TestContext.DataRow["StatsDataFile"].ToString();
            this.NFL = new LeagueBuilder(TestContext).BuildNFLLeague(this.StatsDataFile, this.LeagueDataFile, 17);

            Console.WriteLine($"Author:  {TestContext.DataRow["Author"].ToString()}");
            Console.WriteLine($"Assembly:  {TestContext.DataRow["Assembly"].ToString()}");
            Console.WriteLine($"LeagueFile:  {TestContext.DataRow["LeagueDataFile"].ToString()}");
            Console.WriteLine($"StatsFile:  {TestContext.DataRow["StatsDataFile"].ToString()}");
        }

        public object GetClassInstance(string className, object[] args)
        {
            string assemblyToLoad = TestContext.DataRow["Author"].ToString() == "Local"
                ? $"{Path.GetFullPath("../../../FootballObjects/bin/Debug/")}{TestContext.DataRow["Assembly"].ToString()}"
                : TestContext.DataRow["Assembly"].ToString();

            Assembly assemblyUnderTest = Assembly.LoadFile(assemblyToLoad);
            Type type = assemblyUnderTest.GetType(className);

            return Activator.CreateInstance(type, args);
        }
    }
}
