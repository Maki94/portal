using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Company
{
    public class MemberCompanyDTO
    {
        public int MemberCompanyId { get; set; }

        [DisplayName("Start ")]
        public DateTime StartDate { get; set; }

        [DisplayName("Finish ")]
        public DateTime? FinishDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual MemberDTO Member { get; set; }
        
        public virtual CompanyDTO Company { get; set; }
    }
}
