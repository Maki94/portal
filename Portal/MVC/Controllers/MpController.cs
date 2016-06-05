using System.Web.Mvc;

namespace MVC.Controllers
{
    public class MpController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Izvestaj(int id)
        {
            return View();
        }
    }
}