using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class LeaveStatusDTO
    {
        public int SubmitterID { get; set; }

        public int Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public string SubFirstName { get; set; }
        public string SubLastName { get; set; }

        public string Comment { get; set; }


    }
}
