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
        public static LoginDTO CreateLoginDTO(LoginTransporterDTO ltd)
        {
            LoginDTO ld = new LoginDTO();
            Member mem = Members.MemberExists(ltd.Gmail, ltd.Password);
            if (mem == null)
            {
                if (Members.GmailExists(ltd.Gmail))
                    ld.loginStatus = (int)Enumerations.LoginStatus.IncorrectPassword;
                else
                    ld.loginStatus = (int)Enumerations.LoginStatus.Failed;
                return ld;
            }

            ld.loginStatus = (int)Enumerations.LoginStatus.Successful;

            ld.MemberID = mem.MemberId;
            ld.Name = mem.Name + " " + mem.Surname + " (" + mem.Nickname + ")";
            ld.Gmail = mem.Gmail;
            ld.Role = mem.RoleId;
            ld.Permissions = Members.GetPermissions(ld.Role);
            ld.RememberMe = ltd.RememberMe;

            return ld;
        }
    }
}
