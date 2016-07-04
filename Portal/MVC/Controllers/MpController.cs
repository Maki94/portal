using MVC.Models;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MpController : Controller
    {
        public ActionResult Index()
        {
            MPModel model = MPModel.Load();
            return View(model);
        }

        public ActionResult Izvestaj(int id)
        {
            return View();
        }
    }
}