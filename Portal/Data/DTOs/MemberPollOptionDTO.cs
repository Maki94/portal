using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemberPollOptionDTO
    {
        public int MemberPollOptionId { get; set; }
        public int MemberId { get; set; }
        public int PollOptionId { get; set; }
    }
}
