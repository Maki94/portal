using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class PollOption
    {
        [Key]
        public int PollOptionId { get; set; }
        public string Answer { get; set; }
        public List<int> Voters { get; set; }
        
        public virtual Poll Poll { get; set; }
    }
}
