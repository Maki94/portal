using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{

    public class EmailDTO
    {
        public int EmailID { get; set; }
        public string Email { get; set; }         
    }
    public class PhoneDTO
    {
        public int PhoneID { get; set; }
        public string Phone { get; set; }
    }
    public class AddressDTO
    {
        public int AddressID { get; set; }
        public string Address { get; set; }
    }
}
