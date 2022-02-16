using EZTFT.Models;
using EZTFT.Models.MatchModels;
using EZTFT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EZTFT.Controllers
{
    public class ChampsController : Controller
    {
        private ApplicationDbContext _context;

        public ChampsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Champs
        public ActionResult Index()
        {
            return View();
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
                    throw new ArgumentException("Error getting challenegerLeague from Api");
                }
            }
        }

        public Summoner GetSummoner(string apiKey, string apiKeyHeader, ChallengerLeague challengerLeague)
        {
            Summoner summoner = new Summoner();

            using (var client = new HttpClient())
            {
                var summonerId = challengerLeague.entries[0].summonerId;
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

        public MatchDto GetMatchDto(string apiKey, string apiKeyHeader, List<string> matchIds)
        {
            MatchDto matchDto = new MatchDto();

            using (var client = new HttpClient())
            {
                var machid = matchIds[1];
                var url = new Uri("https://americas.api.riotgames.com/tft/match/v1/matches/" + machid);

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
                    return matchDto;

                }
                else 
                {                  
                    throw new ArgumentException("Error getting matchDto from Api");
                }
            }

        }

        public void SaveMatch(MatchDto matchDto)
        {
            var existingMatchCheck = _context.MatchIds
                           .Where(m => m.matchId == matchDto.metadata.match_id)
                           .FirstOrDefault();

            Champ champ = new Champ();
            MatchId newMatchId = new MatchId();

            //var existingMatchCheckResult = "New Match Found";

            if (existingMatchCheck == null)
            {
                newMatchId.matchId = matchDto.metadata.match_id;
                _context.MatchIds.Add(newMatchId);
                _context.SaveChanges();

                foreach (var participant in matchDto.info.participants)
                {
                    champ.placement = participant.placement;

                    foreach (var unit in participant.units)
                    {
                        champ.character_id = unit.character_id;
                        _context.Champs.Add(champ);
                        _context.SaveChanges();
                    }

                }
            }

            else
            {
                throw new ArgumentException("Match already exists in the database");
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

            MatchDto matchDto = GetMatchDto(apiKey, apiKeyHeader, matchIds);

            //Save match to database

            SaveMatch(matchDto);        

        }

        public ChampViewModel FindAndSaveMatchAndReturnViewModel()
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

            MatchDto matchDto = GetMatchDto(apiKey, apiKeyHeader, matchIds);

            //Save match to database

            SaveMatch(matchDto);

            ChampViewModel champViewModel = new ChampViewModel();

            //champViewModel.challengerLeague = challengerLeague;
            //champViewModel.summoner = summoner;
            //champViewModel.matchIds = matchIds;
            //champViewModel.matchDto = matchDto;

            return champViewModel;

        }  

        public List<AvgChampPlacement> GetAverageChampPlacementsAndReturnList()
        {
            // Initialize Variables
            List<AvgChampPlacement> avgChampPlacementsList = new List<AvgChampPlacement>();
            

            // Perform query to get champs and their placements from champs table, group  champs by their champ ids and calculate the average placement of each group
            var champAvgQuery =
                from Champ in _context.Champs
                group Champ by Champ.character_id into champGroup
                select new
                {
                    ChampName = champGroup.Key,
                    AveragePlacement = champGroup.Average(x => x.placement)
                };        

            // For each group of champs from the query, assign them an average placement and save the results to the avgChampPlacment table
            foreach (var champGroup in champAvgQuery)
            {
                AvgChampPlacement avgChampPlacement = new AvgChampPlacement();
                avgChampPlacement.avgPlacement = champGroup.AveragePlacement;
                avgChampPlacement.character_id = champGroup.ChampName;
                avgChampPlacementsList.Add(avgChampPlacement);
            }          

            return avgChampPlacementsList;
        }

        public void SaveAverageChampPlacementsToDatabase(List<AvgChampPlacement> avgChampPlacements)
        {
            foreach (var avgChampPlacement in avgChampPlacements)
            {
                _context.AvgChampPlacements.Add(avgChampPlacement);
                _context.SaveChanges();
            }
        }

    }   
}