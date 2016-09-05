using GeographyAPI.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyAPI.SqlServer.QueryManager
{
    public class StateQueries : IDisposable
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        public StateQueries(DbContext context)
        {
            db = (GlobalStandardsEntities)context;
        }

        public StateQueries() { }

        public async Task<ICollection<State>> GetAllStatesAsync()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.States.ToListAsync();
        }

        public async Task<State> GetStateAsync(int stateId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.States.Where(w => w.StateId == stateId).FirstOrDefaultAsync();
        }

        public async Task<State> GetStateAsync(string stateName)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.States.Where(w => w.Name == stateName).FirstOrDefaultAsync();
        }

        public async Task<State> GetStateWithCitiesAsync(int stateId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.States.Include("Countries").Where(w => w.StateId == stateId).FirstOrDefaultAsync();
        }


        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }
    }
}
