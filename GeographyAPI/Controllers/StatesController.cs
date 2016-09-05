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

namespace GeographyAPI.Controllers
{
    public class StatesController : ApiController
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        // GET: api/States
        public IQueryable<State> GetStates()
        {
            return db.States;
        }

        // GET: api/States/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> GetState(int id)
        {
            State state = await db.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(int id)
        {
            return db.States.Count(e => e.StateId == id) > 0;
        }
    }
}