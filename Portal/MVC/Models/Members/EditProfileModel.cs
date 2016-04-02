using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;

namespace MVC.Models
{
    public class EditProfileModel
    {
        public int MemberID { get; set; }
        public string Username { get; set; }
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
                MemberID = m.MemberID,
                Username = m.Username,
                Name = m.Name,
                Surname = m.Surname,
                Nickname = m.Nickname,
                Status = m.Status,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
            };

            model.Emails = Data.Entities.Members.GetMemberEmails(memberID);

            return model;
        }
    }
}