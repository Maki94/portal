using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Agenda { get; set; }
        public string Note { get; set; }

        [ForeignKey("HR")]
        public int HRId { get; set; }
        public virtual Member HR { get; set; }

        [ForeignKey("MeetingCreator")]
        public int MeetingCreatorId { get; set; }
        public virtual Member MeetingCreator { get; set; }
    }
}
