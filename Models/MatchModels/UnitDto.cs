using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class UnitDto
    {
        public int id { get; set; }
        public List<int> items { get; set; }
        public string character_id { get; set; }
        public string chosen { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public int tier { get; set; }

    }
}