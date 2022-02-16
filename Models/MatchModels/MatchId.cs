using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EZTFT.Models.MatchModels
{
    public class MatchId
    {
        [Key]
        public string matchId { get; set; }
    }
}