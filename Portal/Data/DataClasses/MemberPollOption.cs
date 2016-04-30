using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberPollOption
    {
        public int MemberPollOptionId { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("PollOption")]
        public int PollOptionId { get; set; }
        public virtual PollOption PollOption { get; set; }
    }
}
