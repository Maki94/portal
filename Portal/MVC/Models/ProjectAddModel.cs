using Data.DataClasses;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC.Models
{
    public class ProjectAddModel
    {
        [Required(ErrorMessage = "Morate uneti ime projekta.")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Sajt")]
        public string Website { get; set; }

        [ValidateFile(ErrorMessage = "Izaberite JPG/PNG/GIF sliku manju od 1MB")]
        public HttpPostedFileBase Logo { get; set; }

        [Required(ErrorMessage = "Morate izabrati datum početka.")]
        [Display(Name = "Datum početka")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Morate izabrati datum završetka.")]
        [Display(Name = "Datum završetka")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Morate uneti mesto.")]
        [Display(Name = "Mesto")]
        public string Place { get; set; }

        public string TeamIdString { get; set; }

        [Required(ErrorMessage = "Morate izabrati realizacioni tim.")]
        [Display(Name = "Tim")]
        public SelectList TeamChoice { get; set; }

        public ProjectAddModel()
        {
            StartDate = DateTime.Now;
            FinishDate = DateTime.Now.AddDays(20);

            List<Team> teams = Teams.GetAllTeams();
            List<SelectListItem> teamitems = new List<SelectListItem>();
            foreach (var t in teams)
            {
                teamitems.Add(new SelectListItem()
                {
                    Value = t.TeamId.ToString(),
                    Text = t.Name
                });
            }
            TeamChoice = new SelectList(teamitems, "Value", "Text");
        }
    }
}