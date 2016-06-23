using System;

namespace Data.DTOs
{
    public class MessageDTO
    {
        public int MessageId { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public virtual int SenderId { get; set; }
        public virtual int ReceiverId { get; set; }


        //public virtual Member Sender { get; set; }
        //public virtual Member Receiver { get; set; }

    }
}