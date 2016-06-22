using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MVC.ViewModels.Chat;
using MVC.ViewModels.Member;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MessengerController : Controller
    {
        public ActionResult Index()
        {
            return View(new MemberListViewModel());
        }

        public string Conversation(int id) // receiverId
        {
            var messageList = new MessageListViewModel(MemberSession.GetMemberId(), id);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(messageList);

            return json;
        }

        public bool SetMessage(int id, string text, DateTime time)
        {
            
        }
    }
}