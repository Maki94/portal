using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Member
{
    public class MemberIndexViewModel
    {
        public string Nickname { get; set; }

        public static MemberIndexViewModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMember(memberID);
            MemberIndexViewModel model = new MemberIndexViewModel
            {
                Nickname = m.Nickname
            };

            return model;
        }
    }
}