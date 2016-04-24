using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public Enumerations.EventState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<Picture> Gallery { get; set; }
    }
}
