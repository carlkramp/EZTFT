using EZTFT.Models.CompModels;
using EZTFT.Models.MatchModels;
using EZTFT.Models.TraitModels;
using EZTFT.Models.Weather;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class ApplicationDbContext: DbContext
    {

        public DbSet<Champ> Champs { get; set; } 
        public DbSet<MatchId> MatchIds { get; set; } 
        public DbSet<AvgChampPlacement> AvgChampPlacements { get; set; } 
        public DbSet<Comp> Comps { get; set; }
        public DbSet<TraitAvgPlacement> TraitAvgPlacements { get; set; }
        public DbSet<Location> location { get; set; }

        public ApplicationDbContext()
           : base("DefaultConnection2")
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}