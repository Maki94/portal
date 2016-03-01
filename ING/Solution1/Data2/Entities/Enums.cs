using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2
{
    public static class Enums
    {
        public enum paid
        {
            unpaid,
            paid
        }
         
        public enum employeeStatus
        {
            Active,
            Suspended,
            Inactive
        }

        public enum leaveTypes
        {
            leave,
            medical
        }

        public enum leaveStatuses
        {
            pending,
            approved,
            rejected,
            cancelled
        }

        public enum gender
        {
            Female = 0,
            Male = 1
        }


        public enum Roles
        {
            Employee =1,
            Manager
          
        }

        public enum Permissions
        {
            EditEmp = 1,
            ViewEmployeeList,
            Respond,
            ViewLeavesList,
            Team_Add_Edit_Remove_View,
            Holiday_Add_Remove,
            ViewHolidayList,
            ViewProfile
        }

        public enum LoginStatus
        {
            Successful,
            Failed,
            IncorrectPassword
        }
       
           
        public static void addPermissionsEnums()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            foreach (var p in Enum.GetValues(typeof(Permissions)))
            {
                Permission perm = new Permission
                {
                    name = p.ToString(),
                };
                dc.Permissions.InsertOnSubmit(perm);
            }
            dc.SubmitChanges();
        }

        public static void addRolesEnums()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            foreach (var r in Enum.GetValues(typeof(Roles)))
            {
                Role role = new Role
                {
                    name = r.ToString(),
                };
                dc.Roles.InsertOnSubmit(role);
            }
            dc.SubmitChanges();
        }



        //public static List<string> getAllStatusesNames()
        //{
        //    List<string> l = new List<string>();
        //    foreach (leaveStatuses a in Enum.GetValues(typeof(leaveStatuses)))            
        //        l.Add(a.ToString());

        //    return l;
        //}
        //public static List<int> getAllStatusesValues()
        //{
        //    List<int> l = new List<int>();
        //    foreach (leaveStatuses a in Enum.GetValues(typeof(leaveStatuses)))
        //        l.Add((int)a);

        //    return l;
        //}

        //public static List<string> getAllTypesNames()
        //{
        //    List<string> l = new List<string>();
        //    foreach (leaveTypes a in Enum.GetValues(typeof(leaveTypes)))
        //        l.Add(a.ToString());

        //    return l;
        //}

        //public static List<string> getAllEmployeeStatusesNames()
        //{
        //    List<string> l = new List<string>();
        //    foreach (employeeStatus a in Enum.GetValues(typeof(employeeStatus)))
        //        l.Add(a.ToString());

        //    return l;
        //}


        public static void addLeaveStatusEnums()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeaveStatusEnum l1 = new LeaveStatusEnum
            {
                leaveStatusEnumName = "Pending",
                leaveStatusEnumID = 0
            };
            LeaveStatusEnum l2 = new LeaveStatusEnum
            {
                leaveStatusEnumName = "Approved",
                leaveStatusEnumID = 1
            };
            LeaveStatusEnum l3 = new LeaveStatusEnum
            { 
                leaveStatusEnumName = "Rejected",
                leaveStatusEnumID = 2
            };
            LeaveStatusEnum l4 = new LeaveStatusEnum
            {
                leaveStatusEnumName = "Cancelled",
                leaveStatusEnumID = 3
             };

            dc.LeaveStatusEnums.InsertOnSubmit(l1);
            dc.LeaveStatusEnums.InsertOnSubmit(l2);
            dc.LeaveStatusEnums.InsertOnSubmit(l3);
            dc.LeaveStatusEnums.InsertOnSubmit(l4);
            dc.SubmitChanges();
        }

        public static void addEmployeeStatusEnums()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            EmployeeStatusEnum l1 = new EmployeeStatusEnum
            {
                EmployeeStatusEnumName = "Active",
                EmployeeStatusEnumID = 0
            };
            EmployeeStatusEnum l2 = new EmployeeStatusEnum
            {
                EmployeeStatusEnumName = "Suspended",
                EmployeeStatusEnumID = 1
            };
            EmployeeStatusEnum l3 = new EmployeeStatusEnum
            {
                EmployeeStatusEnumName = "Inactive",
                EmployeeStatusEnumID = 2
            };
            dc.EmployeeStatusEnums.InsertOnSubmit(l1);
            dc.EmployeeStatusEnums.InsertOnSubmit(l2);
            dc.EmployeeStatusEnums.InsertOnSubmit(l3);
            dc.SubmitChanges();
        }

        public static void addLeaveTypeEnums()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            LeavesTypeEnum l1 = new LeavesTypeEnum
            {
                leaveTypeEnumName = "Leave",
                leaveTypeEnumID = 0
            };
            LeavesTypeEnum l2 = new LeavesTypeEnum
            {
                leaveTypeEnumName = "Medical",
                leaveTypeEnumID = 1
            };

            dc.LeavesTypeEnums.InsertOnSubmit(l1);
            dc.SubmitChanges();
            dc.LeavesTypeEnums.InsertOnSubmit(l2);
            dc.SubmitChanges();
        }

        public static void fillEnumTables()
        {
            addEmployeeStatusEnums();
            addLeaveStatusEnums();
            addLeaveTypeEnums();
        }

        public static List<int> getAllRoles()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            List<int> roles = new List<int>();
            foreach (var a in Enum.GetValues(typeof(Roles)))
            {
                roles.Add((int)a);
            }
            return roles;
        }

    }
}
