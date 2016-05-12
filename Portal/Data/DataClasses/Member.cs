using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public byte[] Avatar { get; set; }
        public string Faculty { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public string Password { get; set; }
        public string Gmail { get; set; }
        public DateTime FeePayedUntil { get; set; }
        public Enumerations.MemberStatus Status { get; set; }

        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<MemberProject> MemberProjects { get; set; }
        public virtual ICollection<MemberTeam> MemberTeams { get; set; }
    }
}
