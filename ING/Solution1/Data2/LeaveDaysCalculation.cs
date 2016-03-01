using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Data2.DTOs;
using Org.BouncyCastle.X509.Extension;

namespace Data2
{
    public  class StartEndStruct
    {
        public  DateTime start;
        public DateTime end;
    }

    public static class LeaveDaysCalculation
    {

        public static List<DateTime> kidsBirthDates;
        public static List<StartEndStruct> HolidaysList;
        public static List<StartEndStruct> activeDates;
        public static List<StartEndStruct> leaveDates;
        public static DateTime? slava;

        public static LeaveDaysCalculationDTO LeaveDaysNumber(int empID, DateTime when)
        {
            LeaveDaysCalculationDTO result = new LeaveDaysCalculationDTO();

            slava = getSlava(empID);

            DateTime firstStartDate = Employees.getFirstStartDate(empID);
            

            double leaveDaysNum = 0;
            double potencialDays = 20;
            int thisYearUsed = 0;
            int lastYearLeaveDaysNum = 0;
            int expiredDays = 0;
            int twoYearCounter = 0;
            int twoYearBonus = 0;
            int futureLeaves = 0;
            int kidNum = 0;

            kidsBirthDates = KidsBornWhileEmployment(empID, when, out kidNum);
           
            HolidaysList = getHolidays();
           
            activeDates = getActiveDates(empID, when);
            
            leaveDates = getLeaveDates(empID, when);
          
            potencialDays += kidNum;

            futureLeaves = getFutureLeaveDaysNum(empID, when);
            
            

            for (DateTime i = firstStartDate.Date; i <= when.Date; i = i.AddDays(1))
            {

                if (i.Day == 1 && i.Month == 1)
                {
                    int max = (int) potencialDays - 10;

                    if (leaveDaysNum > max)
                    {
                        leaveDaysNum = max;
                        expiredDays += (int) leaveDaysNum - max;
                    }

                    lastYearLeaveDaysNum = (int) leaveDaysNum;
                    thisYearUsed = 0;
                }


                if (i.Day == 1 && i.Month == 7)
                {
                    if (lastYearLeaveDaysNum > thisYearUsed)
                    {
                        expiredDays += lastYearLeaveDaysNum - thisYearUsed;
                        leaveDaysNum -= lastYearLeaveDaysNum - thisYearUsed;
                    }

                }

                int newKidsNum = kidsBornOnDate(i.Date);
                potencialDays += newKidsNum;
                kidNum += newKidsNum;



                //if (howManyTwoYear < (howManyTwoYear = TwoYear(empID, i.Date, howManyTwoYear)))

               
                if (IsActiveNow(i.Date))
                {
                   
                    twoYearCounter++;

                    if (twoYearCounter == 2*365)
                    {
                        twoYearCounter = 0;
                        twoYearBonus++;
                        potencialDays++;
                    }

                    leaveDaysNum += potencialDays/365;
                }

                if (isOnVacation(i.Date))
                {
                    if (NotWeekend(i.Date) && NotHoliday(i.Date))
                        if(slava==null || i.Date!=slava.Value.Date)
                        {
                            leaveDaysNum--;
                            thisYearUsed++;
                        }
                }

            }

            result.Potencial = (int) potencialDays;
            result.Kids = kidNum;
            result.TwoYearBonus = twoYearBonus;
            result.LastYearLeaveDays = lastYearLeaveDaysNum;
            result.FutureUsedLeaveDays = futureLeaves;
            result.Expired = expiredDays;
            result.Bonuses = getBonusCount(empID);
            

            result.Total = (int)leaveDaysNum + result.Bonuses;
            
            return result;
        }

        public static int getBonusCount(int empID)
        {
            return Data2.Employees.getAllEmployeeBonusDays(empID).Sum(a => a.bunusCount);
        }

        public static int kidsBornOnDate(DateTime date)
        {
            int i = 0;
            foreach (var d in kidsBirthDates)
            {
                if (d == date)
                    i++;
            }
            return i;
        }

        private static int getFutureLeaveDaysNum(int empID, DateTime when)
        {
            int number = 0;

            var leaves = (from l in leaveDates where l.start > when select l).ToList();
            
            foreach (var l in leaves)
            {
                number += OnlyLeaveDayNumber(l.start, l.end);
            }

            return number;
        }

