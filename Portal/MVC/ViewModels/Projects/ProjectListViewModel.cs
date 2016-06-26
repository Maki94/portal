using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Projects
{
    public class ProjectListViewModel
    {
        public List<ProjectDTO> ProjectThumbnails { get; set; }

        public ProjectListViewModel()
        {
            ProjectThumbnails = Data.Entities.Projects.GetProjectThumbnails();
        }
    }
}