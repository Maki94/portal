using System.Collections.Generic;
using Data.DTOs;
using Data.Entities;

namespace MVC.Models
{
    public class MessageListModel
    {
        private readonly Message _message = new Message();
        public List<MessageDTO> OrderedMessages;

        public MessageListModel(int receiverId)
        {
            OrderedMessages = _message.GetConversationOrderedByDate(MemberSession.GetMemberId(), receiverId);
        }

        public MessageListModel()
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