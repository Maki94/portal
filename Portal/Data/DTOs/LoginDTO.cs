using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            Permissions = new List<int>();
        }
        public int MemberID { get; set; }
        public string Gmail { get; set; }
        public bool RememberMe { get; set; }
        public int Role { get; set; }
        public IList<int> Permissions { get; set; }
        public int LoginStatus { get; set; }
        public int LastChatParticipant { get; set; }
        public byte[] Avatar { get; set; }
    }
}
