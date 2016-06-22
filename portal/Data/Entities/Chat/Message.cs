using System;
using System.Collections.Generic;
using System.Linq;
using Data.DTOs;

namespace Data.Entities
{
    public class Message : IMessage
    {
        public List<MessageDTO> GetConversation(int senderId, int receiverId)
        {
            using (var dc = new DataContext())
            {
                return
                    (from o in dc.Messages
                        where o.SenderId == senderId && o.ReceiverId == receiverId
                        select new MessageDTO
                        {
                            MessageId = o.MessageId,
                            Text = o.Text,
                            Time = o.Time,
                            Sender = o.Sender,
                            Receiver = o.Receiver
                        }).ToList();
            }
        }

        #region CRUD

        public List<MessageDTO> GetAll()
        {
            using (var dc = new DataContext())
            {
                return
                    (from o in dc.Messages
                        select new MessageDTO
                        {
                            MessageId = o.MessageId,
                            Text = o.Text,
                            Time = o.Time,
                            Sender = o.Sender,
                            Receiver = o.Receiver
                        }).ToList();
            }
        }

        public MessageDTO Get(int id)
        {
            using (var dc = new DataContext())
            {
                return (from o in dc.Messages
                    where o.MessageId == id
                    select new MessageDTO
                    {
                        MessageId = o.MessageId,
                        Text = o.Text,
                        Time = o.Time,
                        Sender = o.Sender,
                        Receiver = o.Receiver
                    }).First();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(MessageDTO message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}