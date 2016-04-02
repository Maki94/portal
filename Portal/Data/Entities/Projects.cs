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
        public static Project AddProject(string title, string description)
        {
            DataContext dc = new DataContext();

            Project p = new Project
            {
                Title = title,
                Description = description,
            };

            dc.Projects.Add(p);
            dc.SaveChanges();

            return p;
        }

        public static Project GetProject(int projectID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();
            return (from p in dc.Projects where p.ProjectID == projectID select p).First();
        }

        public static List<ProjectDTO> GetProjectsOfMember(int memberID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();

            return (from p in dc.MemberProjects
                    where p.MemberID == memberID
                    select new ProjectDTO
                    {
                        ProjectID = p.ProjectID,
                        Title = p.Project.Title,
                        Description = p.Project.Description,
                    }).ToList();
        }
    }
}
