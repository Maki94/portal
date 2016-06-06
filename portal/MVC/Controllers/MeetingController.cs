using System.Web.Mvc;

namespace MVC.Controllers
{
    public class MeetingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Meeting(int id)
        {
            return View();
        }
    }
}