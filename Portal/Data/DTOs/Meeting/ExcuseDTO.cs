using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Meeting
{
    class ExcuseDTO
    {
        public int ExcuseId { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public bool IsDeleted { get; set; }
        
        public int MemberId { get; set; }
        public virtual MemberDTO Member { get; set; }
        
        public int MeetingId { get; set; }
        public virtual MeetingDTO Meeting { get; set; }
    }
}
