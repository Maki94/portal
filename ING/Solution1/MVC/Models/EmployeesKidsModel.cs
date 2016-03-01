using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2.DTOs;
using Data2;

namespace MVC.Models
{
    public class EmployeesKidsModel
    {
        public int EmployeeID { get; set; }
        public String FristName { get; set; }
        public String LastName { get; set; }
        public List<KidDTO> Kids { get; set; }


        public static EmployeesKidsModel Load(int id)
        {
            EmployeesKidsModel model = new EmployeesKidsModel();
            model.EmployeeID = id;
            model.FristName = Employees.getEmplyeeAt(id).firstName;
            model.LastName = Employees.getEmplyeeAt(id).lastName;
            model.Kids = new List<KidDTO>();

            List<Kid> kids = Employees.getEmployeesKids(model.EmployeeID);
          
            foreach(Kid k in kids)
            {

                List<int> parents = Employees.getParentsOfKid(k.kidID);
                List<string> pNames = new List<string>();
                foreach (var a in parents)
                    pNames.Add(Employees.getEmployeeFullName(a));
                model.Kids.Add(new KidDTO
                {
                    kidID = k.kidID,
                    FirstName = k.name,
                    DateOfBirth = k.dateOfBirth,
                    Gender = k.gender,
                    Parents = parents,
                    ParentsNames = pNames
                });
            }

            return model;
        }
    }
}