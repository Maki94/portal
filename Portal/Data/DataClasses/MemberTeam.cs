using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberTeam
    {
        [Key]
        public int MemberTeamId { get; set; }
        public int MemberId { get; set; }
        public int TeamId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Team Team { get; set; }
    }
}
