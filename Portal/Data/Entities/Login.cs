using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs;
using Data.DataClasses;

namespace Data.Entities
{
    public class Login
    {
        public static LoginDTO CreateLoginDTO(string gmail, string password, bool rememberMe)
        {
            LoginDTO ld = new LoginDTO();
            Member mem = Members.MemberExists(gmail, password);
            if (mem == null)
            {
                if (Members.GmailExists(gmail))
                    ld.loginStatus = (int)Enumerations.LoginStatus.IncorrectPassword;
                else
                    ld.loginStatus = (int)Enumerations.LoginStatus.Failed;
                return ld;
            }

            ld.loginStatus = (int)Enumerations.LoginStatus.Successful;

            ld.MemberID = mem.MemberId;
            ld.Gmail = mem.Gmail;
            ld.Role = mem.RoleId;
            ld.Permissions = Members.GetPermissions(ld.Role);
            ld.RememberMe = rememberMe;

            return ld;
        }
    }
}
