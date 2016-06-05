using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PollController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}