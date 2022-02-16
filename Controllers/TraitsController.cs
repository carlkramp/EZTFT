using EZTFT.Models;
using EZTFT.Models.CompModels;
using EZTFT.Models.TraitModels;
using EZTFT.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZTFT.Controllers
{
    public class TraitsController : Controller
    {
        private ApplicationDbContext _context;

        public TraitsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Traits
        public ActionResult Index()
        {
            // Gets trait data from Comps/TraitDtoes tables and saves it to TraitAvgPlacement table 
            //var myNewList = GetTraits();
            //SaveAverageTraitPlacementsToDatabase(myNewList);

            // Deletes old data from TraitAvgPlacement Table
            //DropOldData();

            return View();
        }

        public List<TraitAvgPlacement> GetTraits()
        {
            List<TraitAvgPlacement> traitAvgPlacements = new List<TraitAvgPlacement>();

            string queryString = "SELECT TraitDtoes.name, TraitDtoes.tier_current, Count(*) AS TraitMode, AVG(Comps.placement) AS AvgPlacement FROM Comps INNER JOIN TraitDtoes ON Comps.id = TraitDtoes.Comp_id WHERE TraitDtoes.tier_current > 0 GROUP BY TraitDtoes.name, TraitDtoes.tier_current";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=aspnet-EZTFT-20220118022938;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        TraitAvgPlacement traitAvgPlacement = new TraitAvgPlacement();
                        traitAvgPlacement.name = reader.GetString(0);
                        traitAvgPlacement.tier_current = reader.GetInt32(1);
                        traitAvgPlacement.TraitMode = reader.GetInt32(2);
                        traitAvgPlacement.AvgPlacement = reader.GetInt32(3);
                        traitAvgPlacements.Add(traitAvgPlacement);                       

                    }
                }
                finally
                {                 
                    reader.Close();
                }
            }

            return traitAvgPlacements;          

        }
        public void SaveAverageTraitPlacementsToDatabase(List<TraitAvgPlacement> traitAvgPlacements)
        {
            foreach (var traitAvgPlacement in traitAvgPlacements)
            {
                _context.TraitAvgPlacements.Add(traitAvgPlacement);
                _context.SaveChanges();
            }
        }

        public void DropOldData()
        {
            string queryString = "DELETE FROM TraitAvgPlacements";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=aspnet-EZTFT-20220118022938;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();

            }

        }
    }
}