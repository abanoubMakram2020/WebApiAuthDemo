using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestAPITokenProject.Models;
using TestProjectCom;

namespace TestAPITokenProject.Controllers
{
    public class UsersController : ApiController
    {
        BlogsEntities dbContext = new BlogsEntities();


        [Authorize]
        [HttpGet]
        public List<Test> GetAllUsers()
        {
            Test oTest = new Test();

            return oTest.lMappListOfEntity(dbContext.Users.ToList());
        }

        [Authorize]
        [HttpGet]
        public Test GetByID(int nId)
        {
            Test oTest = new Test();
            oTest.oMappForEntity(dbContext.Users.Where(i => i.UserID == nId).FirstOrDefault());
            return oTest;
          //  return dbContext.Users.Where(i => i.UserID == nId).FirstOrDefault();
        }

        [Authorize]
        [HttpPost]
        public bool addUser([FromBody]User oUser)
        {
            try
            {
                if (bEmailIsExist(oUser.UserEmail, null))
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " This Email Already Exist");
                    return false;
                }
                else
                {
                    dbContext.Users.Add(oUser);

                    if (dbContext.SaveChanges() == 1)
                    {
                        // return Request.CreateResponse(HttpStatusCode.Created);
                        return true;
                    }
                    else
                    {
                        // return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " Can not to add this User");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return false;
            }
        }

        [Authorize]
        [HttpPut]
        public bool editUser(int nId, [FromBody]User oUser)
        {
            try
            {

                var vUser = dbContext.Users.FirstOrDefault(c => c.UserID == nId);
                if (vUser == null)
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    //    "User with Id " + nId.ToString() + " Not Found To Update");
                    return false;
                }
                else
                {
                    if (bEmailIsExist(oUser.UserEmail, nId))
                    {
                        //return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " This Email Already Exist");
                        return false;
                    }
                    else
                    {

                        vUser.UserFirstName = oUser.UserFirstName;
                        vUser.UserLastName = oUser.UserLastName;
                        vUser.UserNickName = oUser.UserNickName;
                        vUser.UserPassword = oUser.UserPassword;
                        vUser.UserPhoto = oUser.UserPhoto;
                        vUser.UserRole = oUser.UserRole;
                        vUser.UserIsBlocked = oUser.UserIsBlocked;
                        dbContext.SaveChanges();
                        // return Request.CreateResponse(HttpStatusCode.OK, vUser);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return false;
            }
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete]
        public bool Delete(int nId)
        {
            try
            {
                var vUser = dbContext.Users.FirstOrDefault(c => c.UserID == nId);
                if (vUser == null)
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    //    "User with Id " + nId.ToString() + " Not Found To delete");
                    return false;
                }
                else
                {
                    if (bUserHasPosts(nId))
                    {
                        // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id " + nId.ToString() + " Has Pots Please Delete related posts firstly");
                        return false;
                            
                    }
                    else
                    {

                        dbContext.Users.Remove(vUser);
                        dbContext.SaveChanges();
                        //return Request.CreateResponse(HttpStatusCode.OK);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return false;

            }
        }

        internal bool bEmailIsExist(string sEmail,int? nID)
        {
            User ouser;
            if (nID!=null)
            {
                ouser = dbContext.Users.FirstOrDefault(x => x.UserEmail == sEmail && x.UserID!=nID);
            }
            else
            {
                ouser = dbContext.Users.FirstOrDefault(x => x.UserEmail == sEmail );
            }

            if (ouser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool bUserHasPosts(int nId)
        {
            var vPost = dbContext.Posts.FirstOrDefault(x => x.FKUserID == nId);

            if (vPost != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
