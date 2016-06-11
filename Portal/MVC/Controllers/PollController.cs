using Data.Entities;
using MVC.ViewModels.Poll;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
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
        public void AddVote(int memberId, int[] pollOptionIds)
        {
            Polls.AddVote(memberId, pollOptionIds);
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