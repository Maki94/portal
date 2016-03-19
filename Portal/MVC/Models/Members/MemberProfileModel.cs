using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DataClasses;
using Data.DTOs;

namespace MVC.Models
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
                MemberID = m.MemberID,
                Username = m.Username,
                Name = m.Name,
                Surname = m.Surname,
                Nickname = m.Nickname,
                Status = m.Status,
                JoinDate = m.JoinDate,
            };

            model.Projects = Data.Entities.Projects.GetProjectsOfMember(1);

            return model;
        }
    }
}