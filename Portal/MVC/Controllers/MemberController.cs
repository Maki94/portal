using System.Web.Mvc;
using MVC.ViewModels.Member;
using MVC.ViewModels;
using Data.Entities;
using System;

namespace MVC.Controllers
{
    [AuthorizeMember]
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
            var model = Data.Entities.Members.GetMemberThumbnails();
            return View(model);
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
            Data.Entities.Members.EditProfile(memberId, m.Name, m.Surname, m.Nickname,
                                              m.Faculty, m.DateOfBirth, m.Status, m.Phone,
                                              m.Facebook, m.LinkedIn, m.Skype);
            return RedirectToAction("Profile", new { id = memberId });
        }

        [AuthorizeMember(Permission = (int)Data.Enumerations.Permission.AddMember)]
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