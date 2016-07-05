using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Models
{
    public class MemberAddModel
    {
        [Required(ErrorMessage = "Morate uneti Gmail adresu.")]
        [Display(Name = "Gmail")]
        [EmailAddress]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "Morate uneti lozinku.")]
        [Display(Name = "Lozinka")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti izmedju 5 i 255 karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Morate potvrditi lozinku.")]
        [Display(Name = "Potvrdite lozinku")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti izmedju 5 i 255 karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Morate uneti ime.")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime.")]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Morate izabrati ulogu.")]
        public string RoleIdString { get; set; }

        [Required(ErrorMessage = "Morate izabrati ulogu.")]
        [Display(Name = "Uloga")]
        public SelectList RoleChoice { get; set; }

        [Required(ErrorMessage = "Morate izabrati datum uclanjenja.")]
        [Display(Name = "Datum uclanjenja")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        // status?
        // jos nesto mozda

        public MemberAddModel()
        {
            List<Role> roles = Data.Entities.Roles.GetAllRoles();
            List<SelectListItem> roleitems = new List<SelectListItem>();
            foreach (var r in roles)
            {
                roleitems.Add(new SelectListItem()
                {
                    Value = r.RoleId.ToString(),
                    Text = r.Name
                });
            }
            RoleChoice = new SelectList(roleitems, "Value", "Text");
            Date = DateTime.Now;
        }
    }
}