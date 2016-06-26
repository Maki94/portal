using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Data.DTOs;
using Data.Entities;
using MVC.Models;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MessengerController : Controller
    {
        public ActionResult Index()
        {
            return View(new MemberLoginModel());
        }

        [HttpPost]
        public string Conversation(int id) // receiverId
        {
            var messageList = new MessageListModel(id);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(messageList);

            return json;
        }

        [HttpPost]
        public bool SetMessage(int id, string text) // id - receiverId
        {
            try
            {
                var messageDTO = new MessageDTO
                {
                    SenderId = MemberSession.GetMemberId(),
                    ReceiverId = id,
                    Text = text,
                    Time = DateTime.Now
                };

                var message = new Message();

                message.Save(messageDTO);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
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
    }
}