        public static int OnlyLeaveDayNumber(DateTime start, DateTime end)
        {
            int number = 0;
            for (DateTime j = start; j <= end; j = j.AddDays(1))
                if (NotWeekend(j) && NotHoliday(j))
                    if(slava==null || j.Date!=slava.Value.Date)
                        number++;
            return number;
        }

        public static List<DateTime> KidsBornWhileEmployment(int empID, DateTime when, out int numOfKidsBeforeStart)
        {
            List<DateTime> dates = new List<DateTime>();
            int num = 0;

            List<Kid> kids = Employees.getEmployeesKids(empID);

            foreach (Kid kid in kids)
            {
                if (kid.dateOfBirth > when)
                    dates.Add(kid.dateOfBirth);
                else
                    num++;
            }

            numOfKidsBeforeStart = num;
            return dates;
        }

        //public static int NumOfKidsBornBeforeStart(int empID, DateTime start)
        //{
        //    int num = 0;

        //    List<Kid> kids = Employees.getEmployeesKids(empID);
        //    foreach (Kid kid in kids)
        //    {
        //        if (kid.dateOfBirth.Date < start)
        //        {
        //            num++;
        //        }
        //    }
        //    return num;
        //}

        public static List<StartEndStruct> getLeaveDates(int empID, DateTime when)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from a in dc.Leaves
                        join b in dc.LeaveStatusesHistories on a.leaveID equals b.leaveID
                        where a.employeeID == empID && b.endDate == null && b.leaveStatusEnumID == (int)Enums.leaveStatuses.approved && a.paid == Convert.ToBoolean((int)Enums.paid.paid)
                        select new StartEndStruct { start = a.startDate, end = a.endDate }).ToList();
        }

        public static bool isOnVacation(DateTime when)
        {
            foreach (var l in leaveDates)
            {
                if (l.start.Date <= when && when <= l.end.Date)
                    return true;
            }

            return false;
        }


        public static List<StartEndStruct> getActiveDates(int empID, DateTime when)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();

                return (from esh in dc.EmployeeStatusesHistories
                        where esh.employeeID == empID && esh.emplyeeStatusEnumID == (int)Enums.employeeStatus.Active
                        select new StartEndStruct { start = esh.contractStart, end = esh.contractEnd != null ? (DateTime)esh.contractEnd : when }).ToList();
        }

        public static bool IsActiveNow(DateTime date)
        {
            foreach (var a in activeDates)
            {
                if (a.start <= date && a.end >= date)
                    return true;
            }
            return false;
        }

        //public static bool IsActiveNow(int empID, DateTime when)
        //{
        //    DataClasses1DataContext dc = new DataClasses1DataContext();

        //    var WInfo = from esh in dc.EmployeeStatusesHistories
        //        where esh.employeeID == empID
        //              && esh.emplyeeStatusEnumID == (int) Enums.employeeStatus.Active
        //        select new {start = esh.startDate, end = esh.endDate != null ? (DateTime) esh.endDate : when};

        //    foreach (var w in WInfo)
        //    {
        //        if (w.start.Date <= when && when <= w.end.Date)
        //            return true;
        //    }
        //    return false;
        //}

        public static bool NotWeekend(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                return false;
            return true;
        }

        public static List<StartEndStruct> getHolidays()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from h in dc.Holidays
                        select new StartEndStruct { start = h.startDate, end = h.endDate }).ToList();

        }

        public static bool NotHoliday(DateTime day )
        {
            HolidaysList = HolidaysList ?? getHolidays();

            foreach (var h in HolidaysList)
            {
                for (DateTime i = h.start; i <= h.end; i = i.AddDays(1))
                {
                    if (i == day)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public static DateTime? getSlava(int empID)
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                return (from e in dc.Employees where e.employeeID == empID select e.slava).First();
        }

        public static int OnlyLeaveDayNumber(DateTime start, DateTime end, DateTime when,DateTime? Slava )
        {
             
            int number = 0;
            DateTime min = when < end ? when : end;
            for (DateTime j = start; j <= min; j = j.AddDays(1))
                if (NotWeekend(j) && NotHoliday(j))
                    if(Slava==null||j.Date==Slava.Value.Date)
                        number++;
            return number;
        }


    }
}
























