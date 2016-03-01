using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class EmployeeDetailsTeamsDTO
    {
        public int TeamID { get; set; }
        [Display(Name = "Team name")]
        public String TeamName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

    }
}
