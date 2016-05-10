using System.Web;
using System.Web.Mvc;
using Data.DTOs;

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

            var member = (LoginDTO) httpContext.Session?["Member"];

            if (member != null && member.Permissions.Contains(Permission))
            {
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new RedirectToRouteResult(
            //    new RouteValueDictionary(
            //        new
            //        {
            //            controller = "Access",
            //            action = "Login"
            //        })
            //    );
        }
    }
}