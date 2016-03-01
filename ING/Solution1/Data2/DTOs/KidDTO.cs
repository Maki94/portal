using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data2.DTOs
{
    public class KidDTO
    {
        public int kidID { get; set; }
        public String FirstName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime  DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public List<string> ParentsNames { get; set; }
        public List<int> Parents { get; set; }

   
        
    }
}
