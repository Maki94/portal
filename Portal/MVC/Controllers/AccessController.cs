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
        public ActionResult Index(bool logOut = false, string message = null)
        {
            try
            {
                if (logOut)
                    Session["Member"] = null;

                // ako je ulogovan stavljamo ga trenutno na svoj profil
                // treba to kasnije da se promeni da bude na neki homepage
                if (Session["Member"] != null)
                    return RedirectToAction("Profile", "Member");

                ViewBag.message = message;

                return View(new MemberLoginViewModel());
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Access");
            }
        }

        [HttpPost]
        public ActionResult Index(MemberLoginViewModel model)
        {
            try
            {
                Data.DTOs.LoginDTO member = Data.Entities.Login.CreateLoginDTO(model.Gmail, model.Password, model.RememberMe);

                if (member.loginStatus == (int)Data.Enumerations.LoginStatus.Successful)
                {
                    Session["Member"] = member;

                    Session.Timeout = member.RememberMe ? 525600 : 20;

                    return RedirectToAction("Profile", "Member");
                }
                else if (member.loginStatus == (int)Data.Enumerations.LoginStatus.IncorrectPassword)
                {
                    return RedirectToAction("Index", new { message = "Pogresna lozinka." });
                }
                else
                {
                    return RedirectToAction("Index", new { message = "Ne postoji taj nalog." });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Nesto ne valja" });
            }
        }

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(MemberRegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var member = new Member { Gmail = model.Gmail, Password = model.Password, RoleId = 1 };
        //        using (DataContext dc = new DataContext())
        //        {
        //            dc.Members.Add(member);
        //            dc.SaveChanges();
        //        }
        //        ModelState.Clear();
        //        ViewBag.Message = member.Gmail + " korisnik uspesno registrovan!";
        //    }
        //    return View();
        //}

        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(MemberLoginViewModel model)
        //{
        //    try
        //    {
        //        Data.DTOs.LoginTransporterDTO  ltd = new Data.DTOs.LoginTransporterDTO
        //        {
        //            Gmail = model.Gmail,
        //            Password = model.Password,
        //            RememberMe = model.RememberMe,
        //        };
        //        Data.DTOs.LoginDTO member = Data.Entities.Login.CreateLoginDTO(ltd);
        //        Session["Member"] = member;
        //        Session.Timeout = member.RememberMe ? 525600 : 20;

        //        return RedirectToAction("Profile", "Member");
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index", new { message = "Something went wrong." });
        //    }
        //}
    }
}