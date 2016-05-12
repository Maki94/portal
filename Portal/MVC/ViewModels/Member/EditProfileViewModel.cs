using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;

namespace MVC.ViewModels
{
    public class EditProfileViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Faculty { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Data.Enumerations.MemberStatus Status { get; set; }

        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }

        public static EditProfileViewModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMember(memberID);
            EditProfileViewModel model = new EditProfileViewModel
            {
                Name = m.Name,
                Surname = m.Surname,
                Nickname = m.Nickname,
                Status = m.Status,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Phone = m.Phone,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
                Skype = m.Skype
            };

            return model;
        }
    }
}