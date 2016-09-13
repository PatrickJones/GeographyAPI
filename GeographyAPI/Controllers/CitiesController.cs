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

        [Route("geography/cities")]
        public async Task<ICollection<City>> GetCitiesAsync()
        {
            return await cq.GetAllCitiesAsync();
        }

        [Route("geography/cities/{cityId:int}")]
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> GetCityAsync(int cityId)
        {
            City city = await cq.GetCityAsync(cityId);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [Route("geography/cities/{cityName}")]
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> GetCityByNameAsync(string cityName)
        {
            City city = await cq.GetCityAsync(cityName);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}