using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Excuse
    {
        public int ExcuseId { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }

        public virtual Member Member { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
