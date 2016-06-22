using System.Collections.Generic;
using Data.DTOs;

namespace Data.Entities
{
    public interface IMessage
    {
        List<MessageDTO> GetConversation(int senderId, int receiverId);

        #region CRUD

        List<MessageDTO> GetAll();

        MessageDTO Get(int id);

        void Delete(int id);

        void Update(MessageDTO messageDTO);

        #endregion
    }
}