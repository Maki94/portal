using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class EmployeePageDTO
    {
        public int EmployeeID { get; set; }
        public int Status { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int LeaveDaysNum { get; set; }
        public int Role { get; set; }
    }
}
