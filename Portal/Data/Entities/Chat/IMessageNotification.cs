using System.Collections.Generic;
using Data.DTOs;

namespace Data.Entities
{
    public interface IMessageNotification
    {
        List<MessageNotificationDTO> GetMessageNotifications(int senderId, int receiverId);
        List<MessageNotificationDTO> GetMessageNotifications(int receiverId);
        List<int> GetMessageIds(int receiverId);
        int NumberOfNotifications(int receiverId);
        void DeleteAllFrom(int receiverId, int senderId);
        
        #region CRUD

        List<MessageNotificationDTO> GetAll();

        MessageNotificationDTO Get(int id);

        void Delete(int id);

        void Update(MessageNotificationDTO messageNotificationDTO);

        void Save(MessageNotificationDTO messageNotificationDTO);

        #endregion
    }
}