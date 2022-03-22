using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.ItemModels
{
    public class Item
    {
        public int id { get; set; }

        public int item_id { get; set; }

        public string desc { get; set; }

        public string icon { get; set; }

        public string name { get; set; }

        public bool unique { get; set; }

    }
}