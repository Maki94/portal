using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MpController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Izvestaj(int id)
        {
            return View();
        }
    }
}