using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data2;

namespace WebApplication.Controllers
{
    public class TeamsController : Controller
    {
        // GET: Teams
        public ActionResult Index()
        {
            //prikaze sve timove
            return View();
        }

        public ActionResult ShowTeamInfo(int teamID)
        {
            //prikazuje naziv tima i njegove radnike 
            return View();
        }

        public ActionResult AddEmpToTeam(int empID, int teamID)
        {
            //salje na fromu za dodavanje radnika timu
            return View();
        }

        public ActionResult EditName(int teamID)
        {
            //menja ime timu
            return View();
        }



        public ActionResult ShowEmpsTeams(int empID)
        {
            //prikaze sve timove kojima pripada radnik 
            return View();
        }

        public ActionResult AddTeam()
        {
            //ide na formu za dodavanje tima
            return RedirectToAction("");   
        }



        public ActionResult RemoveTeam(int teamID)
        {
            //setuje endDate na Today
            return RedirectToAction("index");
        }
    }
}