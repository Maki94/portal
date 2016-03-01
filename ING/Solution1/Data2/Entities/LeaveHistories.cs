using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Data2.DTOs;

namespace Data2
{
    //sve ovo bi trbalo da pripada klasi Emplyees ali je ona vec prilicno prenatrpana 
    public static class LeaveHistories
    {

        public static RequestDTO checkRequest(int empID, Enums.leaveTypes type, DateTime start, DateTime end,
            string comment = null)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();

                RequestDTO result = new RequestDTO();


                //provera da li ima dovoljno slobodnih dana
                DateTime? empSlava = LeaveDaysCalculation.getSlava(empID);

                int avaibleLeaveDays =
                    LeaveDaysCalculation.LeaveDaysNumber(empID, new DateTime(DateTime.Today.Year, 12, 31)).Total;

                result.LeaveDuration = LeaveDaysCalculation.OnlyLeaveDayNumber(start, end, end, empSlava);

                if (result.LeaveDuration > avaibleLeaveDays)
                {
                    result.ErrorOccurred = true;
                    result.Error += "You do not have enough days off";
                    return result;
                }
                else
                {
                    result.ErrorOccurred = false;
                }
                return result;
        }

        public static void sendRequest(int empID, Enums.leaveTypes type, Enums.paid paid, DateTime start, DateTime end, string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            //RequestDTO result = new RequestDTO();

            
            ////provera da li ima dovoljno slobodnih dana
            //Employee emp = Employees.getEmplyeeAt(empID, dc);
        
            //int avaibleLeaveDays = LeaveDaysCalculation.LeaveDaysNumber(empID, new DateTime(DateTime.Today.Year,12,31)).Total;
            //result.LeaveDuration = LeaveDaysCalculation.OnlyLeaveDayNumber(start, end, end);

            //if (result.LeaveDuration > avaibleLeaveDays)
            //{
            //    result.ErrorOccurred = true;
            //    result.Error += "You do not have enough days off";
            //    return result;
            //}

            //provera da li osatje bar pola tima 
            //List<Team> teams = Teams.teamsOfEmployee(empID, DateTime.Today);
            //foreach(Team t in teams)
            //{
            //    if(Teams.getEmployeesInTeamNum(t.teamID, DateTime.Today) >= 5 )
            //    {
            //        int allNum = Teams.getEmployeesInTeamNum(t.teamID, start);
            //        int workingNum = Teams.getAllWorkingEmployeesNum(t.teamID, start);
            //        if( (double)(workingNum -1) / (double)allNum <= 0.5 )
            //        {
            //            Error += "Vise od pola tima ce ostati prazno";
            //            Error += "Tim:" + t.teamName;
            //            return Error;
            //        }                              
            //    }
            //}
            

            Leave l = new Leave
            {
                employeeID = empID,
                typeID = (int)type,
                startDate = start,
                endDate = end,
                paid = Convert.ToBoolean((int)paid)
            };
            dc.Leaves.InsertOnSubmit(l);
            dc.SubmitChanges();
            

            LeaveStatusesHistory lsh = new LeaveStatusesHistory
            {
                leaveID = l.leaveID,
                comment = comment,
                startDate = DateTime.Today,
                endDate = null,
                submitterID = empID,
                leaveStatusEnumID = (int)Enums.leaveStatuses.pending
            };
            dc.LeaveStatusesHistories.InsertOnSubmit(lsh);
            dc.SubmitChanges();

        }

        public static List<LeaveStatusesHistory> getHistoryOfLeave(int leaveID, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            return (from lsh in dc.LeaveStatusesHistories where lsh.leaveID == leaveID select lsh).ToList<LeaveStatusesHistory>();
        }

        public static void updateRequest(int leaveID, int submitterID, Enums.leaveStatuses response, string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeaveStatusesHistory l = getLeaveLastStatus(leaveID, dc);
            l.endDate = DateTime.Today;
            dc.SubmitChanges();
            
            LeaveStatusesHistory lsh = new LeaveStatusesHistory
            {
                leaveID = leaveID,
                comment = comment,
                startDate = DateTime.Today,
                endDate = null,
                submitterID = submitterID,               
                leaveStatusEnumID = (int)response
            };
            
            dc.LeaveStatusesHistories.InsertOnSubmit(lsh);
            dc.SubmitChanges();
        }

