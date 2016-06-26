using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class BadgeCreateModel
    {
        [Required(ErrorMessage = "Morate uneti jedinstveno ime za bedz.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Morate izabrati neku sliku.")]
        [ValidateFile(ErrorMessage = "Izaberite JPG/PNG/GIF sliku manju od 1MB")]
        public HttpPostedFileBase File { get; set; }
    }
}