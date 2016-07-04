using MVC.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class FratorController : Controller
    {
        public ActionResult Index(int? id = null)
        {
            CompanyModel model = CompanyModel.Load(MemberSession.GetMemberId());
            if (id != null)
            {
                if (MemberSession.GetRole() == Data.Enumerations.Role.Administrator || MemberSession.GetRole() == Data.Enumerations.Role.FR)
                {
                    model.Show = model.AllCompany.Find(x => x.CompanyId == id);
                }
                else
                {
                    model.Show = model.MyCompany.Find(x => x.CompanyId == id);
                }
            }
                return View(model);
        }

        public ActionResult Profile(int id)
        {
            return RedirectToAction("Index", new { id = id });
        }

        public ActionResult Like(int idm, int idc)
        {
            Data.Entities.Comments.Like(idm, idc);
            return RedirectToAction("Index");
        }

        public ActionResult Unlike(int idm, int idc)
        {
            Data.Entities.Comments.Unlike(idm, idc);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View(new CompanyAddModel());
        }

        public string getContact(int id)
        {
            Data.DataClasses.ContactPerson cp = Data.Entities.Companys.GetContactPerson(id);
            var c = MVC.Models.CompanyModel.crateContactDTO(cp);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(c);
            return json;
        }

        public string GetCompany(int id)
        {
            Data.DataClasses.Company cp = Data.Entities.Companys.GetCompanyAt(id);
            var c = MVC.Models.CompanyModel.createCompanyDTO(cp);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(c);
            return json;
        }
    }
}