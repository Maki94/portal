using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Member
{
    public class MemberProfileViewModel : EditProfileViewModel
    {
        // dodamo samo propertije koji ih nema u EditProfileViewModel
        public byte[] Avatar { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gmail { get; set; }
        public DateTime FeePayedUntil { get; set; }

        public List<ProjectDTO> Projects { get; set; }

        public static MemberProfileViewModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMember(memberID);
            MemberProfileViewModel model = new MemberProfileViewModel
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
                Skype = m.Skype,

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