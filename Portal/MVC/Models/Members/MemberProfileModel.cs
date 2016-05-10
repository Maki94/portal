using System;
using System.Collections.Generic;
using Data;
using Data.DataClasses;
using Data.DTOs;

namespace MVC.Models.Members
{
    public class MemberProfileModel : EditProfileModel
    {
        public DateTime JoinDate { get; set; }

        public List<ProjectDTO> Projects { get; set; }
        
        public static MemberProfileModel Load(int memberID)
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
                JoinDate = m.JoinDate,
                Faculty = m.Faculty,
                DateOfBirth = m.DateOfBirth,
                Facebook = m.Facebook,
                LinkedIn = m.LinkedIn,
            };

            model.Projects = Data.Entities.Projects.GetProjectsOfMember(memberID);

            return model;
        }
    }
}