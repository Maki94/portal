using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class LoginTransporterDTO
    {
        [DisplayName ("Username *")]
        public String Username { get; set; }
        [DisplayName ("Password *")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public Boolean RememberMe { get; set; }
    }
}
