using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2;
using Data2.DTOs;

namespace MVC.Models
{
    public class TeamsOfEmployeeModel
    {
        public int EmployeeID { get; set; }
        public String EmployeeName { get; set; }
        public List<TeamsOfEmployeeDTO> TeamsList { get; set; }
        public bool CurrentFilter { get; set; }

        public static TeamsOfEmployeeModel Load(int empID, bool current = true)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            TeamsOfEmployeeModel model = new TeamsOfEmployeeModel();
            model.EmployeeID = empID;
            model.CurrentFilter = current;
            Employee emp = Employees.getEmplyeeAt(model.EmployeeID);
            model.EmployeeName = emp.firstName + " " + emp.lastName;
            model.TeamsList = (from a in dc.EmployeesTeamsHistories
                               join b in dc.Teams on a.teamID equals b.teamID
                               where a.employeeID == empID && (!current || a.endDate == null)
                               select new TeamsOfEmployeeDTO { TeamID = b.teamID, TeamName = b.teamName, DateOfCreation = b.creationDate, DateOfDeletion = b.deleteDate, LastStartDate = a.startDate, LastEndDate = a.endDate}).OrderBy(a=>a.LastStartDate).ToList();

            string sort = (String)HttpContext.Current.Session["TeamsOfEmpSort"];

            if (sort != null)
            {
                string[] sortList = sort.Split(' ');
                sortList.Reverse();
                foreach (string s in sortList)
                {
                    switch (s)
                    {

                        case "NameAsc":
                            model.TeamsList = model.TeamsList.OrderBy(m => m.TeamName).ToList();
                            break;
                        case "NameDesc":
                            model.TeamsList = model.TeamsList.OrderByDescending(m => m.TeamName).ToList();
                            break;

                        case "DateOfCreationAsc":
                            model.TeamsList = model.TeamsList.OrderBy(m => m.DateOfCreation).ToList();
                            break;
                        case "DateOfCreationDesc":
                            model.TeamsList = model.TeamsList.OrderByDescending(m => m.DateOfCreation).ToList();
                            break;

                        case "DateOfDeletionAsc":
                            model.TeamsList = model.TeamsList.OrderBy(m => m.DateOfDeletion).ToList();
                            break;
                        case "DateOfDeletionDesc":
                            model.TeamsList = model.TeamsList.OrderByDescending(m => m.DateOfDeletion).ToList();
                            break;
                          
                        case "LastStartDateAsc":
                            model.TeamsList = model.TeamsList.OrderBy(m => m.LastStartDate).ToList();
                            break;
                        case "LastStartDateDesc":
                            model.TeamsList = model.TeamsList.OrderByDescending(m => m.LastStartDate).ToList();
                            break;

                        case "LastEndDateAsc":
                            model.TeamsList = model.TeamsList.OrderBy(m => m.LastEndDate).ToList();
                            break;
                        case "LastEndDateDesc":
                            model.TeamsList = model.TeamsList.OrderByDescending(m => m.LastEndDate).ToList();
                            break;
                    }
                }
            }


            return model;
        }
    }
}