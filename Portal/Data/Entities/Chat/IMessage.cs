using System.Collections.Generic;
using Data.DTOs;

namespace Data.Entities
{
    public interface IMessage
    {
        List<MessageDTO> GetConversation(int senderId, int receiverId);
        List<MessageDTO> GetConversationOrderedByDate(int senderId, int receiverId);
        List<MessageDTO> GetConversationOrderedByDateBuffer(int senderId, int receiverId, int numberOfMessages);

        List<MessageDTO> GetConversationWithIds(List<int> ids);

        #region CRUD

        List<MessageDTO> GetAll();

        MessageDTO Get(int id);

        void Delete(int id);

        void Update(MessageDTO messageDTO);

        void Save(ref MessageDTO messageDTO);

        #endregion
    }
}