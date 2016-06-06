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
        public int Id { get; set; }
        public string Answer { get; set; }
        public List<Member> Voters { get; set; }
    }
}
