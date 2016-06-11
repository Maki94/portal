using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberComment
    {
        public int MemberCommentId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
