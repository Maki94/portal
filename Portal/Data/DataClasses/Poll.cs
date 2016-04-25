using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Poll
    {
        public int PollId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public Enumerations.PollState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Member PollCreator { get; set; }
    }
}
