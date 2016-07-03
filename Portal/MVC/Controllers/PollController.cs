using Data.DataClasses;
using Data.Entities;
using MVC.Models;
using System.Collections.Generic;
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

        [HttpPost]
        public JsonResult GetVoterNames(int pollOptionId)
        {
            Poll poll = Polls.GetPollForPollOption(pollOptionId);
            if (poll.HideVoters)
            {
                return Json(new { hidden = true });
            }
            List<Member> members = Polls.GetVotersForPollOption(pollOptionId);
            List<string> names = new List<string>();
            foreach (var m in members)
            {
                if (string.IsNullOrWhiteSpace(m.Nickname))
                {
                    names.Add(m.Name + " " + m.Surname);
                }
                else
                {
                    names.Add(m.Name + " " + m.Surname + " (" + m.Nickname + ")");
                }
            }
            return Json(new { hidden = false, voterNames = names });
        }
    }
}