using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MessengerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}