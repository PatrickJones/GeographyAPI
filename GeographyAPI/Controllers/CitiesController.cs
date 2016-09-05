using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GeographyAPI.SqlServer.EF;
using GeographyAPI.SqlServer.QueryManager;

namespace GeographyAPI.Controllers
{
    public class CitiesController : ApiController
    {
        private CityQueries cq = new CityQueries();

        public CitiesController(DbContext context)
        {
            this.cq = new CityQueries(context);
        }

        public CitiesController() { }


        // GET: api/Cities
        public async Task<ICollection<City>> GetCitiesAsync()
        {
            return await cq.GetAllCitiesAsync();
        }

        // GET: api/Cities/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> GetCityAsync(int id)
        {
            City city = await cq.GetCityAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}