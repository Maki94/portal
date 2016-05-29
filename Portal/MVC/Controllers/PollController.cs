using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PollController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}