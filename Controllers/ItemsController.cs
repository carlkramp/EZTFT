using EZTFT.Models;
using EZTFT.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZTFT.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Items
        public ActionResult Index()
        {
            List<AvgItemPlacement> itemPlacements = new List<AvgItemPlacement>();

            DropOldData();
            itemPlacements = GetAverageItemPlacementsAndReturnList();
            SaveAverageItemPlacementsToDatabase(itemPlacements);

            return View();
        }

       

        public List<AvgItemPlacement> GetAverageItemPlacementsAndReturnList()
        {
            List<AvgItemPlacement> avgItemPlacementsList = new List<AvgItemPlacement>();

            return avgItemPlacementsList;
        }

        public void SaveAverageItemPlacementsToDatabase(List<AvgItemPlacement> avgItemPlacements)
        {
            foreach (var avgItemPlacement in avgItemPlacements)
            {
                //_context.AvgItemPlacements.Add(avgItemPlacement);
                _context.SaveChanges();
            }
        }

        public void DropOldData()
        {
            string queryString = "DELETE FROM ItemAvgPlacements";
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