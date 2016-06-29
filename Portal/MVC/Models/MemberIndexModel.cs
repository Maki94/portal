using Data.DataClasses;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberIndexModel
    {
        
        public string Nickname { get; set; }
        public List<MemberDTO> Birthtdays { get; set; }
        public List<ProjectDTO> CurrentProject { get; set; }
        public List<MemberDTO> Anniversary { get; set; }
        public List<PollDTO> CurrentPoll { get; set; }
        //public List<MeetingDTO> ThisMongthMeetings { get; set; }

        public static List<MemberDTO> CreateMemberDTOListForDateBirth(DateTime date)
        {
            List<Member> members = Data.Entities.Members.GetMemberBirthday(date);
            List<MemberDTO> birthday = new List<MemberDTO>();

            foreach (Member m in members)
            {
                birthday.Add(CreateMemberDTO(m));
            }

            return birthday;
        }

        public static List<MemberDTO> CreateMemberDTOListForDateAnniversary(DateTime date)
        {
            List<Member> members = Data.Entities.Members.GetMemberAnniversary(date);
            List<MemberDTO> anniversary = new List<MemberDTO>();

            foreach (Member m in members)
            {
                anniversary.Add(CreateMemberDTO(m));
            }

            return anniversary;
        }

        public static MemberDTO CreateMemberDTO(Data.DataClasses.Member m)
        {
            MemberDTO member = new MemberDTO()
            {
                Name = m.Name,
                Surname = m.Surname,
                MemberId = m.MemberId
            };

            return member;
        }

        public static List<ProjectDTO> CreateProjectDTOListForDate(DateTime date)
        {
            List<Project> projects = Data.Entities.Projects.GetProjectCurrentAtDate(date);
            List<ProjectDTO> currentProject = new List<ProjectDTO>();

            foreach (Project p in projects)
            {
                currentProject.Add(CreateProjectDTO(p));
            }

            return currentProject;
        }

        public static ProjectDTO CreateProjectDTO(Data.DataClasses.Project p)
        {
            ProjectDTO project = new ProjectDTO()
            {
               Name = p.Name,
            };

            return project;
        }

        public static List<PollDTO> CreatePollDTOListForDate(DateTime date)
        {
            List<Data.DataClasses.Poll> polls = Data.Entities.Polls.GetPollsCurrentAtDate(date);
            List<PollDTO> currentPolls = new List<PollDTO>();

            foreach (Data.DataClasses.Poll p in polls)
            {
                currentPolls.Add(CreatePollDTO(p));
            }

            return currentPolls;
        }

        public static PollDTO CreatePollDTO(Data.DataClasses.Poll p)
        {
            PollDTO poll = new PollDTO()
            {
                Topic = p.Topic
            };

            return poll;
        }

        public static MemberIndexModel Load(int memberID, DateTime? date=null)
        {
            date = date ?? DateTime.Now;

            Data.DataClasses.Member m = Data.Entities.Members.GetMemberAt(memberID);
            MemberIndexModel model = new MemberIndexModel
            {
                Nickname = m.Nickname,
                CurrentProject = CreateProjectDTOListForDate((DateTime) date),
                CurrentPoll = CreatePollDTOListForDate((DateTime) date),
                Birthtdays = CreateMemberDTOListForDateBirth((DateTime) date),
                Anniversary = CreateMemberDTOListForDateAnniversary((DateTime) date)
            };

            return model;
        }
    }
}