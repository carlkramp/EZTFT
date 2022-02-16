using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class ChallengerLeague
    {
        public string tier { get; set; }
        public string leagueId { get; set; }
        public string queue { get; set; }
        public string name { get; set; }
        public Entry[] entries { get; set; }
     
    }
}