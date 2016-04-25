using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberPollOption
    {
        public int MemberPollOptionId { get; set; }
        public int MemberId { get; set; }
        public int PollOptionId { get; set; }

        public virtual Member Member { get; set; }
        public virtual PollOption PollOption { get; set; }
    }
}
