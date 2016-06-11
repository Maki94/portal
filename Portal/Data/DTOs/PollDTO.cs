using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy u hh:mm}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy u hh:mm}")]
        public DateTime EndDate { get; set; }
        public Member PollCreator { get; set; }
    }
}
