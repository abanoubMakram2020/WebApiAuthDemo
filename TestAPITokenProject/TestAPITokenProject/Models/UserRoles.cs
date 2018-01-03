using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProjectCom;

namespace TestAPITokenProject.Models
{
    public class UserRoles
    {
        public int nRoleID { set; get; }
        public string sRoleName { set; get; }
        public bool bRoleIsAdmin { set; get; }
        public bool bRoleIsActive { set; get; }


        public List<UserRoles> lMappListOfEntity(List<UserRole> lUserRole)
        {
            List<UserRoles> lUserRoles = new List<UserRoles>();

            foreach (var item in lUserRole)
            {

                lUserRoles.Add(new UserRoles()
                {
                    nRoleID = item.RoleID,
                    sRoleName = item.RoleName,
                    bRoleIsAdmin = item.RoleIsAdmin,
                    bRoleIsActive = item.RoleIsActive,
      
                });
            }
            return lUserRoles;
        }
    }


}