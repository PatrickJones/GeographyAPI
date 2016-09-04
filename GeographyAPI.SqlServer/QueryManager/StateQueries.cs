using GeographyAPI.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyAPI.SqlServer.QueryManager
{
    public class StateQueries : QueryBase
    {
        private GlobalStandardsEntities db = new GlobalStandardsEntities();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
