using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAPITokenProject.Controllers
{
    public class UserRolesController : ApiController
    {
        // GET: api/UserRoles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserRoles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserRoles
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserRoles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserRoles/5
        public void Delete(int id)
        {
        }
    }
}
