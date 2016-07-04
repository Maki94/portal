using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public byte[] Avatar { get; set; }
        public string Faculty { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gmail { get; set; }
        public DateTime? FeePayedUntil { get; set; }
        public Enumerations.MemberStatus Status { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public Enumerations.Role Role { get; set; }
    }
}
