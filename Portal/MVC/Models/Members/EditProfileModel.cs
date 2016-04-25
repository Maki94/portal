using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;

namespace MVC.Models
{
    public class EditProfileModel
    {
        public int MemberId { get; set; }
        public string Gmail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Faculty { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Data.Enumerations.MemberStatus Status { get; set; }

        public string Facebook { get; set; }
        public string LinkedIn { get; set; }

        public List<string> Emails { get; set; }

        public static EditProfileModel Load(int memberID)
        {
            Member m = Data.Entities.Members.GetMember(memberID);
            MemberProfileModel model = new MemberProfileModel
            {
                MemberId = m.MemberId,
                Gmail = m.Gmail,
                Name = m.Name,
                Surname = m.Surname,
                Nickname = m.Nickname,
                //Status = m.Status,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
            };

            return model;
        }
    }
}