using EZTFT.Models;
using EZTFT.Models.TraitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EZTFT.Controllers.Api
{
    public class TraitAvgPlacementController : ApiController
    {       

            private ApplicationDbContext _context;

            public TraitAvgPlacementController()
            {
                 _context = new ApplicationDbContext();
            }
            //GET /api/traitAvgPlacements
            public IEnumerable<TraitAvgPlacement> GetTraitAvgPlacements()
            {
                return _context.TraitAvgPlacements.ToList();
            }

            // GET /api/traitAvgPlacement
            public TraitAvgPlacement GetTraitAvgPlacement(int id)
            {
                var traitAvgPlacement = _context.TraitAvgPlacements.SingleOrDefault(c => c.id == id);

                if (traitAvgPlacement == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    return traitAvgPlacement;
                }
            }

        // POST /api/TraitAvgPlacements
        [HttpPost]
            public TraitAvgPlacement CreateTraitAvgPlacement(TraitAvgPlacement traitAvgPlacement)
            {
                if (ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                _context.TraitAvgPlacements.Add(traitAvgPlacement);
                _context.SaveChanges();

                return traitAvgPlacement;
            }

        // PUT /api/TraitAvgPlacement/1
        [HttpPut]
            public void UpdateChamp(int id, TraitAvgPlacement traitAvgPlacement)
            {
                if (ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var traitAvgPlacementInDb = _context.TraitAvgPlacements.SingleOrDefault(c => c.id == id);

                if (traitAvgPlacementInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

            traitAvgPlacementInDb.id = traitAvgPlacementInDb.id;
            traitAvgPlacementInDb.name = traitAvgPlacementInDb.name;
            traitAvgPlacementInDb.TraitMode = traitAvgPlacementInDb.TraitMode;
            traitAvgPlacementInDb.tier_current = traitAvgPlacementInDb.tier_current;
            traitAvgPlacementInDb.AvgPlacement = traitAvgPlacementInDb.AvgPlacement;
            }

        // DELETE /api/traitAvgPlacement/1
        [HttpDelete]
            public void DeletetraitAvgPlacement(int id)
            {
                var traitAvgPlacementInDb = _context.TraitAvgPlacements.SingleOrDefault(c => c.id == id);

                if (traitAvgPlacementInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                _context.TraitAvgPlacements.Remove(traitAvgPlacementInDb);
                _context.SaveChanges();
            }
        }
    }
