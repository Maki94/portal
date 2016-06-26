using System;
using System.Collections.Generic;
using System.Linq;
using Data.DTOs;

namespace Data.Entities
{
    public class MessageNotification : IMessageNotification
    {
        public List<MessageNotificationDTO> GetMessageNotifications(int senderId, int receiverId)
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Select(o => new MessageNotificationDTO
                {
                    MessageNotificationId = o.MessageNotificationId,
                    ReceiverId = o.ReceiverId,
                    SenderId = o.SenderId,
                    MessageId = o.MessageId,
                    Time = o.Time
                }).Where(o => o.SenderId == senderId && o.ReceiverId == receiverId).ToList();

                return l;
            }
        }

        public List<MessageNotificationDTO> GetMessageNotifications(int receiverId)
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Where(o => o.ReceiverId == receiverId && o.IsDeleted == false).Select(o => new MessageNotificationDTO
                {
                    MessageNotificationId = o.MessageNotificationId,
                    ReceiverId = o.ReceiverId,
                    SenderId = o.SenderId,
                    MessageId = o.MessageId,
                    Time = o.Time
                }).ToList();

                return l;
            }
        }

        public List<int> GetMessageIds(int receiverId)
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Where(o => o.ReceiverId == receiverId && o.IsDeleted == false).Select(o => o.MessageId).ToList();

                return l;
            }
        }

        public int NumberOfNotifications(int receiverId)
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Where(o => o.ReceiverId == receiverId && o.IsDeleted == false).Select(o => o.MessageId).Count();

                return l;
            }
        }

        public void DeleteAllFrom(int receiverId, int senderId)
        {
            using (var dc = new DataContext())
            {
                var entities = dc.MessageNotifications.Where(o => o.ReceiverId == senderId && o.SenderId == receiverId).ToList();
                entities.ForEach(x => x.IsDeleted = true);
                
                dc.SaveChanges();
            }
        }

        #region CRUD

        public List<MessageNotificationDTO> GetAll()
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Where(o => o.IsDeleted == false).Select(o => new MessageNotificationDTO
                {
                    MessageNotificationId = o.MessageNotificationId,
                    ReceiverId = o.ReceiverId,
                    SenderId = o.SenderId,
                    MessageId = o.MessageId,
                    Time = o.Time
                }).ToList();

                return l;
            }
        }

        public MessageNotificationDTO Get(int id)
        {
            using (var dc = new DataContext())
            {
                var l = dc.MessageNotifications.Select(o => new MessageNotificationDTO
                {
                    MessageNotificationId = o.MessageNotificationId,
                    ReceiverId = o.ReceiverId,
                    SenderId = o.SenderId,
                    MessageId = o.MessageId,
                    Time = o.Time
                }).First(x => x.MessageNotificationId == id);

                return l;
            }
        }

        public void Delete(int id)
        {
            using (var dc = new DataContext())
            {
                var entity = dc.MessageNotifications.Find(id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                }
                dc.SaveChanges();
            }
        }

        public void Update(MessageNotificationDTO messageNotificationDTO)
        {
            throw new NotImplementedException();
        }

        public void Save(MessageNotificationDTO messageNotificationDTO)
        {
            using (var dc = new DataContext())
            {
                var messageNotification = new DataClasses.MessageNotification
                {
                    Receiver =
                        (from m in dc.Members where m.MemberId == messageNotificationDTO.ReceiverId select m).First(),
                    Sender = (from m in dc.Members where m.MemberId == messageNotificationDTO.SenderId select m).First(),
                    MessageId = messageNotificationDTO.MessageId,
                    Time = messageNotificationDTO.Time,
                    IsDeleted = false
                };

                dc.MessageNotifications.Add(messageNotification);

                dc.SaveChanges();
            }
        }

        #endregion
    }
}