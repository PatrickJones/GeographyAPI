using GeographyAPI.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GeographyAPI.SqlServer.QueryManager
{
    public class ContinentQueries : IDisposable
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        public ContinentQueries(DbContext context)
        {
            db = (GlobalStandardsEntities)context;
        }

        public ContinentQueries()
        {}

        public async Task<ICollection<Continent>> GetAllContinentsAsync()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Continents.ToListAsync();
        }

        public async Task<Continent> GetContinentAsync(int continentId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Continents.Where(w => w.ContinentId == continentId).FirstOrDefaultAsync();

        }

        public async Task<Continent> GetContinentAsync(string continentName)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Continents.Where(w => w.Name == continentName).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }
    }
}
