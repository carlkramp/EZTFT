using EZTFT.Models.CompModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.ViewModels
{
    public class CompViewModel
    {
        public List<CompResult> comps = new List<CompResult>();

        public string searchTerm;

    }
}