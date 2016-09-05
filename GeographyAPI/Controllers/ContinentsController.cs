using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GeographyAPI.SqlServer.EF;
using System.Web.Http.Cors;

namespace GeographyAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContinentsController : ApiController
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        public ContinentsController(DbContext context)
        {
            this.db = context as GlobalStandardsEntities;
            db.Configuration.LazyLoadingEnabled = false;
        }

        public ContinentsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Continents
        public IQueryable<Continent> GetContinents()
        {
            return db.Continents;
        }

        // GET: api/Continents/5
        [ResponseType(typeof(Continent))]
        public IHttpActionResult GetContinent(int id)
        {
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return NotFound();
            }

            return Ok(continent);
        }

        // PUT: api/Continents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContinent(int id, Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != continent.ContinentId)
            {
                return BadRequest();
            }

            db.Entry(continent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Continents
        [ResponseType(typeof(Continent))]
        public IHttpActionResult PostContinent(Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Continents.Add(continent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = continent.ContinentId }, continent);
        }

        // DELETE: api/Continents/5
        [ResponseType(typeof(Continent))]
        public IHttpActionResult DeleteContinent(int id)
        {
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return NotFound();
            }

            db.Continents.Remove(continent);
            db.SaveChanges();

            return Ok(continent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContinentExists(int id)
        {
            return db.Continents.Count(e => e.ContinentId == id) > 0;
        }
    }
}