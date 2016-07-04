using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemberProjectDTO
    {
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectWebsite { get; set; }
        public string TeamName { get; set; }
        public Enumerations.TeamRole TeamRole { get; set; }
        public string Function { get; set; }
    }
}
