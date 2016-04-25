using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberBadge
    {
        public int MemberBadgeId { get; set; }
        public int MemberId { get; set; }
        public int BadgeId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
