using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PollDTO
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public bool AllowMultiple { get; set; }
        public bool HideResultsUntilFinished { get; set; }
        public bool HideVoters { get; set; }
        public Enumerations.PollState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Member PollCreator { get; set; }
    }
}
