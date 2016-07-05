using Data;
using Data.DataClasses;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Models
{
    public class MemberEditModel
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Morate uneti Gmail adresu.")]
        [Display(Name = "Gmail")]
        [EmailAddress]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "Morate uneti ime.")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime.")]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Morate izabrati status.")]
        [Display(Name = "Status")]
        public string StatusIdString { get; set; }

        public SelectList StatusChoice { get; set; }

        [Required(ErrorMessage = "Morate izabrati ulogu.")]
        [Display(Name = "Uloga")]
        public string RoleIdString { get; set; }

        public SelectList RoleChoice { get; set; }

        [Required(ErrorMessage = "Morate izabrati datum uclanjenja.")]
        [Display(Name = "Datum uclanjenja")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        // status?
        // jos nesto mozda

        public MemberEditModel()
        {

        }

        public MemberEditModel(int id)
        {
            MemberId = id;
            Member m = Members.GetMemberAt(id);

            Gmail = m.Gmail;
            Name = m.Name;
            Surname = m.Surname;
            StatusIdString = m.Status.ToString();
            RoleIdString = ((int)m.RoleId).ToString();
            Date = m.DateOfBirth;
            List<Role> roles = Data.Entities.Roles.GetAllRoles();
            List<SelectListItem> roleitems = new List<SelectListItem>();
            foreach (var r in roles)
            {
                roleitems.Add(new SelectListItem()
                {
                    Value = r.RoleId.ToString(),
                    Text = r.Name,
                    Selected = (r.RoleId == m.RoleId)
                });
            }
            RoleChoice = new SelectList(roleitems, "Value", "Text");

            List<SelectListItem> statusitems = new List<SelectListItem>();
            Array statusValues = Enum.GetValues(typeof(Enumerations.MemberStatus));
            foreach (var r in statusValues)
            {
                statusitems.Add(new SelectListItem()
                {
                    Value = r.ToString(),
                    Text = Enum.GetName(typeof(Enumerations.MemberStatus), r),
                    Selected = ((Data.Enumerations.MemberStatus)r == m.Status)
                });
            }
            StatusChoice = new SelectList(statusitems, "Value", "Text");
        }
    }
}