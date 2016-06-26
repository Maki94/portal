using Data.DataClasses;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ProjectModel
    {


        public static ProjectDTO CreateProjectDTO(Project p)
        {
            return new ProjectDTO
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Website = p.Website,
                State = p.State,
                Report = p.Report,
                Logo = p.Logo,
                StartDate = p.StartDate,
                FinishDate = p.FinishDate,
                Description = p.Description,
                Place = p.Place,
                FlyerImage = p.FlyerImage,
                Newsletter = p.Newsletter,
                Offer = p.Offer
            };
        }
    }
}