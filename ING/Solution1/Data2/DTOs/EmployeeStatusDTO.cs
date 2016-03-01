using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class EmployeeStatusDTO
    {
        public int StatusID { get; set; }
        public  int EmployeeID { get; set; }
        public int Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InsertDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? ContractDuration { get; set; }
        public String comment { get; set; }
        public int SubmitterID { get; set; }
        public String SubmitterName { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

    }
}
