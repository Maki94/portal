using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data2.DTOs;

namespace Data2
{
    public class Login
    {
        public static LoginDTO createLoginDTO(LoginTransporterDTO ltd)
        {
            LoginDTO ld = new LoginDTO();
            Employee emp = Employees.checkUsernamePassword(ltd.Username, ltd.Password);
            if (emp == null)
            {
                if (Employees.checkUsername(ltd.Username))
                    ld.loginStatus = (int)Enums.LoginStatus.IncorrectPassword;
                else
                     ld.loginStatus = (int)Enums.LoginStatus.Failed;
                return ld;
            }
            
            ld.loginStatus = (int)Enums.LoginStatus.Successful;    
               
            ld.EmployeeID = emp.employeeID;
            ld.Username = emp.username;
            ld.Role = emp.roleID;
            ld.Permissions = Employees.getPermissions(ld.Role);
            ld.Name = emp.firstName + " " + emp.lastName;
            ld.RememberMe = ltd.RememberMe;

            return ld;
        }
    }
}
