using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class MemberProject
    {
        [Key, Column(Order = 0)]
        public int MemberID { get; set; }
        [Key, Column(Order = 1)]
        public int ProjectID { get; set; }

        public virtual Member Member { get; set; }
        public virtual Project Project { get; set; }

        public string Role { get; set; }
    }
}
