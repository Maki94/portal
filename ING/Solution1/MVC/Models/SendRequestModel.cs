using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Data2;

namespace MVC.Models
{
    public class SendRequestModel
    {
        public int leaveID { get; set; }

        [Required]
        public int Type { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int Paid { get; set; }

        [DataType(DataType.MultilineText)]
        public String Comment { get; set; }

        public int LeaveDuration { get; set; }
        public Boolean ErrorOccurred { get; set; }
        public String Error { get; set; }
        public bool canOpen { get; set; }

        public static SendRequestModel Load(int leaveID)
        {
            SendRequestModel model = new SendRequestModel();
            Leave leave = LeaveHistories.getLeaveAt(leaveID);
            model.Type = leave.typeID;
            model.StartDate = leave.startDate;
            model.EndDate = leave.endDate;
            model.Comment = LeaveHistories.getLeaveStatusHistoryOfCreation(leaveID).comment;
            model.leaveID = leaveID;
            model.canOpen = CheckPermission.CheckIsLogIn();
            return model;
        }
    }

   
}