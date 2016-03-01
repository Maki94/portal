using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2
{
    public static class random
    {       
        static DataClasses1DataContext dc = new DataClasses1DataContext();
        public static string generateRandomString()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            int l = random.Next(4, 15);
            string result = new string(
                Enumerable.Repeat(chars, l)
                          .Select(s => s.ToLower()[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static DateTime generateRandomDate()
        {
            DateTime start = new DateTime(1930, 1, 1);
            Random gen = new Random();

            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public static int generateRandomRole()
        {
            List<int>  lista = Enums.getAllRoles();
            Random random = new Random();
            int r = random.Next(0, lista.Count);
            return lista[r];

        }

        public static Employee addRandomFirstEmployee()
        {
            
            Employee emp = Employees.addFirstEmployee(generateRandomString(), generateRandomString(), generateRandomString(), generateRandomString(),generateRandomDate(),generateRandomDate(),0);
            
            return emp;
        }
        public static Employee addRandomEmployee(int submitterID)
        {
            
            Employee emp = Employees.addEmployee(generateRandomRole(), submitterID, generateRandomString(), generateRandomString(), generateRandomString(), generateRandomString(),generateRandomDate(),generateRandomDate(),generateRandomDate());
            return emp;
        }


        public static void addRandomKidToEmployee(int empID)
        {
            Random r = new Random();
            Boolean gender = Convert.ToBoolean(r.Next(0, 2));
            Employees.addKidToEmployee(empID, generateRandomString(), gender, generateRandomDate());
        }

        public static Team addRanodmTeam()
        {
            
            Team t = Teams.createTeam(generateRandomString());
            dc.Teams.InsertOnSubmit(t);
            dc.SubmitChanges();
            return t;
        }

        public static Employee getRandomEmployee()
        {
            
            List<int> emps = Employees.getEmployeesIDs();
            Random r = new Random();

            Employee emp = (from a in dc.Employees
                            where a.employeeID == emps[r.Next(0, emps.Count)]
                            select a).First();
            return emp;
        }

        public static void fillTablesRandomly()
        {
            Random r = new Random();

            Employee first = addRandomFirstEmployee();
                   
                

            for (int i = 0; i < 15; i++)
            {
                int idd = getRandomEmployee().employeeID;
               
                    Employee pom = addRandomEmployee(idd);

                    int numKids = r.Next(1, 4);
                    for (int j = 0; j < numKids; j++)
                        addRandomKidToEmployee(pom.employeeID);
            }

            for (int i = 0; i < 5; i++)
            {
                Team t = addRanodmTeam();
                int numEmployees = r.Next(0, 10);
                
                while(Teams.getEmployeesInTeamNum(t.teamID, DateTime.Now) < numEmployees)
                {
                    //EmployeesTeams.addEmployeeToTeam(getRandomEmployee(), t);
                    Teams.addEmployeeToTeam(getRandomEmployee().employeeID, t.teamID);
                }
            }
        }

        public static void deleteAll()
        { 
            
            var sqlConnection = new SqlConnection(dc.Connection.ConnectionString);
            SqlCommand command = new SqlCommand("deleteAll", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;            
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void testIgranje()
        {
            //LeaveHistories.sendRequest(2, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.sendRequest(2, Enums.leaveTypes.leave, new DateTime(2016, 3, 1), new DateTime(2016, 3, 15));
            //LeaveHistories.sendRequest(2, Enums.leaveTypes.leave, new DateTime(2016, 7, 1), new DateTime(2016, 7, 25));


            //Console.WriteLine(LeaveDaysCalculation.LeaveDaysNumber(2, DateTime.Today.AddDays(365)));
            //LeaveHistories.approveRequest(1, 2);
            //Console.WriteLine(LeaveDaysCalculation.LeaveDaysNumber(2, DateTime.Today.AddDays(365)));
            //LeaveHistories.rejectRequest(1, 2);
            //Console.WriteLine(LeaveDaysCalculation.LeaveDaysNumber(2, DateTime.Today.AddDays(365)));
            //LeaveHistories.cancelRequest(1);
            //Console.WriteLine(LeaveDaysCalculation.LeaveDaysNumber(2, DateTime.Today.AddDays(365)));

            //LeaveHistories.sendRequest(1, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.sendRequest(3, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.sendRequest(4, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.sendRequest(5, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.sendRequest(7, Enums.leaveTypes.leave, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));
            //LeaveHistories.approveRequest(4, 2);
            //LeaveHistories.approveRequest(5, 2);
            //LeaveHistories.approveRequest(6, 2);
            //LeaveHistories.approveRequest(7, 2);

            //Console.WriteLine(Teams.getEmployeesInTeamNum(5, DateTime.Now));
            //Console.WriteLine(Teams.getAllWorkingEmployeesNum(5, new DateTime(2016, 1, 1)));
                
            LeaveHistories.sendRequest(8, Enums.leaveTypes.leave, Enums.paid.paid, new DateTime(2016, 1, 1), new DateTime(2016, 1, 10));

        }


    }
}
