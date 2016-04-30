using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberMeeting
    {
        public int MemberMeetingId { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
