using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2.DTOs;
using Data2;
using Newtonsoft.Json.Converters;
using Org.BouncyCastle.Crypto.Paddings;

namespace MVC.Models
{
    public class EmployeeStatusModel
    {
        public int EmployeeID { get; set; }
        public List<EmployeeStatusDTO> List { get; set; }
        public String EmpName { get; set; }

        public static EmployeeStatusDTO createStatusDTO(EmployeeStatusesHistory esh)
        {
            byte[] file = null;
            if (esh.filePDF != null)
            {
                file = esh.filePDF.ToArray();
            }
            EmployeeStatusDTO e = new EmployeeStatusDTO()
            {
                StatusID = esh.emplyeeStatusHistoryID,
                EmployeeID = esh.employeeID,
                SubmitterID = esh.submitterID,
                SubmitterName = Employees.getEmployeeFullName(esh.submitterID),
                Status = esh.emplyeeStatusEnumID,
                StartDate = esh.contractStart,
                EndDate = esh.contractEnd,
                InsertDate = esh.insertDate,
                FileName = esh.fileName,
                comment = esh.comment,
                ContractDuration = esh.contractDuration
            };
            return e;

        }

        public static EmployeeStatusModel Load(int id)
        {
            EmployeeStatusModel model = new EmployeeStatusModel();
            
            model.EmployeeID = id;
            model.EmpName = Employees.getEmployeeFullName(model.EmployeeID);
            model.List = new List<EmployeeStatusDTO>();
            List<EmployeeStatusesHistory> l = Employees.getAllStatusesOfEmployee(model.EmployeeID);
            foreach (var a in l)
                model.List.Add(createStatusDTO(a));

            return model;
        }


    
    }
}