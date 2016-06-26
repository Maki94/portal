using System.Collections.Generic;
using Data.DTOs;
using Data.Entities;

namespace MVC.ViewModels.Messenger
{
    public class MessageNotificationListViewModel
    {
        private readonly MessageNotification _messageNotification = new MessageNotification();
        private readonly Message _message = new Message();

        public List<MessageNotificationDTO> Notifications;
        public List<MessageDTO> OrderedMessages;

        public MessageNotificationListViewModel(int receiverId)
        {
            Notifications = _messageNotification.GetMessageNotifications(receiverId);

            var ids = _messageNotification.GetMessageIds(receiverId);
            OrderedMessages = _message.GetConversationWithIds(ids);
        }

    }
}