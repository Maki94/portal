using System.Web.Mvc;
using System.Web.Script.Serialization;
using Data;
using Data.Entities;
using MVC.Models;
using System;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class FratorController : Controller
    {
        public ActionResult Index(int? id = null)
        {
            var model = CompanyModel.Load(MemberSession.GetMemberId());
            if (id != null)
            {
                if (MemberSession.GetRole() == Enumerations.Role.Administrator ||
                    MemberSession.GetRole() == Enumerations.Role.FR)
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
            return RedirectToAction("Index", new {id});
        }

        public ActionResult Like(int idm, int idc)
        {
            Comments.Like(idm, idc);
            return RedirectToAction("Index");
        }

        public ActionResult Unlike(int idm, int idc)
        {
            Comments.Unlike(idm, idc);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View(new CompanyAddModel());
        }

        public string GetContact(int id)
        {
            var cp = Companys.GetContactPerson(id);
            var c = CompanyModel.crateContactDTO(cp);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(c);
            return json;
        }
        [HttpPost]
        public void SaveComment(string text, string tip, string projekat, string companyId)
        {
            int idc;
            int.TryParse(companyId, out idc);

            int idp;
            int.TryParse(projekat, out idp);

            Enumerations.CommentType type = Enumerations.CommentType.Classic;
            foreach(int i in Enum.GetValues(typeof(Enumerations.CommentType)))
            {
                if(((Enumerations.CommentType)i).ToString().CompareTo(tip)==0)
                {
                    type = (Enumerations.CommentType)i;
                }
            }
            Data.Entities.Comments.AddComment(MemberSession.GetMemberId(), idc, idp, type, text);
        }

        public ActionResult AddContact(string name, string note, string email, string phone, string companyId)
        {
            int idc;
            int.TryParse(companyId, out idc);

            Data.Entities.Companys.AddContactPerson(idc, email, name, phone, note);
            return RedirectToAction("Index");
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