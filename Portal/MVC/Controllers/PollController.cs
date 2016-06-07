using Data.Entities;
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

        [HttpPost]
        public JsonResult AddVote(int memberId, int pollOptionId)
        {
            var success = Polls.AddVote(memberId, pollOptionId);
            return Json(success);
        }

        [HttpPost]
        public JsonResult RemoveVote(int memberId, int pollOptionId)
        {
            var success = Polls.RemoveVote(memberId, pollOptionId);
            return Json(success);
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