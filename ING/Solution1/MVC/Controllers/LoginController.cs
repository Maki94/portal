using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data2;
using Data2.DTOs;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(Boolean logOut = false, String message = null)
        {
            try
            {

                //Data2.random.deleteAll();
                //Data2.random.fillTablesRandomly();
                //Data2.random.testIgranje();
                //Data2.Enums.addPermissionsEnums();
                //Data2.RolesPermissions.addRolesPermisons();
                //Data2.DTOs.LoginTransporterDTO model = new Data2.DTOs.LoginTransporterDTO();
                if (logOut)
                    Session["User"] = null;


                if (Session["User"] != null)
                {
                    return RedirectFromLoginPage();
                }

                ViewBag.message = message;

                return View(new Data2.DTOs.LoginTransporterDTO());

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult RedirectFromLoginPage()
        {
            LoginDTO user = GetUser.LoginDTO();
            switch (user.Role)
            {
                case (int)Data2.Enums.Roles.Manager: return RedirectToAction("Index", "Leaves");
                case (int)Data2.Enums.Roles.Employee: return RedirectToAction("Index", "Leaves", new { empFilter = user.EmployeeID });
               
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(Data2.DTOs.LoginTransporterDTO model)
        {
            try
            {

                Data2.DTOs.LoginDTO user = Data2.Login.createLoginDTO(model);

                if (user.loginStatus == (int)Data2.Enums.LoginStatus.Successful)
                {

                    

                    Session["User"] = user;
                    Session["EmpSort"] = "";
                    Session["LeaveSort"] = "";
                    Session["TeamSort"] = "";
                    Session["HolidaySort"] = "";
                    Session["TeamsOfEmpSort"] = "";

                    Session.Timeout = user.RememberMe ? 525600 : 20;
                    if (model.Password == "123456")
                    {
                        return RedirectToAction("ChangePassword",new {empID = user.EmployeeID});
                    }
                    return RedirectFromLoginPage();

                }
                else if (user.loginStatus == (int)Data2.Enums.LoginStatus.IncorrectPassword)
                {
                    return RedirectToAction("Index", new { message = "Password is incorrect" });
                }
                else
                {
                    return RedirectToAction("Index", new { message = "Username does not exist" });
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new {message = "Something went wrong"});
            }
        }

        public ActionResult LoginAsManager()
        {
            try
            {

                Data2.DTOs.LoginTransporterDTO ltd = new Data2.DTOs.LoginTransporterDTO
                {
                    Username = "weil",
                    Password = "weil",
                    RememberMe = true
                };
                Data2.DTOs.LoginDTO user = Data2.Login.createLoginDTO(ltd);
                Session["User"] = user;
                Session["EmpSort"] = "";
                Session["LeaveSort"] = "";
                Session["TeamSort"] = "";
                Session["HolidaySort"] = "";
                Session["TeamsOfEmpSort"] = "";

                Session.Timeout = 525600;

                return RedirectFromLoginPage();


            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { message = "Something went wrong" });
            }
        }

        public ActionResult LoginAsEmployee()
        {
            try
            {

                Data2.DTOs.LoginTransporterDTO ltd = new Data2.DTOs.LoginTransporterDTO
                {
                    Username = "lgxh",
                    Password = "lgxh",
                    RememberMe = true
                };
                Data2.DTOs.LoginDTO user = Data2.Login.createLoginDTO(ltd);
                Session["User"] = user;
                Session["EmpSort"] = "";
                Session["LeaveSort"] = "";
                Session["TeamSort"] = "";
                Session["HolidaySort"] = "";
                Session["TeamsOfEmpSort"] = "";

                Session.Timeout = user.RememberMe ? 525600 : 20;

                return RedirectFromLoginPage();


            }
            catch (Exception)
            {

                return RedirectToAction("Index", new { message = "Something went wrong" });
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        public ActionResult ChangePassword(int empID)
        {
            if (CheckPermission.CheckUserID(empID))
            {
                ChangePasswordDTO model = Create.ChangePassDTO(empID);
                return View(model);
            }
            else
            {
                return RedirectFromLoginPage();
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordDTO model)
        {
            if(model.password==model.oldPassword && model.newPassword==model.repeatPassword)
                Employees.changePassword(model.employeeID,model.newPassword);

            return RedirectFromLoginPage();

        }
    }
}