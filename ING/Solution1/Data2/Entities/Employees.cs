using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2
{
    public class Employees
    {      
        public static Employee addFirstEmployee(string first, string last, string user, string pass, DateTime conStart, DateTime? conEnd, int status )
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Employee e = new Employee
                {
                    firstName = first,
                    lastName = last,
                    username = user,
                    password = pass,
                    roleID = (int) Enums.Roles.Manager
                };
                dc.Employees.InsertOnSubmit(e);
                dc.SubmitChanges();

                EmployeeStatusesHistory esh = new EmployeeStatusesHistory
                {
                    employeeID = e.employeeID,
                    emplyeeStatusEnumID = status,
                    submitterID = e.employeeID,
                    contractStart = conStart,
                    contractEnd = conEnd,
                    insertDate = DateTime.Now
                };

                dc.EmployeeStatusesHistories.InsertOnSubmit(esh);
                dc.SubmitChanges();


                return e;
          
        }
        //pazi ovde, izmenila sam da se ne dodaju radnici sad nego godinu dana ranije
        public static Employee addEmployee(int roleID, int submitterID, string first, string last, string user, string pass, DateTime? dateOfBirth, DateTime conStart, DateTime? conEnd)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Employee e = new Employee
                {
                    firstName = first,
                    lastName = last,
                    username = user,
                    password = pass,
                    dateOfBirth = dateOfBirth,
                    roleID = roleID
                };
                dc.Employees.InsertOnSubmit(e);
                dc.SubmitChanges();
                EmployeeStatusesHistory esh = new EmployeeStatusesHistory
                {
                    employeeID = e.employeeID,
                    emplyeeStatusEnumID = (int)Enums.employeeStatus.Active,
                    submitterID = submitterID,
                    contractStart = conStart,
                    contractEnd = conEnd,
                    insertDate = DateTime.Now
                };

                dc.EmployeeStatusesHistories.InsertOnSubmit(esh);
                dc.SubmitChanges();
                return e;
        }
        

        public static void updateUserRole(int empID, int role)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Employee emp = getEmplyeeAt(empID, dc);
                emp.roleID = role;
                dc.SubmitChanges();
            
        }

        public static void updateEmployeeStatus(int empID, int subID, Enums.employeeStatus stat, byte[] File, string FileName, DateTime StartDate, DateTime? EndDate, int? duration = null,  string comment = null)
        { 
                DataClasses1DataContext dc = new DataClasses1DataContext();


                EmployeeStatusesHistory esh = new EmployeeStatusesHistory
                {
                    employeeID = empID,
                    submitterID = subID,
                    comment = comment,
                    emplyeeStatusEnumID = (int)stat,
                    insertDate = DateTime.Today,
                    contractStart = StartDate,
                    contractEnd = EndDate,
                    contractDuration = duration,
                    fileName = FileName,
                    filePDF = new System.Data.Linq.Binary(File)
                };
                dc.EmployeeStatusesHistories.InsertOnSubmit(esh);
                dc.SubmitChanges();
                   
        }



        public static EmployeeStatusesHistory getLastStatus(int empID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeeStatusesHistories where a.employeeID == empID select a).OrderBy(a => a.contractStart).ToList().Last();
        }

        public static List<EmployeeStatusesHistory> getAllStatusesOfEmployee(int empID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeeStatusesHistories where a.employeeID == empID select a).ToList();

        }
    
        public static List<EmployeeStatusesHistory> getAllActiveStatusesOfEmployee(int empID,  DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeeStatusesHistories where a.employeeID == empID && a.emplyeeStatusEnumID == (int)Enums.employeeStatus.Active select a).ToList();

        }

        public static DateTime getFirstStartDate(int empID)
        {
                return (DateTime)Employees.getAllActiveStatusesOfEmployee(empID).OrderBy(a => a.contractStart).First().contractStart;
        }

        public static void updateEmployee(int empID, int? role = null,  string firstName = null, string lastName = null, string password = null, DateTime? dob = null)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Employee emp = (from a in dc.Employees where a.employeeID == empID select a).First();
                if (role != null)
                    emp.roleID = (int)role;
                if (firstName != null)
                    emp.firstName = firstName;
                if (lastName != null)
                    emp.lastName = lastName;
                if (password != null)
                    emp.password = password;
                if (dob != null)
                    emp.dateOfBirth = dob;
                dc.SubmitChanges();
        }

        public static void changePassword(int employeeID, string newPassword)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Employee emp = getEmplyeeAt(employeeID, dc);
            emp.password = newPassword;
            dc.SubmitChanges();
        }

        public static void editFirstName(int empID, string name)    {   updateEmployee(empID, firstName: name);  }
        public static void editLastName(int empID, string name)     {    updateEmployee(empID, lastName: name);  }
        public static void editPassword(int empID, string pass)     {    updateEmployee(empID, password: pass);  }

        public static void addKidToEmployee(int empID, string name, bool gender, DateTime dateOfBirth)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Kid k = new Kid
            {
                name = name,
                gender = gender,
                dateOfBirth = dateOfBirth
            };
            dc.Kids.InsertOnSubmit(k);
            dc.SubmitChanges();
            EmployeesKid ek = new EmployeesKid
            {
                employeeID = empID,
                kidID = k.kidID
            };

            dc.EmployeesKids.InsertOnSubmit(ek);
            dc.SubmitChanges();
        }

        public static void addKidToEmployees(int[] empIDs, string name, bool gender, DateTime dateOfBirth)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Kid k = new Kid
            {
                name = name,
                gender = gender,
                dateOfBirth = dateOfBirth
            };
            dc.Kids.InsertOnSubmit(k);
            dc.SubmitChanges();
            foreach(int empID in empIDs)
            {
                EmployeesKid ek = new EmployeesKid
                {
                    employeeID = empID,
                    kidID = k.kidID
                };
                dc.EmployeesKids.InsertOnSubmit(ek);
                dc.SubmitChanges();
            }  
            
        }

        public static List<Kid> getEmployeesKids(int empID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeesKids where a.employeeID == empID select a.Kid).ToList<Kid>();
        }

        public static List<int> getParentsOfKid(int kidID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.EmployeesKids where a.kidID == kidID select a.employeeID).ToList<int>();
        }

        public static List<int> getEmployeesKidsIDs(int empID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.EmployeesKids where a.employeeID == empID select a.kidID).ToList<int>();
        }

        public static void addBonusToEmployee(int empID, int managerID, int bonus, string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            BonusDaysHistory bdh = new BonusDaysHistory
            {
                employeeID = empID,
                submitterID = managerID,
                bunusCount = bonus,
                date = DateTime.Today,
                comment = comment
            };
            dc.BonusDaysHistories.InsertOnSubmit(bdh);
            dc.SubmitChanges();
        }

        public static List<BonusDaysHistory> getAllBonuses(DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.BonusDaysHistories select a).ToList<BonusDaysHistory>();
        }

        public static List<BonusDaysHistory> getAllEmployeeBonusDays(int empID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.BonusDaysHistories where a.employeeID == empID select a).ToList<BonusDaysHistory>();
        }
        public static List<int> getAllBonusesIDs()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.BonusDaysHistories select a.BonusDayID).ToList<int>();
        }



        public static List<int> getEmployeesIDs()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Employees
                        select a.employeeID).ToList<int>();
        }

        public static List<Employee> getAllEmployees(DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                List<Employee> e = (from a in dc.Employees select a).ToList<Employee>();
                return e;
        }

        public static Employee getEmplyeeAt(int id, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.Employees where a.employeeID == id select a).First();
        }


        public static string getEmployeeFullName(int id)
        {
                Employee emp = getEmplyeeAt(id);
                return (emp.firstName + " " + emp.lastName);
        }
        public static Employee checkUsernamePassword(string user, string pass, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                var find = (from a in dc.Employees where a.username == user && a.password == pass select a);
                if (find.Any())
                    return find.First();
                else return null;
        }

        public static Boolean checkUsername(string user)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Employees where a.username == user select a).Any();
        }

        public static List<int> getPermissions(int roleID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.RolesPermissions where a.roleID == roleID select a.premissionID).ToList<int>();
        }


        public static void  addPhoneNumberToEmployee(int empID, string phoneNumber)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Telephone t = new Telephone { employeeID = empID, telephoneNumber = phoneNumber };
            dc.Telephones.InsertOnSubmit(t);
            dc.SubmitChanges();
        }

        public static void addPDF(int statusID, string fileName, byte[] file)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            EmployeeStatusesHistory status = getStatusAt(statusID, dc);
            status.fileName = fileName;
            status.filePDF = new System.Data.Linq.Binary(file);

            dc.SubmitChanges();

        }

        public static EmployeeStatusesHistory getStatusAt(int statusID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from esh in dc.EmployeeStatusesHistories where esh.emplyeeStatusHistoryID == statusID select esh).First();
        }

        public static void removePhoneNumberFromEmployee(int phoneID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Telephone t = (from a in dc.Telephones where a.telephoneID == phoneID select a).First();
            dc.Telephones.DeleteOnSubmit(t);
            dc.SubmitChanges();
        }
        public static void updateEmployeeTelephone(int phoneID, string newphone)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Telephone phone = (from a in dc.Telephones where a.telephoneID == phoneID select a).First();
            phone.telephoneNumber = newphone;
            dc.SubmitChanges();
        }
        public static List<Telephone> getAllPhoneNubersOfEmployee(int empID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Telephones where a.employeeID == empID select a).ToList();
        }

        public static void addEmailToEmployee(int empID, string mail)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Email e = new Email { employeeID = empID, emailAddress = mail };
            dc.Emails.InsertOnSubmit(e);
            dc.SubmitChanges();
        }

        public static void removeEmailFromEmployee(int emailID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Email e = (from a in dc.Emails where a.emailID==emailID select a).First();
            dc.Emails.DeleteOnSubmit(e);
            dc.SubmitChanges();
        }
        public static void updateEmployeeEmail(int emailID, string newemail)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Email email = (from a in dc.Emails where a.emailID == emailID select a).First();
            email.emailAddress = newemail;
            dc.SubmitChanges();
        }

        public static List<Email> getAllEmailsOfEmployee(int empID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Emails where a.employeeID == empID select a).ToList();
        }


        public static void addAdressToEmployee(int empID, string adress)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Address a = new Address { employeeID = empID, address1 = adress };
            dc.Addresses.InsertOnSubmit(a);
            dc.SubmitChanges();
        }

        public static void removeAdressFromEmployee(int addressID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Address ad = (from a in dc.Addresses where a.addressID == addressID select a).First();
            dc.Addresses.DeleteOnSubmit(ad);
            dc.SubmitChanges();
        }

        public static void updateEmployeeAddress(int addressID, string newaddress)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Address address = (from a in dc.Addresses where a.addressID == addressID select a).First();
            address.address1 = newaddress;
            dc.SubmitChanges();

        }
        public static List<Address> getAllAdressesOfEmployee(int empID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Addresses where a.employeeID == empID select a).ToList();
        }


        public static void changeKid(int kidID, string kidName, DateTime date, Boolean gender)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Kid k = (from a in dc.Kids where a.kidID == kidID select a).First();
            k.name = kidName;
            k.dateOfBirth = date;
            k.gender = gender;
            dc.SubmitChanges();
        }

    }
}
