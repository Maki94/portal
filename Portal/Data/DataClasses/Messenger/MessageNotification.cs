using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataClasses
{
    public class MessageNotification
    {
        #region Fields

        public int MessageNotificationId { get; set; }
        public int MessageId { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region ForeignKeys

        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        public virtual Member Sender { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        public virtual Member Receiver { get; set; }

        #endregion
    }
}