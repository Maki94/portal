using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            var m = Data.Entities.Projects.GetAllProjects();
            return View(m);
        }
    }
}