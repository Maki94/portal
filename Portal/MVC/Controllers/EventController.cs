using System.Web.Mvc;
using System.Web.UI.WebControls;

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

        public ActionResult Profile(int id)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}