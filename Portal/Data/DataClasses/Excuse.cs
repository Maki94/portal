using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
