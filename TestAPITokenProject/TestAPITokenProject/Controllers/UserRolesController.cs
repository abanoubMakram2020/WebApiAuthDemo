using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TestAPITokenProject.Models;
using TestProjectCom;

namespace TestAPITokenProject.Controllers
{
    public class UserRolesController : ApiController
    {
        BlogsEntities dbContext = new BlogsEntities();

        [Authorize]
        [HttpGet]
        //[EnableCors(origins: "http://localhost:4200", headers: null, methods: null)]
        public List<UserRoles> GetAllUserRoles()
        {
            UserRoles oUserRoles = new UserRoles();

            return oUserRoles.lMappListOfEntity(dbContext.UserRoles.ToList());
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
