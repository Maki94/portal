using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using Data2;

namespace MVC.Models
{
    public class EmployeeProfileModel
    {
        public Data2.DTOs.EmployeeDTO Employee { get; set; }
        public Models.ContactModel Contact { get; set; }
        public Models.EmployeesKidsModel Kids  { get; set; }
        public Models.TeamsOfEmployeeModel Teams { get; set; }

     

        public static Models.EmployeeProfileModel Load(int empID, bool current)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            Models.EmployeeProfileModel model = new EmployeeProfileModel();

            model.Employee = Models.EmployeePageModel.CreateDTOForID(empID);
            model.Employee.ContractEnd =
                (from a in dc.EmployeeStatusesHistories select a).OrderByDescending(m => m.contractStart)
                    .First()
                    .contractEnd;
            model.Kids = EmployeesKidsModel.Load(empID);
            model.Teams = TeamsOfEmployeeModel.Load(empID, current);
            model.Contact = ContactModel.Load(empID);

            return model;
        }
    }
}