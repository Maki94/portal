using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class ChangePasswordDTO
    {
        public int employeeID { get; set; }

        public string password { get; set; }
        [Required]
        public string oldPassword { get; set; }

        [Required]
        public string newPassword { get; set; }

        [Required]
        public string repeatPassword { get; set; }
    }

    public class Create
    {
        public static ChangePasswordDTO ChangePassDTO(int empID)
        {
            ChangePasswordDTO model=new ChangePasswordDTO();
            model.employeeID = empID;
            model.password = Employees.getEmplyeeAt(empID).password;

            return model;
        }
    }
}
