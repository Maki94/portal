using Data;
using Data.DTOs.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Models
{
    public class CompanyAddModel
    {
        [Required(ErrorMessage = "Morate uneti ime.")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morate uneti adresu.")]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Morate uneti Grad.")]
        [Display(Name = "Grad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Morate uneti delatnost.")]
        [Display(Name = "Delatnost")]
        public string Field { get; set; }

        public SelectList FieldChoice { get; set; }

        [Required(ErrorMessage = "Morate uneti tip.")]
        [Display(Name = "Tip")]
        public string Type { get; set; }

        public SelectList TypeChoice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Morate uneti broj.")]
        [Required(ErrorMessage = "Morate uneti telefon.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Morate uneti sajt.")]
        [Display(Name = "Sajt")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Morate uneti email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Description { get; set; }

        public CompanyAddModel()
        {
            List<SelectListItem> fielditems = new List<SelectListItem>();
            Array fieldValues = Enum.GetValues(typeof(Enumerations.CompanyField));
            foreach (var r in fieldValues)
            {
                fielditems.Add(new SelectListItem()
                {
                    Value = r.ToString(),
                    Text = Enum.GetName(typeof(Enumerations.CompanyField), r)
                });
            }
            FieldChoice = new SelectList(fielditems, "Value", "Text");

            List<SelectListItem> typeitems = new List<SelectListItem>();
            Array typeValues = Enum.GetValues(typeof(Enumerations.CompanyType));
            foreach (var r in typeValues)
            {
                typeitems.Add(new SelectListItem()
                {
                    Value = r.ToString(),
                    Text = Enum.GetName(typeof(Enumerations.CompanyType), r)
                });
            }
            TypeChoice = new SelectList(typeitems, "Value", "Text");
        }
    }
}