using Data.DataClasses;
using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberProfileModel : MemberEditProfileModel
    {
        // dodamo samo propertije koji ih nema u EditProfileViewModel
        public int Id { get; set; }

        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Display(Name = "Clan od")]
        public DateTime JoinDate { get; set; }

        public string Gmail { get; set; }

        [Display(Name = "Clanarina do")]
        public DateTime? FeePayedUntil { get; set; }

        public List<ProjectDTO> Projects { get; set; }

        public static MemberProfileModel Load(int memberID)
        {
            Data.DataClasses.Member m = Data.Entities.Members.GetMemberAt(memberID);
            MemberProfileModel model = new MemberProfileModel
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
                Avatar = m.Avatar ?? DefaultPictures.GetPictureByName("Avatar"),
                JoinDate = m.JoinDate,
                Gmail = m.Gmail,
                FeePayedUntil = m.FeePayedUntil
            };

            List<Project> projects = Data.Entities.Projects.GetAllProjects();
            model.Projects = ProjectListModel.CreateProjectListDTOs(projects);

            return model;
        }
    }
}