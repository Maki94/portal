using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberEditModel
    {
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Display(Name = "Nadimak")]
        public string Nickname { get; set; }

        [Display(Name = "Fakultet")]
        public string Faculty { get; set; }

        [Display(Name = "Datum rodjenja")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Status")]
        public Data.Enumerations.MemberStatus Status { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

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
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
                Skype = m.Skype
            };

            return model;
        }
    }
}