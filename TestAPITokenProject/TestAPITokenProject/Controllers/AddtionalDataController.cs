using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProjectCom;

namespace TestAPITokenProject.Controllers
{
    public class AddtionalDataController : ApiController
    {
        BlogsEntities dbContext = new BlogsEntities();

        [Authorize]
        [HttpGet]
        // GET: AddtionalData

        public object GetCities(int CountryID)
        {
            return dbContext.Cities.Where(x => x.CountryId == CountryID).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId
            });
        }


    }
}
