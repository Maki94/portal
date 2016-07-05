using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.DataClasses;
using Data.DTOs;

namespace Data.Entities
{
    public class Projects
    {
        public static Project AddProject(string name, string website, byte[] logo, DateTime startdate,
                                         DateTime finishdate, string description, string place, int teamId)
        {
            using (var dc = new DataContext())
            {

                Project p = new Project
                {
                    Name = name,
                    Website = website,
                    Logo = logo,
                    StartDate = startdate,
                    FinishDate = finishdate,
                    State = Enumerations.ProjectState.Aktivan,
                    Description = description,
                    Place = place,
                    TeamId = teamId
                };

                dc.Projects.Add(p);
                dc.SaveChanges();

                return p;
            }
        }

        public static Project GetProjectAt(int projectID)
        {
            using (var dc = new DataContext())
            {
                return (from p in dc.Projects where p.ProjectId == projectID select p).First();
            }
        }

        public static List<Project> GetProjectCurrentAtDate()
        {
            using (var dc = new DataContext())
            {
                return (from p in dc.Projects where p.State == Enumerations.ProjectState.Aktivan select p).ToList();
            }
        }

        public static List<Project> GetProjectsOfMember(int memberID)
        {
            using (var dc = new DataContext())
            {
                return dc.MemberProjects.Where(x => x.MemberId == memberID)
                                        .Include(x => x.Project.Team)
                                        .Select(x => x.Project).ToList();
            }
        }

        public static List<Project> GetTeamProjectsOfMember(int memberId)
        {
            using (var dc = new DataContext())
            {
                Team t = dc.MemberTeams.Where(x => x.MemberId == memberId)
                                       .Select(x => x.Team).First();

                return dc.Projects.Include(x => x.Team)
                                  .Where(x => x.TeamId == t.TeamId)
                                  .ToList();
            }
        }

        public static string FindFunctionOfMemberInProject(int memberId, int projectId)
        {
            using (var dc = new DataContext())
            {
                return dc.MemberProjects.Where(x => x.MemberId == memberId && x.ProjectId == projectId).Select(x => x.Function).First();
            }
        }

        public static List<Project> GetAllProjects()
        {
            using (var dc = new DataContext())
            {
                return dc.Projects.Include(x => x.Team).Select(x => x).OrderBy(x => x.State).ToList();
            }
        }

        public static bool DeleteProject(int projectId)
        {
            using (var dc = new DataContext())
            {
                return true;
            }
        }

        public static void ChangeProject()
        {
            using (var dc = new DataContext())
            {

            }
        }

        public static List<MemberProject> GetProjectHelpers(int projectId)
        {
            using (var dc = new DataContext())
            {
                return dc.MemberProjects.Include(x => x.Member).Where(x => x.ProjectId == projectId).Select(x => x).ToList();
            }
        }
    }   
}
