using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TestProjectCom;

namespace TestAPITokenProject
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string sGrantType = context.Parameters["grant_type"];
            if (sGrantType.ToLower() == "password")
            {
                string sUserEmail = context.Parameters["username"];
                string sPassword = context.Parameters["password"];

                if (new BlogsEntities().Users.Any(x => x.UserEmail.ToLower() == sUserEmail.ToLower() && x.UserPassword == sPassword))
                {
                   // System.Threading.Thread.Sleep(20000);
                    context.Validated();
                }
                else
                {
                    context.Rejected();
                }
            }
            else if (sGrantType.ToLower() == "refresh_token")
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var user = new BlogsEntities().Users.FirstOrDefault(x => x.UserEmail == context.UserName);
            if (user.FKRoleID==2)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                // identity.AddClaim(new Claim(ClaimTypes.Role, "teacher"));
                identity.AddClaim(new Claim("useremail", user.UserEmail));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserEmail));


                context.Validated(identity);
            }
            else if (user.FKRoleID == 3)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "teacher"));
                identity.AddClaim(new Claim("useremail", user.UserEmail));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserEmail));
                identity.AddClaim(new Claim("Actions", "GetAllUsers"));
                identity.AddClaim(new Claim("Actions", "GetForTeacher"));
                identity.AddClaim(new Claim("Actions", "GetForAuthenticate"));
                context.Validated(identity);
            }
            else if (user.FKRoleID == 5)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "student"));
                identity.AddClaim(new Claim("useremail", user.UserEmail));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserEmail));
                identity.AddClaim(new Claim("Actions", "GetForStudent"));
                identity.AddClaim(new Claim("Actions", "GetForAuthenticate"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided useremail and/or password is incorrect");
            }
        }
    }
}