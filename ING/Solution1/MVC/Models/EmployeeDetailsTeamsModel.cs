using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Data2;
using Data2.DTOs;

namespace MVC.Models
{
    public class EmployeeDetailsTeamsModel
    {

        public List<EmployeeDetailsTeamsDTO> TeamsList { get; set; }
     

        public static EmployeeDetailsTeamsModel Load(int empID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            EmployeeDetailsTeamsModel model = new EmployeeDetailsTeamsModel();
          
            model.TeamsList = (from a in dc.EmployeesTeamsHistories
                        join b in dc.Teams on a.teamID equals b.teamID
                        where a.employeeID == empID && a.endDate == null 
                        select new EmployeeDetailsTeamsDTO { TeamID = b.teamID, TeamName = b.teamName, StartDate = a.startDate}).OrderByDescending(a=>a.StartDate).ToList();

            return model;
        }
    }

  
}