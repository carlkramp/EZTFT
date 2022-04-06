using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.CompModels
{
    public class CompResult
    {
        public int id { get; set; }

        public int placement { get; set; }

        public List<TraitDto> traits { get; set; }

        public List<UnitDto> units { get; set; }

        public List<string> items { get; set; }
    }
}