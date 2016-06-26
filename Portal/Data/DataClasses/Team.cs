using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DismantleDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<MemberTeam> TeamMembers { get; set; }
    }
}
