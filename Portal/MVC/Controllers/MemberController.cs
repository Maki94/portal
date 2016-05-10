using System.Web.Mvc;
using MVC.Models;
using MVC.Models.Members;

namespace MVC.Controllers
{
    public class MemberController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index()
        {
            MemberProfileModel model = MemberProfileModel.Load(1);
            return View(model);
        }
        public ActionResult AllMembers()
        {
            var model = Data.Entities.Members.GetAllMember();
            return View(model);
        }

        public ActionResult Profile()
        {
            MemberProfileModel model = MemberProfileModel.Load(1);
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}