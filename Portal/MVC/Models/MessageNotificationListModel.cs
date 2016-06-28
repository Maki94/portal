using System.Collections.Generic;
using Data.DTOs;
using Data.Entities;

namespace MVC.Models
{
    public class MessageNotificationListModel
    {
        private readonly MessageNotification _messageNotification = new MessageNotification();
        private readonly Message _message = new Message();

        public List<MessageNotificationDTO> Notifications;
        public List<MessageDTO> OrderedMessages;

        public MessageNotificationListModel(int receiverId)
        {
            Notifications = _messageNotification.GetMessageNotifications(receiverId);

            var ids = _messageNotification.GetMessageIds(receiverId);
            OrderedMessages = _message.GetConversationWithIds(ids);
        }

        public void CheckNotifications(int receiverId)
        {
            Notifications = _messageNotification.GetMessageNotifications(receiverId);
        }
    }
}