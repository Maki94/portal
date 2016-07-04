using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ReportDTO
    {
        public int ReportId { get; set; }
        public byte[] Text { get; set; }
        public DateTime Time { get; set; }
        public int MemberMasterId { get; set; }
    }
}
