using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Data2;
using Data2.DTOs;

namespace MVC
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public int Permission { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
            //    return false;
            //}

            Data2.DTOs.LoginDTO user = (Data2.DTOs.LoginDTO)httpContext.Session["User"];          

            if (user!= null && user.Permissions.Contains(this.Permission))
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
                                controller = "Login",
                                action = "Index",
                                
                            })
                        );
        }

   

    }

    public class CheckPermission
    {
        public static bool CheckIsLogIn()
        {
            return (Data2.DTOs.LoginDTO) HttpContext.Current.Session["User"] != null;
        }
        public static Boolean CheckUserID(int id)
        {

            Data2.DTOs.LoginDTO user = (Data2.DTOs.LoginDTO)HttpContext.Current.Session["User"];
            if (user == null) return false;
            if (user.EmployeeID == id)
                return true;
            return false;

        }

        public static Boolean CheckUserPermisson(int permission)
        {
            Data2.DTOs.LoginDTO user = (Data2.DTOs.LoginDTO)HttpContext.Current.Session["User"];
            if (user == null) return false;
            if (user.Permissions.Contains(permission))            
                return true;            
           
            return false;           
        }

        public static bool CheckIsSessionMenager()
        {
            return ((Data2.DTOs.LoginDTO)HttpContext.Current.Session["User"]).Role == (int)Data2.Enums.Roles.Manager;
        }

        public static bool CheckIsSessionEmployee()
        {
            return ((Data2.DTOs.LoginDTO)HttpContext.Current.Session["User"]).Role == (int)Data2.Enums.Roles.Employee;
        }

        public static bool CheckIsManager(int id)
        {
            return Data2.Employees.getEmplyeeAt(id).roleID == (int)Data2.Enums.Roles.Manager;
        }
        public static bool CheckIsEmployee(int id)
        {
            return Data2.Employees.getEmplyeeAt(id).roleID == (int)Data2.Enums.Roles.Employee;
        }

    }

    public class GetUser
    {

        public static Employee Employee()
        {
            return Data2.Employees.getEmplyeeAt(((LoginDTO)HttpContext.Current.Session["User"]).EmployeeID);
        }

        public static LoginDTO LoginDTO()
        {
            return ((LoginDTO)HttpContext.Current.Session["User"]);
        }

        public static int EmployeeID()
        {
            return ((LoginDTO)HttpContext.Current.Session["User"]).EmployeeID;
        }
    }

    public class ConvertTo
    {
        public static DateTime MyTimeZone(DateTime time)
        {
            return time.AddHours(DateTime.Now.Hour-DateTime.UtcNow.Hour);
        }
    }
}