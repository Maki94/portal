using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class LoginDTO
    {
        public int EmployeeID { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public Boolean RememberMe { get; set; }
        public List<int> Permissions { get; set; }
        public int Role { get; set; }
        public int loginStatus { get; set; }
    }
}
