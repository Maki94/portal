using System;
using System.Web.Mvc;
using Data.Entities;
using MVC.Models;
using System.IO;

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
        public ActionResult Edit(MemberEditProfileModel model)
        {
            var memberId = MemberSession.GetMemberId();
            if (ModelState.IsValid)
            {
                byte[] array = null;
                if (model.Avatar != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        model.Avatar.InputStream.Position = 0;
                        model.Avatar.InputStream.CopyTo(ms);
                        array = ms.GetBuffer();
                    }
                }
                Members.EditProfile(memberId, model.Nickname, array,
                model.Faculty, model.DateOfBirth, model.Status, model.Phone,
                model.Facebook, model.LinkedIn, model.Skype);
            }

            return RedirectToAction("Profile", new { id = memberId });
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