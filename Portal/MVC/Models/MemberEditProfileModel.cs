using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class MemberEditProfileModel
    {
        [Display(Name = "Nadimak")]
        public string Nickname { get; set; }

        [Display(Name = "Fakultet")]
        public string Faculty { get; set; }

        [Display(Name = "Datum rodjenja")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Status")]
        public Data.Enumerations.MemberStatus Status { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        public byte[] Avatar { get; set; }

        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }

        public static MemberEditProfileModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMemberAt(memberID);
            MemberEditProfileModel model = new MemberEditProfileModel
            {
                Nickname = m.Nickname,
                Status = m.Status,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Phone = m.Phone,
                Avatar = m.Avatar,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
                Skype = m.Skype
            };

            return model;
        }
    }
}