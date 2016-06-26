using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberIndexModel
    {
        public string Nickname { get; set; }

        public static MemberIndexModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMemberAt(memberID);
            MemberIndexModel model = new MemberIndexModel
            {
                Nickname = m.Nickname
            };

            return model;
        }
    }
}