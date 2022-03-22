using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.ItemModels
{
    public class AvgItemPlacement
    {
        public double avgPlacement { get; set; }
        public int id { get; set; }
        public int itemMode { get; set; }
        public double? playRate { get; set; }
        public string name { get; set; }
    }
}