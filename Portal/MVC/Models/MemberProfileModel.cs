using Data;
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

        public List<MemberProjectDTO> MemberTeamProjects { get; set; }
        public List<MemberProjectDTO> MemberHelpProjects { get; set; }
        public List<BadgeDTO> MemberBadges { get; set; }

        public Enumerations.Role Role { get; set; }

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
                //Avatar = m.Avatar ?? DefaultPictures.GetPictureByName("Avatar"),
                JoinDate = m.JoinDate,
                Gmail = m.Gmail,
                FeePayedUntil = m.FeePayedUntil,
                Role = (Data.Enumerations.Role)m.Role.RoleId
            };

            List<Project> projects = Data.Entities.Projects.GetTeamProjectsOfMember(memberID);
            List<Project> projectsPomagao = Data.Entities.Projects.GetProjectsOfMember(memberID);
            List<Badge> badges = Data.Entities.Badges.GetAllBadgesOfMember(memberID);
            model.MemberBadges = BadgeListModel.CreateBadgeDTOs(badges);
            model.MemberTeamProjects = ProjectModel.CreateMemberTeamProjectDTOList(m, projects);
            model.MemberHelpProjects = ProjectModel.CreateMemberHelpProjectDTOList(m, projectsPomagao);

            return model;
        }
    }
}