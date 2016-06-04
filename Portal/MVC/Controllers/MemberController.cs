using System.Web.Mvc;
using MVC.ViewModels.Member;
using MVC.ViewModels;
using Data.Entities;
using System;
using Data.DataClasses;

namespace MVC.Controllers
{
    public class MemberController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index()
        {
            MemberIndexViewModel model = MemberIndexViewModel.Load(MemberSession.GetMemberId());
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
            Member mem = Members.GetMember(id);
            byte[] image = (mem.Avatar == null || mem.Avatar.Length == 0) ? DefaultPictures.GetPictureByName("Avatar") : mem.Avatar;
            var base64 = Convert.ToBase64String(image);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return Content(imgSrc);
        }

        public ActionResult Profile(int id)
        {
            MemberProfileViewModel model = MemberProfileViewModel.Load(id);
            return View(model);
        }

        public ActionResult Edit()
        {
            EditProfileViewModel model = EditProfileViewModel.Load(MemberSession.GetMemberId());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProfileViewModel m)
        {
            int memberId = MemberSession.GetMemberId();
<<<<<<< HEAD
            Members.EditProfile(memberId, m.Name, m.Surname, m.Nickname,
=======
            Data.Entities.Members.EditProfile(memberId, m.Nickname,
>>>>>>> 3c4643b69ee7c03f2e5c0327fcbdad820c351614
                                              m.Faculty, m.DateOfBirth, m.Status, m.Phone,
                                              m.Facebook, m.LinkedIn, m.Skype);
            return RedirectToAction("Profile", new { id = memberId });
        }

<<<<<<< HEAD
=======
        //[AuthorizeMember(Permission = (int)Data.Enumerations.Permission.AddMember)]
>>>>>>> 26b19c3e5dabefb49276c359159f9e6402efd292
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

            Members.AddMember(m.Gmail, m.Password, m.Name, m.Surname, m.RoleId);

            return RedirectToAction("AllMembers");
        }
    }
}