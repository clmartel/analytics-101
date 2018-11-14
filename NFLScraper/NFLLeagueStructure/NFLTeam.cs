using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLScraper.NFLLeagueStructure
{
    [Serializable]
    public class NFLTeam
    {
        public string City { get; set; }
        public string NickName { get; set; }
        public string ID { get; set; }

        internal NFLTeam() { }
        public NFLTeam(string city, string nickName, string id)
        {
            this.City = city;
            this.NickName = nickName;
            this.ID = id;
        }
    }
}
