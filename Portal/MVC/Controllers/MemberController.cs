using System;
using System.Web.Mvc;
using Data.Entities;
using MVC.Models;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MemberController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index()
        {
            var model = MemberIndexModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        public ActionResult AllMembers()
        {
            return View(new MemberListModel());
        }

        [HttpPost]
        public JsonResult SearchMembers(string term)
        {
            var resultIds = Members.SearchMembers(term);
            return Json(resultIds);
        }

        public ActionResult GetAvatar(int id)
        {
            /*
            var mem = Members.GetMemberAt(id);
            var image = mem.Avatar == null || mem.Avatar.Length == 0
                ? DefaultPictures.GetPictureByName("Avatar")
                : mem.Avatar;
            var base64 = Convert.ToBase64String(image);
            var imgSrc = $"data:image/gif;base64,{base64}";
            return Content(imgSrc);
            */

            var imgSrc = $"http://i.imgur.com/Xqdt97P.png";
            return Content(imgSrc);
        }

        public ActionResult Profile(int id)
        {
            var model = MemberProfileModel.Load(id);
            return View(model);
        }

        public ActionResult Edit()
        {
            var model = MemberEditProfileModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MemberEditProfileModel m)
        {
            var memberId = MemberSession.GetMemberId();
            Members.EditProfile(memberId, m.Nickname,
                m.Faculty, m.DateOfBirth, m.Status, m.Phone,
                m.Facebook, m.LinkedIn, m.Skype);
            return RedirectToAction("Profile", new {id = memberId});
        }

        //[AuthorizeMember(Permission = (int)Data.Enumerations.Permission.AddMember)]
        public ActionResult Add()
        {
            return View(new MemberAddModel());
        }

        [HttpPost]
        public ActionResult Add(MemberAddModel m)
        {
            if (Members.GmailExists(m.Gmail))
            {
                return RedirectToAction("Add");
            }

            Members.AddMember(m.Gmail, m.Password, m.Name, m.Surname, m.RoleId, m.Date);

            return RedirectToAction("AllMembers");
        }
    }
}