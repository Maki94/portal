using System;
using Data.DataClasses;

namespace Data.DTOs
{
    public class MessageNotificationDTO
    {
        #region Fields

        public int MessageNotificationId { get; set; }

        public int MessageId { get; set; }

        public DateTime Time { get; set; }

        #endregion

        #region ForeignKeys

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        #endregion
    }
}