using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberComment
    {
        public int MemberCommentId { get; set; }
        public int MemberId { get; set; }
        public int CommentId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
