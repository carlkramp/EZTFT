using EZTFT.Models;
using EZTFT.Models.ItemModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            // Get and display average item placements to user
            //List<AvgItemPlacement> itemPlacements = new List<AvgItemPlacement>();

            //DropOldData();
            //itemPlacements = GetAverageItemPlacementsAndReturnList();
            //SaveAverageItemPlacementsToDatabase(itemPlacements);


            // Update items from Community Dragon
            //GetItemsFromCommunityDragonAsync();
            //Item[] itemArray = GetItemsFromCommunityDragonAsync();
            //SaveItemsToDatabase(itemArray);

            return View();
        }

       

        public List<AvgItemPlacement> GetAverageItemPlacementsAndReturnList()
        {
            List<AvgItemPlacement> avgItemPlacementsList = new List<AvgItemPlacement>();

            var rows = _context.ItemStats.Select(x => x).ToList();

            double rowsCount = rows.Count();

            string queryString = "SELECT ItemStats.name, Count(*) AS ItemMode, AVG(Champs.placement) AS AvgPlacement FROM Champs INNER JOIN ItemStats ON Champs.id = ItemStats.Champ_id GROUP BY ItemStats.name";
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
                        AvgItemPlacement avgItemPlacement = new AvgItemPlacement();
                        avgItemPlacement.name = reader.GetString(0);
                        avgItemPlacement.itemMode = reader.GetInt32(1);
                        avgItemPlacement.avgPlacement = reader.GetInt32(2);
                        avgItemPlacement.playRate = avgItemPlacement.itemMode / rowsCount;

                        string itemPlayRate = "";
                        double itemPlayRateDouble = 0;

                        itemPlayRate = (avgItemPlacement.playRate * 100).ToString().Substring(0, 4);
                        itemPlayRateDouble = Convert.ToDouble(itemPlayRate);

                        avgItemPlacement.playRate = itemPlayRateDouble;

                        avgItemPlacementsList.Add(avgItemPlacement);

                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return avgItemPlacementsList;
        }

        public void SaveAverageItemPlacementsToDatabase(List<AvgItemPlacement> avgItemPlacements)
        {
            foreach (var avgItemPlacement in avgItemPlacements)
            {
                _context.AvgItemPlacement.Add(avgItemPlacement);
                _context.SaveChanges();
            }
        }

        public Item[] GetItemsFromCommunityDragonAsync()
        {
            Item item = new Item();
            Item[] itemArray = new Item[637];

            using (var client = new HttpClient())
            {
                var url = "https://raw.communitydragon.org/latest/cdragon/tft/en_us.json";
                //client.DefaultRequestHeaders.Add(apiKeyHeader, apiKey);
                //HTTP GET
                var responseTask = client.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();                  
                    readTask.Wait();

                    CDResponseData responseData = JsonConvert.DeserializeObject<CDResponseData>(readTask.Result);
                    itemArray = responseData.items;

                    return itemArray;

                }
                else
                {
                    throw new ArgumentException("Error getting Items from Api");
                }
            }
        }     

        public void SaveItemsToDatabase(Item[] itemArray)
        {
            Item itemInDb = new Item();

            foreach (Item item in itemArray)
            {
                itemInDb.desc = item.desc;
                itemInDb.icon = item.icon;
                itemInDb.item_id = item.id;
                itemInDb.name = item.name;
                itemInDb.unique = item.unique;

                _context.Items.Add(itemInDb);
                _context.SaveChanges();
            }
        }


        public void DropOldData()
        {
            string queryString = "DELETE FROM AvgItemPlacements";
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