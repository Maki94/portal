using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2.DTOs;
using Data2;

namespace MVC.Models
{
    public class LeaveHistoryPageModel
    {
        public List<LeaveStatusDTO> States { get; set; }

        public LeaveDTO leave { get; set; }

        public int LastStatus { get; set; }

        public static LeaveHistoryPageModel Load(LeaveDTO l)
        {
            LeaveHistoryPageModel model = new LeaveHistoryPageModel();

            model.States = new List<LeaveStatusDTO>();

            model.leave = l;

            model.leave.Comment = l.Comment;
            model.leave.Date = l.Date;
            model.leave.Duration = l.Duration;
            model.leave.StartDate = l.StartDate;
            model.leave.EmployeeID = l.EmployeeID;
            model.leave.EndDate = l.EndDate;


            List<LeaveStatusesHistory> states = LeaveHistories.getHistoryOfLeave(model.leave.LeaveID);

            foreach (LeaveStatusesHistory s in states)
            {
                model.States.Add(new LeaveStatusDTO()
                {
                    SubmitterID = s.submitterID,
                    SubFirstName = Employees.getEmplyeeAt(s.submitterID).firstName,
                    SubLastName = Employees.getEmplyeeAt(s.submitterID).lastName,
                    Status = s.leaveStatusEnumID,
                    StartDate = s.startDate,
                    EndDate = s.endDate,
                    Comment = s.comment
                });
            }

            model.LastStatus = (int)LeaveHistories.getLeaveLastStatus(l.LeaveID).leaveStatusEnumID ;

            return model;
        }
    }
}