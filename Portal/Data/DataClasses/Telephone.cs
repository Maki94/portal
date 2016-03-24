using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Telephone
    {
        public int TelephoneID { get; set; }
        public Enumerations.OwnerType OwnerType { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public string Number { get; set; }
    }
}
