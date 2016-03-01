using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data2;
using Data2.DTOs;

namespace Data2
{
    public static class Holidays
    {
       
        public static void addHoliday(string name, DateTime startDate, DateTime endDate, Boolean same)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Holiday h = new Holiday
            {
                name = name,
                startDate = startDate,
                endDate = endDate,
                sameEveryYear = same
            };
            dc.Holidays.InsertOnSubmit(h);
            dc.SubmitChanges();
        }

        public static Holiday getHolidayAt(int hID, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return (from a in dc.Holidays where a.holidayID == hID select a).First();
        }

        public static void removeHoliday(int hID)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Holiday h = (from a in dc.Holidays where a.holidayID == hID select a).First();
            h.sameEveryYear = false;
            dc.SubmitChanges();
        }

        public static void editHoliday(int hID, string name = null, DateTime? startDate = null, DateTime? endDate = null,
            Boolean? same = null)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Holiday h = getHolidayAt(hID, dc);
            if (name != null)
                h.name = name;
            if (startDate != null)
                h.startDate = startDate.Value;
            if (endDate != null)
                h.endDate = endDate.Value;
            if (same != null)
                h.sameEveryYear = same.Value;

            dc.SubmitChanges();
        }

        public static List<int> getAllHolidaysIDs()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                List<int> hs = (from a in dc.Holidays select a.holidayID).ToList<int>();
                return hs;
        }
        public static List<Holiday> getAllHolidays(DataClasses1DataContext dc = null )
        {
                dc = dc ?? new DataClasses1DataContext();
                List<Holiday> hs = (from a in dc.Holidays select a).ToList<Holiday>();
                return hs;
        }

        public static List<int> getAllHolidayYears()
        {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                List<Holiday> list = getAllHolidays(dc);
                List<int> years = new List<int>();
                foreach (Holiday l in list)
                    years.Add(l.startDate.Year);
                years = years.Distinct().ToList();
                return years;
        }

        public static List<Holiday> getHolidaysWhichAreNotSameEveryYearAtYear(int year, DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                List<Holiday> hs = getAllHolidays(dc);
                hs = (from a in hs where a.startDate.Year == year && a.sameEveryYear == false select a).ToList<Holiday>();
                return hs;
        }

        public static List<Holiday> getHolidaysWhichAreNotSameEveryYear(DataClasses1DataContext dc = null)
        {
                dc = dc ?? new DataClasses1DataContext();
                return getHolidaysWhichAreNotSameEveryYearAtYear(DateTime.Today.Year - 1, dc);
        }

        public static void deleteHoliday(int holidayID, DataClasses1DataContext dc = null)
        {
            dc = dc ?? new DataClasses1DataContext();
            Holiday h = Holidays.getHolidayAt(holidayID, dc);
            dc.Holidays.DeleteOnSubmit(h);
            dc.SubmitChanges();
        }

    }
}
