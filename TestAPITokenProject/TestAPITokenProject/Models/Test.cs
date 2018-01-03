using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProjectCom;

namespace TestAPITokenProject.Models
{

    public class Test
    {
        //BlogsEntities dbContext = new BlogsEntities();
        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserNickName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsBlocked { get; set; }
        public string UserPhoto { get; set; }
        public int FKRoleID { get; set; }

        public string RoleName { get; set; }

        public void oMappForEntity(User oUser )
        {
            this.UserID        = oUser.UserID;
            this.UserFirstName = oUser.UserFirstName;
            this.UserLastName  = oUser.UserLastName;
            this.UserNickName  = oUser.UserNickName;
            this.UserPassword  = oUser.UserPassword;
            this.UserEmail     = oUser.UserEmail;
            this.UserPhoto     = oUser.UserPhoto;
            this.UserIsBlocked = oUser.UserIsBlocked;
            this.FKRoleID      = oUser.FKRoleID;
            this.RoleName      = oUser.UserRole.RoleName;
        }

        public List<Test> lMappListOfEntity(List<User> lUser)
        {
            List<Test> lTest = new List<Test>();

            foreach (var item in lUser)
            {

                lTest.Add(new Test()
                {
                    UserID = item.UserID,
                    UserFirstName = item.UserFirstName,
                    UserLastName  = item.UserLastName,
                    UserNickName  = item.UserNickName,
                    UserPassword  = item.UserPassword,
                    UserEmail     = item.UserEmail,
                    UserPhoto     = item.UserPhoto,
                    UserIsBlocked = item.UserIsBlocked,
                    FKRoleID      = item.FKRoleID,
                    RoleName      = item.UserRole.RoleName
                });
            }
            return lTest;
        }
    }



}