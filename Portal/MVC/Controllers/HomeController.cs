using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeMember(Permission = "Logged")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeMember(Permission = "ViewAboutPage")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AuthorizeMember(Permission = "Logged")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}