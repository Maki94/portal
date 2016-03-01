using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class LeavesController : Controller
    {
        // GET: Leaves
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowLeaves()
        {
            return View();
        }

        public ActionResult AddLeave()
        {
            return View();
        }

        public ActionResult RespondToRequest()
        {
            return View();
        }

        public ActionResult ShowComment()
        {
            return View();
        }
    }
}