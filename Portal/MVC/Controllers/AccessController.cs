﻿using MVC.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

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

                if (Session["Member"] != null)
                    return RedirectToAction("Index", "Member");

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

                    return RedirectToAction("Index", "Member");
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

        //[AuthorizeMember(Permission = (int)Data.Enumerations.Permission.UploadDefaultPicture)]
        public ActionResult UploadDefaultPicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadDefaultPicture(string name, HttpPostedFileBase file)
        {
            if (file != null)
            {
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
                Data.Entities.DefaultPictures.UploadPicture(array, name);
            }

            return RedirectToAction("UploadDefaultPicture");
        }
    }
}