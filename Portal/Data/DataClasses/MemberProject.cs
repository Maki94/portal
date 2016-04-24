using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberProject
    {
        [Key]
        public int MemberProjectId { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Project Project { get; set; }

        public string Role { get; set; }
    }
}
