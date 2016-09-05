using GeographyAPI.SqlServer.EF;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyAPI.SqlServer.QueryManager
{
    /// <summary>
    /// City query methods against database
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class CityQueries : IDisposable
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        public CityQueries(DbContext context)
        {
            db = (GlobalStandardsEntities)context;
        }

        public CityQueries() { }

        public async Task<ICollection<City>> GetAllCitiesAsync()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Cities.ToListAsync();
        }

        public async Task<City> GetCityAsync(int cityId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Cities.Where(w => w.CityId == cityId).FirstOrDefaultAsync();

        }

        public async Task<City> GetContinentAsync(string cityName)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Cities.Where(w => w.Name == cityName).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }
    }
}
