using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProjectController : Controller
    {
        [AuthorizeMember(Permission = "Logged")]
        public ActionResult Index()
        {
            var m = Data.Entities.Projects.GetAllProjects();
            return View(m);
        }
    }
}