using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Report
    {
        public int ReportId { get; set; }
        public byte[] Text { get; set; }
        public DateTime Time { get; set; }
        public int MemberMasterId { get; set; }
    }
}
