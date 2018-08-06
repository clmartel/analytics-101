using FootballObjectContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballObjects.TeamComparers
{
    public class NameCompare : IComparer<ITeam>
    {
        public int Compare(ITeam x, ITeam y)
        {
            return String.Compare(x.ToString(), y.ToString());
        }
    }


    //The refactoring made these two methods not build.  Will need to rethink how this is done.
    //public class ConferenceNameCompare : IComparer<ITeam>
    //{
    //    public int Compare(ITeam x, ITeam y)
    //    {
    //        string xName = String.Format("{0} {1}", x.Conference, x);
    //        string yName = String.Format("{0} {1}", y.Conference, y);
    //        return String.Compare(xName,yName);
    //    }
    //}

    //public class ConferenceDivisionNameCompare : IComparer<ITeam>
    //{
    //    public int Compare(ITeam x, ITeam y)
    //    {
    //        string xName = String.Format("{0} {1} {2}", x.Conference, x.Division, x);
    //        string yName = String.Format("{0} {1} {2}", y.Conference, y.Division, y);
    //        return String.Compare(xName, yName);
    //    }
    //}

}
