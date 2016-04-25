using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Member Member1 { get; set; }
        public virtual Member Member2 { get; set; }
    }
}
