using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
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