using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Company
{
    public class MemberCommentDTO
    {
        public int MemberCommentId { get; set; }
        public bool IsDeleted { get; set; }
        
        public virtual MemberDTO Member { get; set; }
        
        public virtual CommentDTO Comment { get; set; }
    }
}
