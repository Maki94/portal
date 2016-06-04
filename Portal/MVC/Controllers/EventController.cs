using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}