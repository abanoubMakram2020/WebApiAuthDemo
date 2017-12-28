using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAPITokenProject.Controllers
{
    public class BlogContributersController : ApiController
    {
        // GET: api/BlogContributers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BlogContributers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BlogContributers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BlogContributers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BlogContributers/5
        public void Delete(int id)
        {
        }
    }
}
