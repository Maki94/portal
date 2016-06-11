using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Member1")]
        public int Member1Id { get; set; }
        public virtual Member Member1 { get; set; }

        [ForeignKey("Member2")]
        public int Member2Id { get; set; }
        public virtual Member Member2 { get; set; }
    }
}
