using Data;
using Data.DataClasses;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC.Controllers
{
    public class AccessController : Controller
    {
        // GET: Login
        public ActionResult Index(bool logOut = false)
        {
            if (logOut)
                Session["Member"] = null;

            if (Session["Member"] != null)
                return View();

            return View(new Data.DTOs.LoginTransporterDTO());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(MemberRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = new Member { Gmail = model.Gmail, Password = model.Password, RoleId = 1 };
                using (DataContext dc = new DataContext())
                {
                    dc.Members.Add(member);
                    dc.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = member.Gmail + " korisnik uspesno registrovan!";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberLoginViewModel model)
        {
            try
            {
                Data.DTOs.LoginTransporterDTO  ltd = new Data.DTOs.LoginTransporterDTO
                {
                    Gmail = model.Gmail,
                    Password = model.Password,
                    RememberMe = model.RememberMe,
                };
                Data.DTOs.LoginDTO member = Data.Entities.Login.CreateLoginDTO(ltd);
                Session["Member"] = member;
                Session.Timeout = member.RememberMe ? 525600 : 20;

                return RedirectToAction("Profile", "Member");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Something went wrong." });
            }
        }
    }
}