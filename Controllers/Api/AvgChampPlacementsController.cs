using EZTFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EZTFT.Controllers.Api
{
    public class AvgChampPlacementsController : ApiController
    {
        private ApplicationDbContext _context;

        public AvgChampPlacementsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/avgChampPlacements
        public IEnumerable<AvgChampPlacement> GetAvgChampPlacements()
        {
            return _context.AvgChampPlacements.ToList();
        }

        // GET /api/avgChampPlacement
        public AvgChampPlacement AvgChampPlacement(string character_id)
        {
            var avgChampPlacement = _context.AvgChampPlacements.SingleOrDefault(x => x.character_id == character_id);

            if (avgChampPlacement == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return avgChampPlacement;
            }
        }

        // POST /api/avgChampPlacements
        [HttpPost]
        public AvgChampPlacement CreateAvgChampPlacement(AvgChampPlacement avgChampPlacement)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.AvgChampPlacements.Add(avgChampPlacement);
            _context.SaveChanges();

            return avgChampPlacement;
        }

        // PUT /api/avgChampPlacement/1
        [HttpPut]
        public void UpdateAvgChampPlacement(string character_id, Champ champ)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var avgChampPlacementInDb = _context.AvgChampPlacements.SingleOrDefault(x => x.character_id == character_id);

            if (avgChampPlacementInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            avgChampPlacementInDb.character_id = avgChampPlacementInDb.character_id;
            avgChampPlacementInDb.avgPlacement = avgChampPlacementInDb.avgPlacement;
        }

        // DELETE /api/avgChampPlacement/1
        [HttpDelete]
        public void DeleteAvgChampPlacement(string character_id)
        {
            var avgChampPlacementInDb = _context.AvgChampPlacements.SingleOrDefault(x => x.character_id == character_id);

            if (avgChampPlacementInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.AvgChampPlacements.Remove(avgChampPlacementInDb);
            _context.SaveChanges();
        }

    }
}
