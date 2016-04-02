using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberProject
    {
        [Key]
        public int MemberProjectID { get; set; }
        public int MemberID { get; set; }
        public int ProjectID { get; set; }

        public virtual Member Member { get; set; }
        public virtual Project Project { get; set; }

        public string Role { get; set; }
    }
}