//    /*Slobodni dani*/
        //    //public static int LeaveDaysNumber(int empID, DateTime when)
        //    //{

        //    //    int a = KidsBonus(empID, when);
        //    //    Console.WriteLine("Kids " + a);
        //    //    int b = ToThisYearBonus(empID, when);
        //    //    Console.WriteLine("ToThisYearBonus " + b);
        //    //    int c = ThisYearBonus(empID, when);
        //    //    Console.WriteLine("ThisYearBonus " + c);
        //    //    int d = TwoYearBonus(empID, when);
        //    //    Console.WriteLine("TwoYearBonus " + d);
        //    //    int e = Bonuses(empID, when);
        //    //    Console.WriteLine("Bonuses " + e);
        //    //    int f = UsedDays(empID, when);
        //    //    Console.WriteLine("UsedDays " + f);

        //    //    return a + b + c + d + e - f;
        //    //    // return KidsBonus(empID, when) + ToThisYearBonus(empID, when) + ThisYearBonus(empID, when) + TwoYearBonus(empID, when) + Bonuses(empID, when) - UsedDays(empID, when);
        //    //}

        //    //public static int LeaveDaysNumber(int empID, DateTime when, out int kids, out int toThisYear, out int thisYear, out int twoYear, out int bonuses, out int usedDays)
        //    //{
        //    //    DataClasses1DataContext dc = new DataClasses1DataContext();
        //    //    int a = KidsBonus(empID, when);
        //    //    kids = a;
        //    //    int b = ToThisYearBonus(empID, when);
        //    //    toThisYear = b;
        //    //    int c = ThisYearBonus(empID, when);
        //    //    thisYear = c;
        //    //    int d = TwoYearBonus(empID, when);
        //    //    twoYear = d;
        //    //    int e = Bonuses(empID, when);
        //    //    bonuses = e;
        //    //    int f = UsedDays(empID, when);
        //    //    usedDays = f;

        //    //    return a + b + c + d + e - f;
        //    //}


        //    /*svako dete 1 dan*/
        //    //slucajno dodato jedno dete
        //    public static int KidsBonus(int empID, DateTime when)
        //    {            
        //        int number = 0;

        //        List<int> kidYearBirth = KidYearBirth(empID);

        //        List<int> workYears = WorkYears(empID, when);

        //        foreach (int k in kidYearBirth)
        //            foreach (int y in workYears)
        //                if (y >= k)
        //                    number++;

        //        return number;
        //    }
        //    //mozda de se vrate sva deca pa onda da se uzmu godine jer nam kasnije trebaju samo deca
        //    public static List<int> KidYearBirth(int empID)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        List<int> kids = (from ek in dc.EmployeesKids
        //                          join k in dc.Kids on ek.kidID equals k.kidID
        //                          where ek.employeeID == empID
        //                          select k.dateOfBirth.Year).ToList<int>();

        //        return kids;
        //    }
        //    public static List<int> WorkYears(int empID, DateTime when)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        List<int> years = new List<int>();

        //        var WInfo = from esh in dc.EmployeeStatusesHistories
        //                    where esh.employeeID == empID
        //                    && esh.emplyeeStatusEnumID == (int)Enums.employeeStatus.Active
        //                    select new { start = esh.startDate, end = esh.endDate == null ? when : esh.endDate > when ? when : (DateTime)esh.endDate };

        //        foreach (var w in WInfo)
        //        {
        //            List<int> aux = GetYears(w.start, w.end);

        //            for (int i = 0; i < aux.Count; i++)
        //                years.Add(aux[i]);
        //        }

        //        return years.Distinct().ToList();
        //    }
        //    public static List<int> GetYears(DateTime start, DateTime end)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        List<int> years = new List<int>();

        //        if (start > end)
        //            return years;

        //        for (int i = start.Year; i <= end.Year; i++)
        //            years.Add(i);

        //        return years;
        //    }



        //    /*koliko je radio proporcionalno sa 20*/
        //    //problem kod oduzimanja
        //    public static int ToThisYearBonus(int empID, DateTime when)
        //    {
        //        double WDTTY = WorkingDaysToThisYear(empID, when);

        //        return (int)(WDTTY / 365 * 20);
        //    }
        //    public static double WorkingDaysToThisYear(int empID, DateTime when)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        double number = 0;

        //        var WInfo = from esh in dc.EmployeeStatusesHistories
        //                    where esh.employeeID == empID
        //                    && esh.emplyeeStatusEnumID == (int)Enums.employeeStatus.Active
        //                    select new { start = esh.startDate, end = esh.endDate != null ? (DateTime)esh.endDate : when };

        //        foreach (var w in WInfo)
        //        {
        //            if (w.end.Year >= when.Year)
        //                number += GetDays(w.start, new DateTime(when.Year, 1, 1));
        //            else
        //                number += GetDays(w.start, w.end);
        //        }
        //        return number;
        //    }
        //    public static double GetDays(DateTime start, DateTime end)
        //    {
        //        if (start > end)
        //            return 0;

        //        return (end.Subtract(start)).TotalDays;
        //    }



        //    /*20 ove godine*/
        //    public static int ThisYearBonus(int empID, DateTime when)
        //    {
        //        if (WorkingNow(empID, when))
        //            return 20;
        //        else
        //            return 0;
        //    }
        //    public static bool WorkingNow(int empID, DateTime when)
        //    {
        //        List<int> workYears = WorkYears(empID, when);
        //        return workYears.Contains(when.Year);

        //        //var Now = from esh in dc.EmployeesStatusesHistories
        //        //          where esh.employeeID == empID.employeeID
        //        //          && esh.endDate == null
        //        //          && esh.employeeStatusID == (int)Employees.employeeStatus.Active
        //        //          select new { start = esh.startDate };

        //        //return Now.Any();
        //    }



        //    /*na dve godine 1 dan*/
        //    public static int TwoYearBonus(int empID, DateTime when)
        //    {
        //        double W = WorkingDaysToThisYear(empID, when);
        //        return BonusTwo(W);
        //    }
        //    public static int BonusTwo(double days)
        //    {
        //        //[bdr/365]-[[bdr/365]-[bdr/365]/2]
        //        double M = (int)Math.Round(days / 365);
        //        return (int)(M - Math.Round(M - M / 2));
        //    }



        //    /*bonusi*/
        //    public static int Bonuses(int empID, DateTime when)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        var bonus = from b in dc.BonusDaysHistories
        //                    where b.employeeID == empID
        //                    select new { days = b.bunusCount, gettingDay = b.date };

        //        int number = 0;

        //        foreach (var b in bonus)
        //        {
        //            if (b.gettingDay <= when)
        //                number += b.days;
        //        }

        //        return number;
        //    }



        //    /*iskorisceni dani*/
        //    public static int UsedDays(int empID, DateTime when)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        int number = 0;

        //        var leaves = from l in dc.Leaves
        //                     join lsh in dc.LeaveStatusesHistories on l.leaveID equals lsh.leaveID
        //                     where l.employeeID == empID && lsh.endDate == null && lsh.leaveStatusEnumID == (int)Enums.leaveStatuses.approved && l.startDate <= when
        //                     select new { start = l.startDate, end = l.endDate };

        //        foreach (var l in leaves)
        //        {
        //            number += OnlyLeaveDayNumber(l.start, l.end, when);
        //        }

        //        return number;
        //    }
        //    public static int OnlyLeaveDayNumber(DateTime start, DateTime end, DateTime when)
        //    {
        //        int number = 0;
        //        DateTime min = when < end ? when : end;
        //        for (DateTime j = start; j <= min; j = j.AddDays(1))
        //            if (NotWeekend(j) && NotHoliday(j))
        //                number++;
        //        return number;
        //    }
        //    public static bool NotWeekend(DateTime day)
        //    {
        //        if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
        //            return false;
        //        return true;
        //    }
        //    public static bool NotHoliday(DateTime day)
        //    {
        //        DataClasses1DataContext dc = new DataClasses1DataContext();
        //        var holiday = from h in dc.Holidays
        //                      select new { start = h.startDate, end = h.endDate };

        //        foreach (var h in holiday)
        //        {
        //            for (DateTime i = h.start; i <= h.end; i= i.AddDays(1))
        //            {
        //                if (i == day)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //        return true;
        //    }


        //}



   
