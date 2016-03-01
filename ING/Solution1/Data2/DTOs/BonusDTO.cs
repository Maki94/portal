using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class BonusDTO
    {
        public int BonusNumber { get; set; }
        public string Comment { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string SubmitterName { get; set; }

    }
}
