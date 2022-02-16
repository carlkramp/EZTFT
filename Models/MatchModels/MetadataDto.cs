using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class MetadataDto
    {
        public string data_version { get; set; }
        public string match_id { get; set; }
        public List<string> participants { get; set; }
     
    }
}