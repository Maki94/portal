using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PollOptionDTO
    {
        public int PollOptionId { get; set; }
        public string Answer { get; set; }
        public int PollId { get; set; }
        public List<int> VotersIds { get; set; }
    }
}
