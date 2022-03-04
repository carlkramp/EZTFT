using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class AvgChampPlacement
    {
        public double avgPlacement { get; set; }
        [Key]
        public string character_id { get; set; }
        public int champMode { get; set; }
        public double? playRate { get; set; }
    }
}