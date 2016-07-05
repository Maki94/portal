using Data.Entities;
using MVC.Models;
using System;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("AllProjects");
        }

        public ActionResult AllProjects()
        {
            return View(new ProjectListModel());
        }

        public ActionResult Details(int id)
        {
            ProjectModel model = ProjectModel.Load(id);
            return View(model);
        }

        public ActionResult Add()
        {
            return View(new ProjectAddModel());
        }

        [HttpPost]
        public ActionResult Add(ProjectAddModel model)
        {
            int id = Int32.Parse(model.TeamIdString);
            return RedirectToAction("AllProjects");
        }
    }
}