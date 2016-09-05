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
    public class CountriesController : ApiController
    {
        private CountryQueries cq = new CountryQueries();

        public CountriesController(DbContext context)
        {
            cq = new CountryQueries(context);
        }

        public CountriesController() { }

        // GET: api/Countries
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await cq.GetAllCountriesAsync();
        }

        // GET: api/Countries/5
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> GetCountryAsync(int id)
        {
            Country country = await cq.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }
    }
}