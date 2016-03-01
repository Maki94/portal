using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LeaveModel
    {
        public int LeaveID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int leaveDaysNum { get; set; }
        public int type { get; set; }
        public DateTime date { get; set; }
        public int status { get; set; }
  

    }
}