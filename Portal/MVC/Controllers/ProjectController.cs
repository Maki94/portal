using Data.Entities;
using MVC.Models;
using System;
using System.IO;
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

        [HttpPost]
        public JsonResult SearchProjects(string term)
        {
            var resultIds = Projects.SearchProjects(term);
            return Json(resultIds);
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
            byte[] array = null;
            if (model.Logo != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.Logo.InputStream.Position = 0;
                    model.Logo.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
            }

            Projects.AddProject(model.Name, model.Website, array, model.StartDate,
                                model.FinishDate, model.Description, model.Place, id);

            return RedirectToAction("AllProjects");
        }
    }
}