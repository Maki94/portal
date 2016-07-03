using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC
{
    public class MemberSession
    {
        public static LoginDTO GetMember()
        {
            return (LoginDTO)HttpContext.Current.Session["Member"];
        }

        public static int GetMemberId()
        {
            LoginDTO member = GetMember();
            if (member == null)
            {
                throw new Exception("Nije prijavljen");
            }
            return member.MemberID;
        }
        public static Data.Enumerations.Role GetRole()
        {
            LoginDTO member = GetMember();
            if (member == null)
            {
                throw new Exception("Nije prijavljen");
            }
            return (Data.Enumerations.Role)member.Role;
        }
    }
}