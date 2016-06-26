using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberChangePasswordModel
    {
        [Required(ErrorMessage = "Morate uneti staru lozinku.")]
        [Display(Name = "Stara lozinka")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti izmedju 5 i 255 karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Morate uneti novu lozinku.")]
        [Display(Name = "Nova lozinka")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti izmedju 5 i 255 karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Morate potvrditi novu lozinku.")]
        [Display(Name = "Potvrdite lozinku")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti izmedju 5 i 255 karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string RepeatPassword { get; set; }
    }
}