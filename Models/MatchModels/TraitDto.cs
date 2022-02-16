using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class TraitDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int num_units { get; set; }
        public int style { get; set; }
        public int tier_current { get; set; }
        public int tier_total { get; set; }
    }
}