using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class HolidayDTO
    {
        public int HolidayID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Start_date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]  
        [DisplayName("End_date")]     
        public DateTime  EndDate { get; set; }
        [DisplayName("Repetition")]
        public Boolean Same { get; set; }
    }
}
