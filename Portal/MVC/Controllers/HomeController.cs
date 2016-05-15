using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeMember(Permission = (int)Data.Enumerations.Permission.ViewAboutPage)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}