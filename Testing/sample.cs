/*
 
 this is a section of comments used to 
 provide lengthy information about the 
 code. good documentation is helpful 
 to remind us what the code does or for 
 other programmers to understand things 
 more quickly.

   -Chris Martel 8/1/2018

*/
using System;  // "using" provides references to other namespaces (this is an "inline" comment)

namespace SampleCode   // "namespace" defines a collection of classes
{
    /// <summary>
    /// A "class" is the base representation of an object.  We "extend" the 
    /// base class by defining "properties" and "methods" which represent 
    /// our object.  In this case the object is a person.
    /// 
    /// We don't really need to summarize a class called "Person" as it's
    /// pretty self explanatory as to what this object represents.  Too much
    /// documentation can clutter up the code.  Ideally good code will be 
    /// simple and will document itself.
    /// </summary>
    public class Person 
    {
        // #region is used to organize code for readability
        #region Properties  

        // Public properties - accessible by other classes
        public DateTime BirthDate { get; }
        public string FirstName { get; }
        public string LastName { get; } 
        public string FullName => String.Format("{0} {1}", FirstName, LastName);
        public int YearsOld => AgeAsOf(DateTime.Today);
        public bool IsAdult => (YearsOld >= 18);

        // Private properties - only accessible within this class
        private string SSN;
        #endregion
        //----------------------------------------------------------------------------------------
        #region Methods

        // Constructor: used by other classes to "instatiate" or create
        // a new instance of this class 
        public Person(string firstName, string lastName, DateTime birthday)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthday;
        }

        // void: a method that performs an action but does not return data
        public void SetSSN(string ssn)
        {
            this.SSN = ssn;
        }

        public int AgeAsOf(DateTime onDate)
        {
            int age = 0;

            // TODO: Calculate the age!

            return age;
        }

        // a boolean method could tell the calling code if an action succeeded or not, for example
        public bool Save()
        {
            bool success = false;
            // this might contain code for saving the 
            // properties into a database
            return success;
        }

        // override method overrides a member of the inherited class.  in this case
        // "ToString()" is a member of the base class object in C# - meaning that 
        // all classes have a ToString() method which may or may not be overridden 
        // with custom code
        public override string ToString()
        {
            // returns a string formatted like "Doe, John: 4/12/1994"
            return String.Format("{0}, {1}: {2}", LastName, FirstName, BirthDate.ToShortDateString());
        }
        #endregion
    }
}
