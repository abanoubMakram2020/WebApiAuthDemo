using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;

namespace TestAPITokenProject
{
    public class AuthorizeAttribute: System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //  var vControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var vActionName = actionContext.ActionDescriptor.ActionName;

            ClaimsIdentity oClaimsIdentity;
            var vHttpContext = HttpContext.Current;

            if (!(vHttpContext.User.Identity is ClaimsIdentity))
            {
                return false;
            }


            oClaimsIdentity = vHttpContext.User.Identity as ClaimsIdentity;

            var vRoles = oClaimsIdentity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x=>x.Value);
            var vActionNames = oClaimsIdentity.Claims.Where(x => x.Type == "Actions").Select(x => x.Value);

            foreach (var vRole in vRoles)
            {
                if (vRole.ToLower()=="admin")
                {
                    return true;
                }
            }


            if (vActionNames.Contains(vActionName))
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