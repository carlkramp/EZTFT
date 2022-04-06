using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models.CompModels
{
    public class searchResult
    {
        public List<int> compIds { get; set; }
        
        public string searchTerm { get; set; }
    }
}