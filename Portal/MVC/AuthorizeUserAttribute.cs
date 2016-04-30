using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC
{
    public class AuthorizeMemberAttribute : AuthorizeAttribute
    {
        public string Permission { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // ovo koristi default identity i authentication klase
            // koje mi u ovom slucaju ne koristimo

            //var authorized = base.AuthorizeCore(httpContext);
            //if (!authorized)
            //{
            //    // if member is not logged in, no need to check further
            //    return false;
            //}

            Data.DTOs.LoginDTO member = (Data.DTOs.LoginDTO)httpContext.Session["Member"];

            if (member != null && member.Permissions.Contains(this.Permission))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Access",
                        action = "Index",
                    })
                    );
        }
    }
}