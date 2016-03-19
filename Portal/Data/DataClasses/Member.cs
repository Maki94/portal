using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Status { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual ICollection<MemberProject> MemberProjects { get; set; }
    }
}
