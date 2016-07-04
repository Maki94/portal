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
            using (var dc = new DataContext())
            {

                Project p = new Project
                {
                    Name = name,
                    Description = description,
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
                return (from p in dc.Projects where p.State == Enumerations.ProjectState.aktivan select p).ToList();
            }
        }

        public static List<Project> GetProjectsOfMember(int memberID)
        {
            using (var dc = new DataContext())
            {
                return (from p in dc.MemberProjects
                        where p.MemberId == memberID
                        select p.Project).ToList();
            }
        }
        
        public static List<Project> GetAllProjects()
        {
            using (var dc = new DataContext())
            {
                return (from p in dc.Projects select p).ToList();
            }
        }

        public static Project AddProject()
        {
            using (var dc = new DataContext())
            {
                return new Project();
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

    }   
}
