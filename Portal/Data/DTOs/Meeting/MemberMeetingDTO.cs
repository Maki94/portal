using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Meeting
{
    class MemberMeetingDTO
    {
        public int MemberMeetingId { get; set; }
        public bool IsDeleted { get; set; }
        
        public int MemberId { get; set; }
        public virtual MemberDTO Member { get; set; }
        
        public int MeetingId { get; set; }
        public virtual MeetingDTO Meeting { get; set; }
    }
}
