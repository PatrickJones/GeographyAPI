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
using System.Collections;

namespace GeographyAPI.Controllers
{
    public class StatesController : ApiController
    {
        private StateQueries sq = new StateQueries();

        public StatesController(DbContext context)
        {
            sq = new StateQueries(context);
        }

        public StatesController() { }

        // GET: api/States
        public async Task<ICollection<State>> GetStatesAsync()
        {
            return await sq.GetAllStatesAsync();
        }

        // GET: api/States/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> GetStateAsync(int id)
        {
            State state = await sq.GetStateAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }
    }
}