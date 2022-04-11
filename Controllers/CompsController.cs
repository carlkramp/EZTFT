using EZTFT.Models;
using EZTFT.Models.CompModels;
using EZTFT.Models.ItemModels;
using EZTFT.Models.MatchModels;
using EZTFT.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EZTFT.Controllers
{
    public class CompsController : Controller
    {
        private ApplicationDbContext _context;

        public CompsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Comps
        public ActionResult Index()
        {
            // Finds match, checks to see if match already exists in database, if not, saves match to MatchIds table and the list of comps in the match to the Comps table
            //DropAllOldData();
            //FindAndSaveMatch();

            //Fetch comps containing searched champ       
            return View();         
        }

        public ActionResult Result(string searchTerm)
        {
            CompViewModel compViewModel = GetComps(searchTerm);
            compViewModel.searchTerm = searchTerm + ".png";

            return View(compViewModel);
        }

        public ChallengerLeague GetSummonerId(string apiKey, string apiKeyHeader)
        {
            ChallengerLeague challengerLeague = new ChallengerLeague();

            using (var client = new HttpClient())
            {
                var url = "https://na1.api.riotgames.com/tft/league/v1/challenger";
                client.DefaultRequestHeaders.Add(apiKeyHeader, apiKey);
                //HTTP GET
                var responseTask = client.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ChallengerLeague>();
                    readTask.Wait();

                    challengerLeague = readTask.Result;
                    return challengerLeague;
                }
                else
                {
                    throw new ArgumentException("Error getting challengerLeague from Api");
                }
            }
        }

        public Summoner GetSummoner(string apiKey, string apiKeyHeader, ChallengerLeague challengerLeague)
        {
            Summoner summoner = new Summoner();

            using (var client = new HttpClient())
            {
                int x = 4;
                var summonerId = challengerLeague.entries[x].summonerId;
                var url = "https://na1.api.riotgames.com/tft/summoner/v1/summoners/" + summonerId;
                client.DefaultRequestHeaders.Add(apiKeyHeader, apiKey);
                //HTTP GET
                var responseTask = client.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Summoner>();
                    readTask.Wait();

                    summoner = readTask.Result;
                    return summoner;
                }
                else
                {
                    throw new ArgumentException("Error getting Summoner from Api");
                }
            }
        }

        public List<string> GetMatchIds(string apiKey, string apiKeyHeader, Summoner summoner)
        {
            List<string> matchIds = new List<string>();

            using (var client = new HttpClient())
            {
                var puuid = summoner.puuid;
                var url = "https://americas.api.riotgames.com/tft/match/v1/matches/by-puuid/" + puuid + "/ids?count=20";

                client.DefaultRequestHeaders.Add(apiKeyHeader, apiKey);
                //HTTP GET
                var responseTask = client.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<string>>();
                    readTask.Wait();

                    matchIds = readTask.Result;

                    return matchIds;

                }
                else
                {
                    throw new ArgumentException("Error getting matchIds from Api");
                }
            }
        }

        public List<MatchDto> GetMatchDto(string apiKey, string apiKeyHeader, List<string> matchIds)
        {
            List<MatchDto> matchDtos = new List<MatchDto>();
            MatchDto matchDto = new MatchDto();
            var rankedTFTQueueId = 1100;
            int x = 0;

            do
            {
                using (var client = new HttpClient())
                {

                    var matchid = matchIds[x];
                    var url = new Uri("https://americas.api.riotgames.com/tft/match/v1/matches/" + matchid);

                    client.DefaultRequestHeaders.Add(apiKeyHeader, apiKey);
                    //HTTP GET
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<MatchDto>();
                        readTask.Wait();

                        matchDto = readTask.Result;

                        if (x == 19 && matchDto.info.queue_id == rankedTFTQueueId)
                        {
                            matchDtos.Add(matchDto);
                            return matchDtos;
                        }
                        else if (x == 19 && matchDto.info.queue_id != rankedTFTQueueId)
                        {
                            return matchDtos;
                        }
                        else if (matchDto.info.queue_id == rankedTFTQueueId)
                        {
                            matchDtos.Add(matchDto);
                            x++;
                        }
                        else
                        {
                            x++;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Error getting matchDto from Api");
                    }
                }
            } while (x < 20);

            throw new ArgumentException("Error getting matchDto from Api, how did you get here");
        }      

        public void SaveMatches(List<MatchDto> matchDtoes)
        {

            MatchDto lastMatch = matchDtoes.Last();

            foreach (var match in matchDtoes)
            {
                var existingMatchCheck = _context.MatchIds
                           .Where(m => m.matchId == match.metadata.match_id)
                           .FirstOrDefault();

                Comp comp = new Comp();
                Champ champ = new Champ();
                MatchId newMatchId = new MatchId();
                ItemStats itemStats = new ItemStats();
                List<ItemStats> itemList = new List<ItemStats>();
                Item item = new Item();

                if (existingMatchCheck == null)
                {
                    newMatchId.matchId = match.metadata.match_id;
                    newMatchId.gameVersion = match.info.game_version;
                    _context.MatchIds.Add(newMatchId);
                    _context.SaveChanges();

                    foreach (var participant in match.info.participants)
                    {
                        comp.placement = participant.placement;
                        comp.units = participant.units;
                        comp.traits = participant.traits;
                        champ.placement = participant.placement;

                        _context.Comps.Add(comp);
                        _context.SaveChanges();

                        foreach (var unit in participant.units)
                        {

                            champ.character_id = unit.character_id;
                            itemList.Clear();

                            if (unit.items.Count > 0)
                            {
                                for (int x = 0; x < unit.items.Count; x++)
                                {
                                    ItemStats itemStats1 = new ItemStats();
                                    int itemId = unit.items[x];

                                    var itemQuery =
                                        from dbItem in _context.Items
                                        where dbItem.item_id == itemId
                                        select dbItem;

                                    item = itemQuery.FirstOrDefault();
                                    itemStats1.description = item.desc;
                                    itemStats1.item_id = item.item_id;
                                    itemStats1.isUnique = item.unique;
                                    itemStats1.name = item.name;

                                    itemList.Add(itemStats1);
                                }
                            }

                            champ.items = itemList;
                            champ.unitDtoId = unit.id;
                            _context.Champs.Add(champ);
                            _context.SaveChanges();

                        }
                    }


                }

                else
                {
                    if (match == lastMatch)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }

        public void FindAndSaveMatch()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["apiKey"];
            string apiKeyHeader = "X-Riot-Token";

            //get a summonerid using challenger league players

            ChallengerLeague challengerLeague = GetSummonerId(apiKey, apiKeyHeader);

            //Get a puuid using summonerid

            Summoner summoner = GetSummoner(apiKey, apiKeyHeader, challengerLeague);

            //Get match ids based off puuid

            List<string> matchIds = GetMatchIds(apiKey, apiKeyHeader, summoner);

            //Get matches by matchId

            List<MatchDto> matchDtoes = GetMatchDto(apiKey, apiKeyHeader, matchIds);

            //Save match to database

            SaveMatches(matchDtoes);

        }

        public void DropAllOldData()
        {
            string queryString1 = "DELETE FROM TraitDtoes";
            string queryString2 = "DELETE FROM UnitDtoes";
            string queryString3 = "DELETE FROM TraitAvgPlacements";
            string queryString4 = "DELETE FROM AvgItemPlacements";
            string queryString5 = "DELETE FROM AvgChampPlacements";
            string queryString6 = "DELETE FROM MatchIds";
            string queryString7 = "DELETE FROM Comps";
            string queryString8 = "DELETE FROM ItemStats";
            string queryString9 = "DELETE FROM Champs";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=aspnet-EZTFT-20220118022938;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command1 = new SqlCommand(queryString1, connection);
                SqlCommand command2 = new SqlCommand(queryString2, connection);
                SqlCommand command3 = new SqlCommand(queryString3, connection);
                SqlCommand command4 = new SqlCommand(queryString4, connection);
                SqlCommand command5 = new SqlCommand(queryString5, connection);
                SqlCommand command6 = new SqlCommand(queryString6, connection);
                SqlCommand command7 = new SqlCommand(queryString7, connection);
                SqlCommand command8 = new SqlCommand(queryString8, connection);
                SqlCommand command9 = new SqlCommand(queryString9, connection);
                connection.Open();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                command5.ExecuteNonQuery();
                command6.ExecuteNonQuery();
                command7.ExecuteNonQuery();
                command8.ExecuteNonQuery();
                command9.ExecuteNonQuery();

            }
        } 

        public int GetChampRarity(string userInput)
        {
            string queryString = "SELECT UnitDtoes.rarity FROM UnitDtoes WHERE UnitDtoes.character_id = 'TFT6_" + userInput + "'";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=aspnet-EZTFT-20220118022938;Integrated Security=True;";
            int champRarity = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        champRarity = reader.GetInt32(0);                       

                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return champRarity;

        }

        // Perform SQL search to retrieve list of comps ids which contain searched unit
        public List<int> ChampCompSearch(string userInput)
        {
            // Declare variables
            List<int> compIds = new List<int>();
            string queryString = "";
            
            // Get champ rarity
            int champRarity = GetChampRarity(userInput);

            // If champ rarity(cost) is 0-2, only return comps in which the searched champ's tier is 3 (unit is 3 starred), otherwise if the champ's rarity is 3 or higher, return any comps that champ is in.
            if (champRarity < 3)
            {
                queryString = "SELECT UnitDtoes.Comp_id FROM UnitDtoes JOIN Comps ON UnitDtoes.Comp_id = Comps.id WHERE UnitDtoes.Comp_id IN ( SELECT Comp_id FROM UnitDtoes WHERE UnitDtoes.character_id = 'TFT6_" + userInput + "' AND UnitDtoes.tier = 3) GROUP BY UnitDtoes.Comp_id";
            }
            else
            {
                queryString = "SELECT UnitDtoes.Comp_id FROM UnitDtoes JOIN Comps ON UnitDtoes.Comp_id = Comps.id WHERE UnitDtoes.Comp_id IN ( SELECT Comp_id FROM UnitDtoes WHERE UnitDtoes.character_id = 'TFT6_" + userInput + "') GROUP BY UnitDtoes.Comp_id";
            }

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
                        int compId = reader.GetInt32(0);
                        compIds.Add(compId);

                    }
                }
                finally
                {
                    reader.Close();
                }
            }          

            return compIds;
        }

        public CompViewModel GetComps(string userInput)
        {
            //Declare variables
            CompViewModel compViewModel = new CompViewModel();
            List<CompResult> compList = new List<CompResult>();

            //Get list of compIds
            List<int> compIds = ChampCompSearch(userInput);

            // Perform linq query using list of comp ids to get individual comps
            foreach (var compId in compIds)
            {
                var compQuery =
                     from Comp in _context.Comps
                     where Comp.id == compId
                     select new
                     {
                         myCompId = Comp.id,
                         myCompPlacement = Comp.placement,
                         myCompTraits = Comp.traits,
                         myCompUnits = Comp.units

                     };

                foreach (var comp in compQuery)
                {
                    // Reset the CompResult and list of item names for each new comp 
                    CompResult newComp = new CompResult();
                    List<string> itemNames = new List<string>();

                    newComp.id = comp.myCompId;
                    newComp.placement = comp.myCompPlacement;
                    // Sort units in comp by their cost 1-5
                    List<UnitDto> sortedUnitList = comp.myCompUnits.OrderBy(m => m.rarity).ToList();
                    newComp.units = sortedUnitList;
                    //Sort traits by their tier 1-4
                    List<TraitDto> sortedTraitList = comp.myCompTraits.OrderBy(t => t.tier_current).ToList();
                    newComp.traits = sortedTraitList;

                    for (int x = 0; x < newComp.units.Count; x++)
                    {
                        // Convert champ names from TFT6_Name to just Name, ie, TFT6_Jhin to Jhin
                        int charPos = newComp.units[x].character_id.IndexOf("_") + 1;
                        string champName = newComp.units[x].character_id.Substring(charPos);

                        if (champName == userInput)
                        {
                            int unitId = newComp.units[x].id;

                            var itemQuery =
                                    from Champ in _context.Champs
                                    where Champ.unitDtoId == unitId
                                    select new
                                    {
                                        items = Champ.items
                                    };

                            // List of champs and their list of items, however should only return a singular champ matching the searched id
                            var champItemsList = itemQuery.ToList();

                            if (champItemsList != null)
                            {
                                // For each individual champ that the query returns, though there should only be one
                                foreach (var champItems in champItemsList)
                                {
                                    // For each item in the list of items belonging to the return champ
                                    foreach (var item in champItems.items)
                                    {
                                        // Item names from Riot API have a space betweeen them, but spaces are replaced with underscores here so their names can be used in the src variable in <img> tags
                                        string itemName = item.name.Replace(" ", "_");
                                        itemName = itemName + ".png";
                                        itemNames.Add(itemName);
                                    }
                                }
                            }
                        }
                    }
                    newComp.items = itemNames;
                    compList.Add(newComp);
                }
            }

            compViewModel.comps = compList;

            return compViewModel;

        }
    }
}