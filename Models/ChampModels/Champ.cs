using EZTFT.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class Champ
    {
        public int id { get; set; }
        public int placement { get; set; }
        public string character_id { get; set; }    
        public List<ItemStats> items { get; set; }

        public int unitDtoId { get; set; }
    }
}