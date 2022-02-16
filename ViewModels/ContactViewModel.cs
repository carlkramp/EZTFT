using EZTFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.ViewModels
{
    public class ContactViewModel
    {
        public ChallengerLeague challengerLeague { get; set; }
        public Summoner summoner { get; set; }
        public List<string> matchIds { get; set; }
        public MatchDto matchDto { get; set; }
        public Champ unitPlacement { get; set; }

        public string matchIdCheckResult { get; set; }
    }
}