//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProjectCom
{
    using System;
    
    public partial class GetUsersById_Result
    {
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
        public bool RoleIsAdmin { get; set; }
        public bool RoleIsActive { get; set; }
    }
}
