using GeographyAPI.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyAPI.SqlServer.QueryManager
{
    public class CountryQueries : IDisposable
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();

        public CountryQueries(DbContext context)
        {
            db = (GlobalStandardsEntities)context;
        }

        public CountryQueries() { }
        public async Task<ICollection<Country>> GetAllCountriesAsync()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryAsync(int countryId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Countries.Where(w => w.CountryId == countryId).FirstOrDefaultAsync();

        }

        public async Task<Country> GetCountinentAsync(string countryName)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Countries.Where(w => w.Name == countryName).FirstOrDefaultAsync();
        }

        public async Task<Country> GetCountryWithStatesAsync(int countryId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return await db.Countries.Include("States").Where(w => w.CountryId == countryId).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }
    }
}
