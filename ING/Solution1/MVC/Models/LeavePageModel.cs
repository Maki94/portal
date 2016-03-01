using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2;
using Data2.DTOs;
using System.Web.Mvc;


namespace MVC.Models
{
    public class LeavePageModel
    {
        public List<LeaveDTO> leavesList { get; set; }

        public int? StatusFilter { get; set; }

        public int? TeamFilter { get; set; }

        public int? TypeFilter { get; set; }

        public int? EmpFilter { get; set; }

        public DateTime? StartDateFilter { get; set; }

        public DateTime? EndDateFilter { get; set; }

        public DateTime? OnDateFilter { get; set; }

        public string Search { get; set; }

        public SendRequestModel SendRequestModel { get; set; }

        public static LeaveDTO createDTOForID(int id)
        {
             Leave l = LeaveHistories.getLeaveAt(id);
            Employee e = Employees.getEmplyeeAt(l.employeeID);
            Enums.leaveStatuses currStatus = (Enums.leaveStatuses)LeaveHistories.getLeaveLastStatus(l.leaveID).leaveStatusEnumID;
            LeaveDTO a = new LeaveDTO()
            {
                LeaveID = l.leaveID,

                EmployeeID = l.employeeID,

                FirstName = e.firstName,

                LastName = e.lastName,

                Role = Employees.getEmplyeeAt(l.employeeID).roleID,

                Status = (int)currStatus,

                Date = LeaveHistories.getLeaveStatusHistoryOfCreation(l.leaveID).startDate,

                StartDate = l.startDate,

                EndDate = l.endDate,

                Duration = LeaveDaysCalculation.OnlyLeaveDayNumber(l.startDate, l.endDate, l.endDate,e.slava),

                Type = ((Enums.leaveTypes)l.typeID).ToString(),

                Comment = LeaveHistories.getLeaveStatusHistoryOfCreation(l.leaveID).comment,
                
                Paid = l.paid
            };
            return a;
        }  
        


        public static LeavePageModel Load(string search, int? status = null, int? team = null, int? type = null, DateTime? startDate = null, DateTime? endDate = null, int? empFilter = null, string sort = null, DateTime? onDateFilter = null)
        {
            var model = new LeavePageModel();
            model.SendRequestModel = null;
            
            
            
            model.StatusFilter = status;
            model.TeamFilter = team;
            model.TypeFilter = type;
            model.StartDateFilter = startDate;
            model.EndDateFilter = endDate;
            model.EmpFilter = empFilter;
            model.OnDateFilter = onDateFilter;

            List<LeaveDTO> dtoList = new List<LeaveDTO>();
            List<Leave> pomList;
            if (model.StatusFilter != null)
                 pomList = LeaveHistories.getAllLeaveDays((Enums.leaveStatuses)status);
            else
                 pomList = LeaveHistories.getAllLeaveDays();

            
            if (model.EmpFilter != null)
            {
                pomList = (from a in pomList where a.employeeID == empFilter select a).ToList();
            }
            else
            {
                if (model.TeamFilter != null)
                {
                    pomList = (from a in pomList where Teams.employeeBelongsToTeam(a.employeeID, (int)model.TeamFilter) select a).ToList();
                }               
            }

            if (model.TypeFilter != null)
            {
                pomList = (from a in pomList where a.typeID == (int)type select a).ToList();
            }
            
            if(model.OnDateFilter != null)
            {
                pomList = (from a in pomList where a.startDate <= model.OnDateFilter && a.endDate >= model.OnDateFilter select a).ToList();
            }
            else
            {
                if (model.StartDateFilter != null)
                {
                    pomList = (from a in pomList where a.startDate >= model.StartDateFilter select a).ToList();
                }

                if (model.EndDateFilter != null)
                {
                    pomList = (from a in pomList where a.endDate <= model.EndDateFilter select a).ToList();
                }

            }

            if (search != null)
            {
                
                pomList =
                    (from a in pomList where Employees.getEmployeeFullName(a.employeeID).ToLower().Contains(search.ToLower())  select a).ToList();
            }


            foreach (Leave l in pomList)
            {
                Enums.leaveStatuses currStatus = (Enums.leaveStatuses)LeaveHistories.getLeaveLastStatus(l.leaveID).leaveStatusEnumID;
                dtoList.Add(createDTOForID(l.leaveID));            
            }

            if(sort == null)
            {
                sort = "StartDateAsc StatusAsc";
            }

            model.leavesList = dtoList;
            if (sort != null)
            {
                string[] sortList = sort.Split(' ');
                sortList.Reverse();
                foreach (string s in sortList)
                {
                    switch (s)
                    {

                        // Date Status  FirstName StartDate   EndDate Duration    Type Comment
                        case "DateAsc":
                            model.leavesList = model.leavesList.OrderBy(m => m.Date).ToList();
                            break;
                        case "DateDesc":
                            model.leavesList = model.leavesList.OrderByDescending(m => m.Date).ToList();
                            break;
                        case "StatusAsc":
                             model.leavesList =  model.leavesList.OrderBy(m => m.Status).ToList();
                            break;
                        case "StatusDesc":
                             model.leavesList =  model.leavesList.OrderByDescending(m => m.Status).ToList();
                            break;

                        case "NameAsc":
                             model.leavesList =  model.leavesList.OrderBy(m => m.LastName).ToList();
                            break;
                        case "NameDesc":
                             model.leavesList =  model.leavesList.OrderByDescending(m => m.LastName).ToList();
                            break;
                        

                        case "StartDateAsc":
                             model.leavesList =  model.leavesList.OrderBy(m => m.StartDate).ToList();
                            break;
                        case "StartDateDesc":
                             model.leavesList =  model.leavesList.OrderByDescending(m => m.StartDate).ToList();
                            break;

                        case "EndDateAsc":
                            model.leavesList = model.leavesList.OrderBy(m => m.EndDate).ToList();
                            break;
                        case "EndDateDesc":
                            model.leavesList = model.leavesList.OrderByDescending(m => m.EndDate).ToList();
                            break;

                        case "DurationAsc":
                            model.leavesList = model.leavesList.OrderBy(m => m.Duration).ToList();
                            break;
                        case "DurationDesc":
                            model.leavesList = model.leavesList.OrderByDescending(m => m.Duration).ToList();
                            break;

                        case "TypeAsc":
                            model.leavesList = model.leavesList.OrderBy(m => m.Type).ToList();
                            break;
                        case "TypeDesc":
                            model.leavesList = model.leavesList.OrderByDescending(m => m.Type).ToList();
                            break;

                        case "PaidAsc":
                            model.leavesList = model.leavesList.OrderBy(m => m.Paid).ToList();
                            break;
                        case "PaidDesc":
                            model.leavesList = model.leavesList.OrderByDescending(m => m.Paid).ToList();
                            break;
                    }
                }
            }

            return model;
        }


    }

   

   
}