        public static void cancelRequest(int leaveID, string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeaveStatusesHistory l = getLeaveLastStatus(leaveID, dc);
            if (l.leaveStatusEnumID != (int)Enums.leaveStatuses.cancelled)
                updateRequest(leaveID, getLeaveAt(leaveID).employeeID, Enums.leaveStatuses.cancelled, comment);
        }
        public static void approveRequest(int leaveID, int submitterID, string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeaveStatusesHistory l = getLeaveLastStatus(leaveID, dc);
            if(l.leaveStatusEnumID ==  (int)Enums.leaveStatuses.pending)
                updateRequest( leaveID,  submitterID, Enums.leaveStatuses.approved,  comment = null);
        }
        public static void rejectRequest(int leaveID, int submitterID,  string comment = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeaveStatusesHistory l = getLeaveLastStatus(leaveID, dc);
            if (l.leaveStatusEnumID == (int)Enums.leaveStatuses.pending)
                updateRequest(leaveID, submitterID, Enums.leaveStatuses.rejected, comment = null);
        }
               
        public static Leave getLeaveAt(int id, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            return (from a in dc.Leaves where a.leaveID == id select a).First();
        }

        public static LeaveStatusesHistory getLeaveLastStatus(int id, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            return (from a in dc.LeaveStatusesHistories where a.leaveID == id && a.endDate == null select a).First();

        }

        public static LeaveStatusesHistory getLeaveStatusHistoryOfCreation(int id, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            return (from a in dc.LeaveStatusesHistories where a.leaveID == id && a.leaveStatusEnumID == (int)Enums.leaveStatuses.pending select a).First();
        }

       

       


        public static List<Leave> getAllLeaveDays(Enums.leaveStatuses? filter = null, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            if (filter != null)
                return (from a in dc.Leaves
                        join b in dc.LeaveStatusesHistories on a.leaveID equals b.leaveID
                        where b.endDate == null && b.leaveStatusEnumID == (int)filter 
                        select a
                        ).ToList<Leave>();
                else
                    return (from a in dc.Leaves select a).ToList<Leave>();

        }

        public static List<Leave> getAllLeavesOfEmployee(int id, Enums.leaveStatuses? filter = null, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            if (filter != 0)
                return (from a in dc.Leaves
                        join b in dc.LeaveStatusesHistories on a.leaveID equals b.leaveID
                        where a.employeeID == id && b.endDate == null && b.leaveStatusEnumID == (int)filter
                        select a
                       ).ToList<Leave>();
            else
                return (from a in dc.Leaves where a.employeeID == id select a).ToList<Leave>();

        }

        public static Boolean isOnVacation(int empID, DateTime when)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            List<Leave> activeLeaves = (from a in dc.Leaves
                                        join b in dc.LeaveStatusesHistories on a.leaveID equals b.leaveID
                                        where a.employeeID == empID && b.endDate == null && b.leaveStatusEnumID == (int)Enums.leaveStatuses.approved
                                        select a).ToList<Leave>();

                foreach (Leave l in activeLeaves)
            {
                if (l.startDate.Date <= when && when <= l.endDate.Date)
                    return true;
            }

            return false;

        }



        public static void EditLeave(int leaveID, DateTime? startDate = null, DateTime? endDate = null, String comment = null, int? type = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Leave l = getLeaveAt(leaveID,dc);

            if (startDate != null)
                l.startDate = (DateTime)startDate;
            if (endDate != null)
                l.endDate = (DateTime)endDate;
            if (type != null)
                l.typeID = (int)type;

            LeaveStatusesHistory lsh = LeaveHistories.getLeaveStatusHistoryOfCreation(leaveID, dc);
            lsh.startDate = DateTime.Today;

            if (comment != null)
                lsh.comment = comment;

            dc.SubmitChanges();

           
        }

    }
}
