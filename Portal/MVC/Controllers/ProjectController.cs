using MVC.Models;
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
    }
}