using Data.Entities;
using MVC.Models;
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
            return View(new PollListModel());
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
            return View(new PollAddModel());
        }

        [HttpPost]
        public ActionResult Add(PollAddModel model)
        {
            Polls.AddPoll(model.Topic, model.Description, model.AllowMultiple, model.HideResultsUntilFinished,
                            model.HideVoters, model.EndDate, model.EndTime, model.Options, MemberSession.GetMemberId());

            return RedirectToAction("AllPolls");
        }

        public ActionResult Edit(int id)
        {
            return View(PollAddModel.GetEditModel(id));
        }
    }
}