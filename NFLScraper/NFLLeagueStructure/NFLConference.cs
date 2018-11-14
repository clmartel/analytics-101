using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLScraper.NFLLeagueStructure
{
    [Serializable]
    public class NFLConference
    {
        public string ConferenceName { get; set; }
        public NFLDivision[] Divisions { get; set; }

        internal NFLConference() { }
        public NFLConference(string name, NFLDivision[] divisions)
        {
            this.ConferenceName = name;
            this.Divisions = divisions;
        }
    }
}
