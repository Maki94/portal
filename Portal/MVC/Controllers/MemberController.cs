using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class MemberController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index()
        {
            MemberProfileModel model = MemberProfileModel.Load(1);
            return View(model);
        }

        public ActionResult Profile()
        {
            MemberProfileModel model = MemberProfileModel.Load(1);
            return View(model);
        }
    }
}