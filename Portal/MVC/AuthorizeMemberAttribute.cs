using System.Web;
using System.Web.Mvc;
using Data.DTOs;
using System.Web.Routing;

namespace MVC
{
    public class AuthorizeMemberAttribute : AuthorizeAttribute
    {
        public int Permission { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var member = (LoginDTO) httpContext.Session?["Member"];

            // Permission = 0 znaci da se ne trazi nikakva posebna permisija,
            // tj. zahteva se samo da je clan ulogovan (ostale permisije krecu od 1 navise)
            if (member != null)
            {
                if (Permission == 0)
                    return true;
                else
                {
                    return member.Permissions.Contains(Permission);
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Access",
                        action = "Index"
                    })
                );
        }
    }
}