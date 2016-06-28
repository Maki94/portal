using Data.DataClasses;
using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ProjectListModel
    {
        public List<ProjectDTO> ProjectThumbnails { get; set; }

        public ProjectListModel()
        {
            List<Project> projects = Data.Entities.Projects.GetAllProjects();
            ProjectThumbnails = CreateProjectListDTOs(projects);
        }

        public static List<ProjectDTO> CreateProjectListDTOs(List<Project> projects)
        {
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();

            foreach (Project p in projects)
            {
                projectDTOs.Add(ProjectModel.CreateProjectDTO(p));
            }

            return projectDTOs;
        }
    }
}