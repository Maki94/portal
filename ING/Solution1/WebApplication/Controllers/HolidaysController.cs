using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HolidaysController : Controller
    {
        // GET: Holidays
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int year, int filterNzm)
        {
            //prikazuje praznike koji odgovaraju filterima
            return View();
        }

        public ActionResult AddHoliday()
        {
            //salje na formu za dodavanje praznika
            return View();
        }
    }
}