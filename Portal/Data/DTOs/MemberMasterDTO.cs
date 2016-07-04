using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemberMasterDTO
    {
        public int MemberMasterId { get; set; }
        public MemberDTO Master { get; set; }
        public MemberDTO Padawan { get; set; }
        public List<ReportDTO> Reports { get; set; }
    }
}
