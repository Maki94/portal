using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2.DTOs;
using Data2;

namespace MVC.Models
{
    public class TeamHistoryModel
    {
        public int TeamID { get; set; }
        public String TeamName { get; set; }
        public bool Deleted { get; set; }
        public List<EmployeeTeamDTO> List { get; set; }

        public static EmployeeTeamDTO createEmployeeTeamDTO(EmployeesTeamsHistory eth)
        {
            Employee emp = Employees.getEmplyeeAt(eth.employeeID);
            EmployeeTeamDTO et = new EmployeeTeamDTO
            {
                EmployeeID = eth.employeeID,
                Status = eth.endDate == null,
                Name = emp.firstName + " " + emp.lastName,
                Role = emp.roleID,
                StartDate = eth.startDate,
                EndDate = eth.endDate
            };
            return et;
        }

        public static TeamHistoryModel Load(int id)
        {
            TeamHistoryModel model = new TeamHistoryModel();
            model.TeamID = id;
            model.Deleted = Teams.getTeamAt(id).deleteDate != null;
            model.List = new List<EmployeeTeamDTO>();
            model.TeamName = Teams.getTeamAt(id).teamName;
            List<EmployeesTeamsHistory> list = Teams.getHistoryOfTeam(model.TeamID);
            foreach (var l in list)
                model.List.Add(createEmployeeTeamDTO(l));
            return model;
        }
    }
}