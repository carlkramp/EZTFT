using EZTFT.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class WeatherModel
    {
        public Request request { get; set; }
        public Location location { get; set; }
        public Current current { get; set; }
    }
}