using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberCompany
    {
        public int MemberCompanyId { get; set; }
        public int MemberId { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public virtual Member Member { get; set; }
        public virtual Company Company { get; set; }
    }
}
