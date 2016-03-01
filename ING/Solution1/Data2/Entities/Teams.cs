using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2
{
    public static class Teams
    {        
        public static Team createTeam(string name)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Team t = new Team
                {
                    teamName = name,
                    creationDate = DateTime.Today,
                    deleteDate = null
                };
                return t;
        }

        public static Team addTeam(string name)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Team t = new Team
                {
                    teamName = name,
                    creationDate = DateTime.Today,
                    deleteDate = null
                };
                dc.Teams.InsertOnSubmit(t);
                dc.SubmitChanges();
                return t;
        }

        public static Team addTeam(string name, int[] empIDs)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                Team t = addTeam(name);
                addEmployeesToTeam(empIDs, t.teamID);
                return t;
        }


        public static Team getTeamAt(int id, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.Teams where a.teamID == id select a).First();
        }

        public static List<int> getTeamsIDs()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                List<int> tIDs = (from a in dc.Teams
                                  select a.teamID).ToList<int>();
                return tIDs;
        }

        public static void changeTeamName(int teamID, string newName)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Team t = (from a in dc.Teams
                     where a.teamID == teamID
                     select a).First();
            t.teamName = newName;
            dc.SubmitChanges();
        }


        public static void addEmployeeToTeam(int empID, int teamID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            if (Teams.employeeBelongsToTeam(empID,teamID))
            {
                Console.WriteLine("Vec je u timu");
                return;
            }
            
            EmployeesTeamsHistory eth = new EmployeesTeamsHistory
            {
                employeeID = empID,
                teamID = teamID,
                startDate = DateTime.Today,
                endDate = null
            };

            dc.EmployeesTeamsHistories.InsertOnSubmit(eth);
            dc.SubmitChanges();
        }

        public static void addEmployeesToTeam(int[] empIDs, int teamID)
        {
            
            foreach (int i in empIDs)
            {
                addEmployeeToTeam(i, teamID);
            }         
        }

        public static void removeEmployeeFromTeam(int empID, int teamID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var find = from a in dc.EmployeesTeamsHistories
                       where a.employeeID == empID && a.teamID == teamID && a.endDate == null
                       select a;
            foreach (var a in find)
            {
                a.endDate = DateTime.Today;
            }

            dc.SubmitChanges();
        }

        public static List<Employee> getAllEmployeesFromTeam(int teamID, DateTime when, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                List<Employee> find = (from a in dc.EmployeesTeamsHistories
                                       join b in dc.Employees on a.employeeID equals b.employeeID
                                       where a.teamID == teamID && a.startDate <= when && (a.endDate == null || when <= a.endDate)
                                       select b).ToList<Employee>();
                return find;
        }

        public static List<int> getAllEmployeesIDsFromTeam(int teamID, DateTime? when = null)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                List<int> find;
                if (when != null)
                {
                    find = (from a in dc.EmployeesTeamsHistories
                            where a.teamID == teamID && a.startDate <= when && (a.endDate == null || when <= a.endDate)
                            select a.employeeID).ToList<int>();
                }
                else
                {
                    find = (from a in dc.EmployeesTeamsHistories
                            where a.teamID == teamID
                            select a.employeeID).ToList<int>();
                }

                return find;
        }

        public static void removeAllEmployes(int teamID)
        {
            List<int> emps = getAllEmployeesIDsFromTeam(teamID);
            foreach(int empID in emps)
            {
                removeEmployeeFromTeam(empID, teamID);
            }
        }

        public static void removeTeam(int teamID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Team t = (from a in dc.Teams
                      where a.teamID == teamID
                      select a).First();
            if(t.deleteDate == null)
            {
                //removeAllEmployes(teamID);
                t.deleteDate = DateTime.Today;
            }              
            dc.SubmitChanges();
        }

        public static List<int> getAllTeamsIDs()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Teams select a.teamID).ToList<int>();
        }

        public static List<Team> getAllTeams(DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.Teams select a).ToList<Team>();
        }

        public static List<Team> getAllActiveTeams(DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            return (from a in dc.Teams where a.deleteDate == null select a).ToList<Team>();
        }



        public static Boolean employeeBelongsToTeam(int empID, int teamID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.EmployeesTeamsHistories
                        where a.employeeID == empID && a.teamID == teamID && a.endDate == null
                        select a).Any();
        }

        public static List<Team> allTeamsOfEmployee(int empID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeesTeamsHistories
                        join b in dc.Teams on a.teamID equals b.teamID
                        where a.employeeID == empID
                        select b).ToList<Team>();
        }

        public static List<Team> teamsOfEmployee(int empID, DateTime when, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeesTeamsHistories
                        join b in dc.Teams on a.teamID equals b.teamID
                        where a.employeeID == empID && a.startDate <= when && (a.endDate == null || when < a.endDate)
                        select b).ToList<Team>();
        }

        public static int getEmployeesInTeamNum(int teamID, DateTime when)
        {
                return getAllEmployeesFromTeam(teamID, when).Count();
        }

        public static List<Employee> getAllWorkingEmployees(int teamID, DateTime when, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                List<Employee> emps = getAllEmployeesFromTeam(teamID, when);
                List<Employee> newEmps = new List<Employee>();

                int num = emps.Count();
                foreach (Employee e in emps)
                {
                    if (!LeaveHistories.isOnVacation(e.employeeID, when))
                        newEmps.Add(e);
                }

                return newEmps;
        }

        public static int getAllWorkingEmployeesNum(int teamID, DateTime when)
        {
            return getAllWorkingEmployees(teamID, when).Count();
        }

        public static List<EmployeesTeamsHistory> getHistoryOfTeam(int teamID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.EmployeesTeamsHistories where a.teamID == teamID select a).ToList();
        }
    }
}
