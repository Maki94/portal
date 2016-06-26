using System;

namespace Data.DTOs
{
    public class MessageDTO
    {
        public int MessageId { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        #region Foreign keys

        public virtual int SenderId { get; set; }
        public virtual int ReceiverId { get; set; }

        #endregion
    }
}