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

            // TODO: postavi da se sve notifikacije obrisu koje su bile za tog usera
            var messageNotification = new MessageNotification();
            messageNotification.DeleteAllFrom(id, MemberSession.GetMemberId());
            return json;
        }

        [HttpPost]
        public string GetNotifications(int id) // senderId
        {
            var messageNotificationListViewModel = new MessageNotificationListViewModel(MemberSession.GetMemberId());

            if (messageNotificationListViewModel.Notifications == null ||
                messageNotificationListViewModel.OrderedMessages.Count == 0)
                return false.ToString();

            var messageList = new MessageListViewModel
            {
                OrderedMessages = messageNotificationListViewModel.OrderedMessages
            };
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(messageList);

            // obrisati sve notifikacje od tog Sendera, namenjene receiveru
            var messageNotification = new MessageNotification();
            messageNotification.DeleteAllFrom(id, MemberSession.GetMemberId());

            return json; // vraca listu nedodatih poruka
        }

        [HttpPost]
        public string SetMessage(int id, string text) // id - receiverId
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

                message.Save(ref messageDTO);
                // put notification

                var messageNotification = new MessageNotification();

                var messageNotificationDTO = new MessageNotificationDTO
                {
                    Time = DateTime.Now,
                    MessageId = messageDTO.MessageId,
                    ReceiverId = id,
                    SenderId = MemberSession.GetMemberId()
                };

                messageNotification.Save(messageNotificationDTO);
                return true.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false.ToString();
            }
        }

        public int GetNumberOfNotifications()
        {
            var message = new MessageNotification();

            var br = message.NumberOfNotifications(MemberSession.GetMemberId());

            return br;
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