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
        public int MemberTeamID { get; set; }
        public int MemberID { get; set; }
        public int TeamID { get; set; }

        public virtual Member Member { get; set; }
        public virtual Team Team { get; set; }
    }
}
