using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;

namespace MVC.Models
{
    public class MemberIndexModel
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public Data.Enumerations.MemberStatus Status { get; set; }
        public DateTime JoinDate { get; set; }

        public List<string> Emails { get; set; }

        public static MemberIndexModel Load(int memberID)
        {
            Member m = Data.Entities.Members.GetMember(memberID);
            MemberIndexModel model = new MemberIndexModel
            {
                MemberId = m.MemberId,
                Username = m.Username,
                Name = m.Name,
                Surname = m.Surname,
                Nickname = m.Nickname,
                //Status = m.Status,
                JoinDate = m.JoinDate,
            };

            return model;
        }
    }
}