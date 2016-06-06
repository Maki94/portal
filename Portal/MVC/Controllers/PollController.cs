using MVC.ViewModels.Poll;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PollController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("AllPolls");
        }

        public ActionResult AllPolls()
        {
            return View(new PollListViewModel());
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