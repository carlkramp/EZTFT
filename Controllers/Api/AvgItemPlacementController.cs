using EZTFT.Models;
using EZTFT.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace EZTFT.Controllers.Api
{
    public class AvgItemPlacementController : ApiController
    {
        private ApplicationDbContext _context;

        public AvgItemPlacementController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/avgItemPlacements
        public IEnumerable<AvgItemPlacement> GetAvgItemPlacements()
        {
            return _context.AvgItemPlacement.ToList();
        }


        // GET /api/avgItemPlacement
        public AvgItemPlacement AvgItemPlacement(int id)
        {
            var avgItemPlacement = _context.AvgItemPlacement.SingleOrDefault(x => x.id == id);

            if (avgItemPlacement == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return avgItemPlacement;
            }
        }

        // POST /api/avgItemPlacements
        [HttpPost]
        public AvgItemPlacement CreateAvgItemPlacement(AvgItemPlacement avgItemPlacement)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.AvgItemPlacement.Add(avgItemPlacement);
            _context.SaveChanges();

            return avgItemPlacement;
        }

        // PUT /api/avgItemPlacement/1
        [HttpPut]
        public void UpdateAvgItemPlacement(int id, Item item)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var avgItemPlacementInDb = _context.AvgItemPlacement.SingleOrDefault(x => x.id == id);

            if (avgItemPlacementInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            avgItemPlacementInDb.id = avgItemPlacementInDb.id;
            avgItemPlacementInDb.avgPlacement = avgItemPlacementInDb.avgPlacement;
        }

        // DELETE /api/avgItemPlacement/1
        [HttpDelete]
        public void DeleteAvgItemPlacement(int id)
        {
            var avgItemPlacementInDb = _context.AvgItemPlacement.SingleOrDefault(x => x.id == id);

            if (avgItemPlacementInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.AvgItemPlacement.Remove(avgItemPlacementInDb);
            _context.SaveChanges();
        }



    }
}