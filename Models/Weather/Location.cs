using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.Weather
{
    public class Location
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone_id { get; set; }
        public string localtime { get; set; }
        public long localtime_epoch { get; set; }
        public string utc_offset { get; set; }
    }
}