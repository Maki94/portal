using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Email
    {
        [Key]
        public int EmailID { get; set; }
        public Enumerations.OwnerType OwnerType { get; set; }

        public int? MemberID { get; set; }
        public virtual Member Member { get; set; }

        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public string Address { get; set; }
    }
}
