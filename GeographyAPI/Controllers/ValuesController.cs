using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GeographyAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var strs = new string[] { "value1", "value2" };
            return Request.CreateResponse<Array>(HttpStatusCode.OK, strs);
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse<string>(HttpStatusCode.OK, $"value:{id}");
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            return Request.CreateResponse<string>(HttpStatusCode.Created, value);
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse<int>(HttpStatusCode.OK, id);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse<int>(HttpStatusCode.OK, id);
        }
    }
}
