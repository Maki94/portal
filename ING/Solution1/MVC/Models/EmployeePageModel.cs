using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2.DTOs;
using Data2;

namespace MVC.Models
{
    public struct Sort
    {
        public Boolean? Date { get; set; }
        public Boolean? Status { get; set; }
        public Boolean? Name { get; set; }
        public Boolean? StartDate { get; set; }
        public Boolean? EndDate { get; set; }
        public Boolean? Duration { get; set; }
        public Boolean? Type { get; set; }
    }

    public class EmployeePageModel
    {
        public List<EmployeePageDTO> EmpsList { get; set; }
        public int? StatusFilter { get; set; }
        public int? TeamFilter { get; set; }
        public Boolean? onHoliday { get; set; }
        public int? RoleFilter { get; set; }
        public string Search { get; set; } 



        public static EmployeePageDTO CreatePageDTOForID(int id)
        {
            Employee emp = Employees.getEmplyeeAt(id);
            EmployeePageDTO a = new EmployeePageDTO
            {
                EmployeeID = emp.employeeID,
                FirstName = emp.firstName,
                LastName = emp.lastName,
                LeaveDaysNum = LeaveDaysCalculation.LeaveDaysNumber(id, DateTime.Now).Total,
                Status = Employees.getLastStatus(id).emplyeeStatusEnumID,
                Role = emp.roleID
            };
            return a;
        }

        public static EmployeeDTO CreateDTOForID(int id)
        {
            Employee emp = Employees.getEmplyeeAt(id);
            EmployeeDTO a = new EmployeeDTO
            {
                EmployeeID = emp.employeeID,
                FirstName = emp.firstName,
                LastName = emp.lastName,
                LeaveDaysNum = LeaveDaysCalculation.LeaveDaysNumber(id, DateTime.Now).Total,
                Status = Employees.getLastStatus(id).emplyeeStatusEnumID,
                DateOfBirth = emp.dateOfBirth == null ? new DateTime(2001, 1, 1) : (DateTime)emp.dateOfBirth,
                Username = emp.username,
                Password = emp.password,
                Role = emp.roleID
                

            };
            return a;
        }



        public static EmployeePageModel Load(string search, int? status = null, int? teamFilter = null, Boolean? onHoliday = null, int? roleFilter = null)
        {
            EmployeePageModel model = new EmployeePageModel();
            model.EmpsList = new List<EmployeePageDTO>();

            model.StatusFilter = status;
            model.TeamFilter = teamFilter;
            model.onHoliday = onHoliday;
            model.RoleFilter = roleFilter;
            List<Employee> list;

            list = Employees.getAllEmployees();

            if (onHoliday == true)
                list = (from a in list where LeaveHistories.isOnVacation(a.employeeID, DateTime.Today) select a).ToList();

            else
                list = (from a in list where !LeaveHistories.isOnVacation(a.employeeID, DateTime.Today) select a).ToList();



            if (roleFilter != null)
            {
                list = (from a in list where a.roleID == roleFilter select a).ToList();
            }


            if (status != null)
                list = (from a in list where Employees.getLastStatus(a.employeeID).emplyeeStatusEnumID == (int)model.StatusFilter select a).ToList();

            if (teamFilter != null)
            {
                if ((int)teamFilter == int.MaxValue)
                    list = (from a in list where Teams.teamsOfEmployee(a.employeeID, DateTime.Today).Count == 0 select a).ToList();
                else
                    list = (from a in list where Teams.employeeBelongsToTeam(a.employeeID, (int)model.TeamFilter) select a).ToList();

            }

            if (search != null)
            {
                list =
                    (from a in list where a.firstName.ToLower().Contains(search.ToLower()) || a.lastName.ToLower().Contains(search.ToLower()) select a).ToList();
            }

            foreach (Employee a in list)
                model.EmpsList.Add(CreatePageDTOForID(a.employeeID));

            string sort = (String)HttpContext.Current.Session["EmpSort"];

            if (sort != null)
            {
                string[] sortList = sort.Split(' ');
                sortList.Reverse();
                foreach(string s in sortList)
                {
                    switch (s)
                    {
                        case "StatusAsc":
                            model.EmpsList = model.EmpsList.OrderBy(m =>m.Status).ToList();
                            break;
                        case "StatusDesc":
                            model.EmpsList = model.EmpsList.OrderByDescending(m =>m.Status).ToList();
                            break;

                        case "RoleAsc":
                            model.EmpsList = model.EmpsList.OrderBy(m =>m.Role).ToList();
                            break;
                        case "RoleDesc":
                            model.EmpsList = model.EmpsList.OrderByDescending(m => m.Role).ToList();
                            break;

                        case "NameAsc":
                            model.EmpsList = model.EmpsList.OrderBy(m => m.LastName).ToList();
                            break;
                        case "NameDesc":
                            model.EmpsList = model.EmpsList.OrderByDescending(m => m.LastName).ToList();
                            break;

                        case "LDNAsc":
                            model.EmpsList = model.EmpsList.OrderBy(m => m.LeaveDaysNum).ToList();
                            break;
                        case "LDNDesc":
                            model.EmpsList = model.EmpsList.OrderByDescending(m => m.LeaveDaysNum).ToList();
                            break;
                    }
                }
            }
                
                


            return model;

        }
    }

  
}