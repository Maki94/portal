using Data.DataClasses;
using Data.Entities;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace MVC.Models
{
    public class ProjectModel
    {
        public ProjectDTO ProjectInfo { get; set; }
        public Team ProjectTeam { get; set; }
        public List<MemberTeam> MemberTeamList { get; set; }
        public List<ProjectMemberDTO> ProjectHelpers { get; set; }

        public static ProjectModel Load(int id)
        {
            ProjectModel model = new ProjectModel();
            model.ProjectInfo = CreateProjectDTO(Projects.GetProjectAt(id));
            model.ProjectTeam = Teams.GetTeamOfProject(id);
            model.MemberTeamList = Teams.GetMemberTeamList(model.ProjectTeam.TeamId);
            model.ProjectHelpers = CreateProjectHelpMemberDTOList(Projects.GetProjectAt(id));

            return model;
        }

        public static ProjectDTO CreateProjectDTO(Project p)
        {
            return new ProjectDTO
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Website = p.Website,
                State = p.State,
                Logo = p.Logo,
                StartDate = p.StartDate,
                FinishDate = p.FinishDate,
                Description = p.Description,
                Place = p.Place,
            };
        }

        public static MemberProjectDTO CreateMemberTeamProjectDTO(Member member, Project project)
        {
            Enumerations.TeamRole r = Teams.FindTeamRoleOfMemberProject(member.MemberId, project.ProjectId);
            return new MemberProjectDTO
            {
                MemberId = member.MemberId,
                ProjectId = project.ProjectId,
                ProjectName = project.Name,
                ProjectWebsite = project.Website,
                TeamName = project.Team.Name,
                TeamRole = r
            };
        }

        public static List<MemberProjectDTO> CreateMemberTeamProjectDTOList(Member member, List<Project> projects)
        {
            List<MemberProjectDTO> memberProjectDTOs = new List<MemberProjectDTO>();
            if (projects != null)
            {
                foreach (var p in projects)
                {
                    memberProjectDTOs.Add(CreateMemberTeamProjectDTO(member, p));
                }
                return memberProjectDTOs;
            }
            return null;
        }

        public static MemberProjectDTO CreateMemberHelpProjectDTO(Member member, Project project)
        {
            string function = Projects.FindFunctionOfMemberInProject(member.MemberId, project.ProjectId);
            return new MemberProjectDTO
            {
                MemberId = member.MemberId,
                ProjectId = project.ProjectId,
                ProjectName = project.Name,
                ProjectWebsite = project.Website,
                Function = function
            };
        }

        public static List<MemberProjectDTO> CreateMemberHelpProjectDTOList(Member member, List<Project> projects)
        {
            List<MemberProjectDTO> memberProjectDTOs = new List<MemberProjectDTO>();
            foreach (var p in projects)
            {
                memberProjectDTOs.Add(CreateMemberHelpProjectDTO(member, p));
            }
            return memberProjectDTOs;
        }

        public static ProjectMemberDTO CreateProjectHelpMemberDTO(MemberProject memberproject, Project project)
        {
            ProjectMemberDTO ret = new ProjectMemberDTO
            {
                MemberId = memberproject.MemberId,
                MemberName = memberproject.Member.Name + " " + memberproject.Member.Surname,
                Function = memberproject.Function
            };
            if (!string.IsNullOrWhiteSpace(memberproject.Member.Nickname))
            {
                ret.MemberName += " (" + memberproject.Member.Nickname + ")";
            }
            return ret;
        }

        public static List<ProjectMemberDTO> CreateProjectHelpMemberDTOList(Project proj)
        {
            List<MemberProject> helpers = Projects.GetProjectHelpers(proj.ProjectId);
            List<ProjectMemberDTO> projectMemberDTOList = new List<ProjectMemberDTO>();
            foreach (var h in helpers)
            {
                projectMemberDTOList.Add(CreateProjectHelpMemberDTO(h, proj));
            }
            return projectMemberDTOList;
        }
    }
}