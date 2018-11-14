using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLScraper.NFLLeagueStructure
{
    [Serializable]
    public class NFLDivision
    {
        public string DivisionName { get; set; }
        public NFLTeam[] Teams { get; set; }

        internal NFLDivision() { }
        public NFLDivision(string name, NFLTeam[] teams)
        {
            this.DivisionName = name;
            this.Teams = teams;
        }
    }
}
