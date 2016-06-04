using Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Member
{
    public class MemberProfileViewModel : EditProfileViewModel
    {
        // dodamo samo propertije koji ih nema u EditProfileViewModel
        public int Id { get; set; }

        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        public byte[] Avatar { get; set; }

        [Display(Name = "Clan od")]
        public DateTime JoinDate { get; set; }

        public string Gmail { get; set; }

        [Display(Name = "Clanarina do")]
        public DateTime FeePayedUntil { get; set; }

        public List<ProjectDTO> Projects { get; set; }

        public static MemberProfileViewModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMember(memberID);
            MemberProfileViewModel model = new MemberProfileViewModel
            {
                Nickname = m.Nickname,
                Status = m.Status,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Phone = m.Phone,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
                Skype = m.Skype,

                Id = m.MemberId,
                Name = m.Name,
                Surname = m.Surname,
                Avatar = m.Avatar,
                JoinDate = m.JoinDate,
                Gmail = m.Gmail,
                FeePayedUntil = m.FeePayedUntil
            };

            model.Projects = Data.Entities.Projects.GetProjectsOfMember(memberID);

            return model;
        }
    }
}