using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class FratorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile(int id)
        {
            return View();
        }
    }
}