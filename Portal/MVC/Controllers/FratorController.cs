using System.Web.Mvc;

namespace MVC.Controllers
{
    public class FratorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile(int id)
        {
            return View();
        }
    }
}