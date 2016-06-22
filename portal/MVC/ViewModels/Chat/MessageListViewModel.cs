using System.Collections.Generic;
using Data.DTOs;
using Data.Entities;

namespace MVC.ViewModels.Chat
{
    public class MessageListViewModel
    {
        private readonly Message _message = new Message();
        public List<MessageDTO> Messages;

        public MessageListViewModel(int senderId, int receiverId)
        {
            Messages = _message.GetConversation(senderId, receiverId);
        }

        public MessageListViewModel()
        {
            // TODO: ubaciti default last conversation
        }

        public List<MessageDTO> GetConversation(int senderId, int receiverId)
        {
            Messages = _message.GetConversation(senderId, receiverId);
            return Messages;
        }
    }
}