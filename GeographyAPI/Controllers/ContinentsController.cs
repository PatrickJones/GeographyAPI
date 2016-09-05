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
using GeographyAPI.SqlServer.QueryManager;
using System.Threading.Tasks;

namespace GeographyAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContinentsController : ApiController
    {
        //private GlobalStandardsEntities db = new GlobalStandardsEntities();
        private ContinentQueries cq = new ContinentQueries();

        public ContinentsController(DbContext context)
        {
            //this.db = context as GlobalStandardsEntities;
            //db.Configuration.LazyLoadingEnabled = false;
            cq = new ContinentQueries(context);
        }

        public ContinentsController()
        {
            //db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Continents
        public async Task<IEnumerable<Continent>> GetContinentsAsync()
        {
            return await cq.GetAllContinentsAsync();
        }

        // GET: api/Continents/5
        [ResponseType(typeof(Continent))]
        public async Task<IHttpActionResult> GetContinentAsync(int id)
        {
            Continent continent = await cq.GetContinentWithCountriesAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            return Ok(continent);
        }

        
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}