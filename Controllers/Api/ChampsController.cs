using EZTFT.Models;
using EZTFT.Models.MatchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EZTFT.Controllers.Api
{
    public class ChampsController : ApiController
    {

        private ApplicationDbContext _context;

        public ChampsController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/champs
        public IEnumerable<Champ> GetChamps()
        {
            return _context.Champs.ToList();
        }  

        // GET /api/champ
        public Champ GetChamp(int id)
        {
            var champ = _context.Champs.SingleOrDefault(c => c.id == id);

            if (champ == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return champ;
            }
        }

        // POST /api/champs
        [HttpPost]
        public Champ CreateChamp(Champ champ)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Champs.Add(champ);
            _context.SaveChanges();

            return champ;
        }

        // PUT /api/champ/1
        [HttpPut]
        public void UpdateChamp(int id, Champ champ)
        {
            if (ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var champInDb = _context.Champs.SingleOrDefault(c => c.id == id);

            if (champInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            champInDb.character_id = champ.character_id;
            champInDb.placement = champ.placement;
        }

        // DELETE /api/champ/1
        [HttpDelete]
        public void DeleteChamp(int id)
        {
            var champInDb = _context.Champs.SingleOrDefault(c => c.id == id);

            if (champInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Champs.Remove(champInDb);
            _context.SaveChanges();
        }
    }
}
