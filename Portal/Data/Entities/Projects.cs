using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using Data.DTOs;

namespace Data.Entities
{
    public class Projects
    {
        public static Project AddProject(string name, string description)
        {
            DataContext dc = new DataContext();

            Project p = new Project
            {
                Name = name,
                Description = description,
            };

            dc.Projects.Add(p);
            dc.SaveChanges();

            return p;
        }

        public static Project GetProject(int projectID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();
            return (from p in dc.Projects where p.ProjectId == projectID select p).First();
        }

        public static List<ProjectDTO> GetProjectsOfMember(int memberID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();

            return (from p in dc.MemberProjects
                    where p.MemberId == memberID
                    select new ProjectDTO
                    {
                        ProjectId = p.ProjectId,
                        Name = p.Project.Name,
                        Description = p.Project.Description,
                    }).ToList();
        }
    }
}
