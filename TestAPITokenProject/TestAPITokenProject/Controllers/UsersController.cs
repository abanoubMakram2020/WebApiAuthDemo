using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
        }//

        [Authorize]
        [HttpPost]
        public int addUser([FromBody]User oUser)
        {
            try
            {
                if (bEmailIsExist(oUser.UserEmail, null))
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " This Email Already Exist");
                    return 0;
                }
                else
                {
                    string sFileName = sSaveBase64Files(oUser.UserPhoto);
                    oUser.UserPhoto = sFileName;
                    dbContext.Users.Add(oUser);
                    
                    if (dbContext.SaveChanges() == 1)
                    {
                        // return Request.CreateResponse(HttpStatusCode.Created);
                        return 1;
                    }
                    else
                    {
                        // return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " Can not to add this User");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return -1;
            }
        }

        [Authorize]
        [HttpPut]
        public int editUser(int nId, [FromBody]User oUser)
        {
            try
            {

                var vUser = dbContext.Users.FirstOrDefault(c => c.UserID == nId);
                if (vUser == null)
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    //    "User with Id " + nId.ToString() + " Not Found To Update");
                    return -1;
                }
                else
                {
                    if (bEmailIsExist(oUser.UserEmail, nId))
                    {
                        //return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, " This Email Already Exist");
                        return 0;
                    }
                    else
                    {

                        vUser.UserFirstName = oUser.UserFirstName;
                        vUser.UserLastName = oUser.UserLastName;
                        vUser.UserNickName = oUser.UserNickName;
                        vUser.UserPassword = oUser.UserPassword;
                        //vUser.UserPhoto = oUser.UserPhoto;

                        string sFileName = sSaveBase64Files(oUser.UserPhoto);
                        vUser.UserPhoto = sFileName;

                        vUser.UserRole = oUser.UserRole;
                        vUser.UserIsBlocked = oUser.UserIsBlocked;
                        vUser.CityId = oUser.CityId;
                        vUser.CreateDate = oUser.CreateDate;

                        dbContext.SaveChanges();
                        // return Request.CreateResponse(HttpStatusCode.OK, vUser);
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return -2;
            }
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete]
        public int Delete(int nId)
        {
            try
            {
                var vUser = dbContext.Users.FirstOrDefault(c => c.UserID == nId);
                if (vUser == null)
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    //    "User with Id " + nId.ToString() + " Not Found To delete");
                    return 0;
                }
                else
                {
                    dbContext.Users.Remove(vUser);
                    dbContext.SaveChanges();
                    //return Request.CreateResponse(HttpStatusCode.OK);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.ToLower().Contains("delete statement conflicted with the reference constraint"))
                {
                    return -1;
                }
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return -2;

            }
        }

        internal bool bEmailIsExist(string sEmail, int? nID)
        {
            User ouser;
            if (nID != null)
            {
                ouser = dbContext.Users.FirstOrDefault(x => x.UserEmail == sEmail && x.UserID != nID);
            }
            else
            {
                ouser = dbContext.Users.FirstOrDefault(x => x.UserEmail == sEmail);
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

        internal string sSaveBase64Files(string sFileData)
        {
            string sFileName = null;
            int nIndexOf_base64 = -1;
            if (sFileData != null)
            {
                string sFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images");
                if (sFileData.StartsWith("data:") && (nIndexOf_base64 = sFileData.IndexOf(";base64,")) > -1)
                {
                    int nIndexOfExtention = sFileData.IndexOf("/") + 1;
                    sFileName = DateTime.Now.ToString("ddmmyyyyhhmmssfff") + "." + sFileData.Substring(nIndexOfExtention, nIndexOf_base64 - nIndexOfExtention);
                    sFileData = sFileData.Replace('-', '+').Replace('_', '/');
                    sFileData = sFileData.Substring(nIndexOf_base64 + 8);
                    File.WriteAllBytes(sFilePath + "/" + sFileName, Convert.FromBase64String(sFileData));
                }
                else
                {
                    sFileName = sFileData;
                    //if (File.Exists(sFilePath + "/" + sFileData) == false)
                    //{
                    //    File.
                    //}
                }
            }
            return sFileName;
        }
    }
}
