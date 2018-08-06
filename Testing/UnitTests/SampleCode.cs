using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleCode;


namespace Testing.UnitTests
{
    [TestClass]
    public class SampleCode 
    {
        [TestMethod]
        public void SampleCode_Test_Constructor()
        {
            Person person = new Person("John", "Doe", DateTime.Parse("5/7/1987"));
            Assert.AreEqual("John", person.FirstName);
            Assert.AreEqual("Doe", person.LastName);
            Assert.AreEqual("John Doe", person.FullName);
        }

        [TestMethod]
        public void SampleCode_Test_YearsOld()
        {
            Person person = new Person("John", "Doe", DateTime.Parse("5/7/1987"));
            Assert.AreEqual(31, person.YearsOld);

            Person LeapBaby = new Person("", "", DateTime.Parse("2/29/2000"));

            Assert.AreEqual(18, LeapBaby.YearsOld);
            Assert.AreEqual(18, LeapBaby.AgeAsOf(DateTime.Parse("3/1/2018")));
            Assert.AreEqual(17, LeapBaby.AgeAsOf(DateTime.Parse("2/28/2018")));
            Assert.AreEqual(8, LeapBaby.AgeAsOf(DateTime.Parse("2/28/2009")));
        }
    }
}
