using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EZTFT.Models.CompModels
{
    public class Comp
    {
   
        public int id { get; set; }

        public int placement { get; set; }

        public List<TraitDto> traits { get; set; }

        public List<UnitDto> units { get; set; }
    }
}