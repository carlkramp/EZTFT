using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.TraitModels
{
    public class TraitAvgPlacement
    {

        public int id { get; set; }
        public string name { get; set; }
        public int tier_current { get; set; }
        public int TraitMode { get; set; }
        public double AvgPlacement { get; set; }

        public double? playRate { get; set; }
    }
}