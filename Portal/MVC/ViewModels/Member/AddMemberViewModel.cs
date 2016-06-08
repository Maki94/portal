using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Member
{
    public class AddMemberViewModel
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
        [Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Morate uneti ime.")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime.")]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        public List<Role> Roles { get; set; }
        
        [Required(ErrorMessage = "Morate izabrati ulogu.")]
        [Display(Name = "Uloga")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Morate izabrati datum uclanjenja.")]
        [Display(Name = "Datum uclanjenja")]
        public DateTime Date { get; set; }

        // status?
        // jos nesto mozda

        public AddMemberViewModel()
        {
            Roles = Data.Entities.Roles.GetAllRoles();
        }
    }
}