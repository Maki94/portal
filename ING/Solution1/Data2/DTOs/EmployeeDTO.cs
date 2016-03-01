using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public String  FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public int  LeaveDaysNum { get; set; }
        [Required]
        public String Username { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}" , ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public int Role { get; set; }
        //mislim da ne treba
        public int? Bonus { get; set; }
        public String BonusComment { get; set; }
        public string newNumber { get; set; }
        //

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime ContractStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ContractEnd { get; set; }

    }
}
