using System.Collections.Generic;
using Data.DTOs;
using Data.Entities;

namespace MVC.ViewModels.Messenger
{
    public class MessageListViewModel
    {
        private readonly Message _message = new Message();
        public List<MessageDTO> OrderedMessages;

        public MessageListViewModel(int receiverId)
        {
            OrderedMessages = _message.GetConversationOrderedByDate(MemberSession.GetMemberId(), receiverId);
        }

        public MessageListViewModel()
        {
            // TODO: ubaciti default last conversation
        }
        

        //public List<MessageDTO> GetConversation(int senderId, int receiverId)
        //{
        //    OrderedMessages = _message.GetConversation(senderId, receiverId);
        //    return OrderedMessages;
        //}
    }
}