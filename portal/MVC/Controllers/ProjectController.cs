using MVC.Models;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            return View(new ProjectListModel());
        }
    }
}