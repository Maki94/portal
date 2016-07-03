using MVC.Models;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class FratorController : Controller
    {
        public ActionResult Index()
        {
            CompanyModel model = CompanyModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        public ActionResult Profile(int id)
        {
            return View();
        }
    }
}