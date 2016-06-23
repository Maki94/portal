using System;
using System.Web.Mvc;
using Data.Entities;
using MVC.ViewModels;
using MVC.ViewModels.Member;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MemberController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index()
        {
            var model = MemberIndexViewModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        public ActionResult AllMembers()
        {
            return View(new MemberListViewModel());
        }

        [HttpPost]
        public JsonResult SearchMembers(string term)
        {
            var resultIds = Members.SearchMembers(term);
            return Json(resultIds);
        }

        public ActionResult GetAvatar(int id)
        {
            var mem = Members.GetMemberAt(id);
            var image = mem.Avatar == null || mem.Avatar.Length == 0
                ? DefaultPictures.GetPictureByName("Avatar")
                : mem.Avatar;
            var base64 = Convert.ToBase64String(image);
            var imgSrc = $"data:image/gif;base64,{base64}";
            return Content(imgSrc);
        }

        public ActionResult Profile(int id)
        {
            var model = MemberProfileViewModel.Load(id);
            return View(model);
        }

        public ActionResult Edit()
        {
            var model = EditProfileViewModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProfileViewModel m)
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
            return View(new AddMemberViewModel());
        }

        [HttpPost]
        public ActionResult Add(AddMemberViewModel m)
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