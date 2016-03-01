using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers.Content
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowProfile(int empID)
        {
            return View();
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        public ActionResult AddKid(int empID)
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
        
        public ActionResult ChangeStatus(int empID )
        {
            return View();
        }
    }
}