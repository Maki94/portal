using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberMeeting
    {
        public int MemberMeetingId { get; set; }
        public int MemberId { get; set; }
        public int MeetingId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
