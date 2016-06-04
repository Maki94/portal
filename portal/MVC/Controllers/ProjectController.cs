using MVC.ViewModels.Projects;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            return View(new ProjectListViewModel());
        }
    }
}