using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class LeaveDTO
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Status { get; set; }        
        public String FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public bool Paid { get; set; }
        public string Type { get; set; }
        public String Comment { get; set; }
    }
}
