using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemberEdit(int memberId)
        {
            //return View(new MemberEditViewModel());
            return View();
        }
    }
}