using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class ExecutiveBoard
    {
        public int ExecutiveBoardId { get; set; }
        public byte[] Report { get; set; }
        public byte[] Image { get; set; }
        public string Note { get; set; }

        public virtual Team Team { get; set; }
    }
}
