using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.ItemModels
{
    public class ItemStats
    {
        public int id { get; set; }

        public int item_id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool isUnique { get; set; }

        //public bool isShadow { get; set; }


    }
}