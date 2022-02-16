using EZTFT.Models;
using EZTFT.Models.CompModels;
using EZTFT.Models.MatchModels;
using System;
using System.Collections.Generic;
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
            //FindAndSaveMatch();

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
                    throw new ArgumentException("Error getting challengerLeague from Api");
                }
            }
        }

        public Summoner GetSummoner(string apiKey, string apiKeyHeader, ChallengerLeague challengerLeague)
        {
            Summoner summoner = new Summoner();

            using (var client = new HttpClient())
            {
                int x = 0;
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
            //bool validMatchId = false;
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

                        if (x == 19 && matchDto.info.queue_id == 74)
                        {
                            matchDtos.Add(matchDto);
                            return matchDtos;
                        }
                        else if (x == 19 && matchDto.info.queue_id != 74)
                        {
                            return matchDtos;
                        }
                        else if (matchDto.info.queue_id == 74)
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
            } while (x < 19);

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
                MatchId newMatchId = new MatchId();

                if (existingMatchCheck == null)
                {
                    newMatchId.matchId = match.metadata.match_id;
                    _context.MatchIds.Add(newMatchId);
                    _context.SaveChanges();

                    foreach (var participant in match.info.participants)
                    {
                        comp.placement = participant.placement;
                        comp.units = participant.units;
                        comp.traits = participant.traits;

                        _context.Comps.Add(comp);
                        _context.SaveChanges();

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

        //public GetCompsAndReturnList()
        //{
        //   var compGroupQuery =
        //   from Comp in _context.Comps
        //   group Comp by Comp.id into compGroup
        //   select new
        //   {
        //       ChampName = champGroup.Key,
        //       AveragePlacement = champGroup.Average(x => x.placement)
        //   };

        //    List<Comp> comp = new List<Comp>();
        //}
    }


}