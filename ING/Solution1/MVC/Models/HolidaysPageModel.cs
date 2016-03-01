using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2;
using Data2.DTOs;

namespace MVC.Models
{
    public class HolidaysPageModel
    {
        public List<HolidayDTO> List { get; set; }
        public int? YearFilter { get; set; }
        public Boolean? SameFilter { get; set; }

        public HolidayDTO newHoliday { get; set; }

        public static HolidayDTO createHolidayDTO(Holiday h)
        {
            HolidayDTO hd = new HolidayDTO
            {
                HolidayID = h.holidayID,
                Name = h.name,
                StartDate = h.startDate,
                EndDate = h.endDate,
                Same = h.sameEveryYear
            };
            return hd;
        }

        public static HolidaysPageModel Load(int? filter, Boolean? same = null, String sort = null)
        {
            HolidaysPageModel model = new HolidaysPageModel();
            model.newHoliday = null;
            model.List = new List<HolidayDTO>();
            model.YearFilter = filter;
            model.SameFilter = same;
            List<Holiday> h = Holidays.getAllHolidays();
            if(model.YearFilter != null)
                h = (from a in h where a.startDate.Year == model.YearFilter select a).ToList();

            if (model.SameFilter == false)
                h = (from a in h where a.sameEveryYear == false select a).ToList();
            if (model.SameFilter == true)
                h = (from a in h where a.sameEveryYear == true select a).ToList();

            h = h.OrderBy(m => m.startDate).ToList();

            foreach (var a in h)
                model.List.Add(createHolidayDTO(a));

            if (sort != null)
            {
                string[] sortList = sort.Split(' ');
                sortList.Reverse();
                foreach (string s in sortList)
                {
                    switch (s)
                    {

                        case "NameAsc":
                            model.List = model.List.OrderBy(m => m.Name).ToList();
                            break;
                        case "NameDesc":
                            model.List = model.List.OrderByDescending(m => m.Name).ToList();
                            break;

                        case "StartDateAsc":
                            model.List = model.List.OrderBy(m => m.StartDate).ToList();
                            break;
                        case "StartDateDesc":
                            model.List = model.List.OrderByDescending(m => m.StartDate).ToList();
                            break;

                        case "EndDateAsc":
                            model.List = model.List.OrderBy(m => m.EndDate).ToList();
                            break;
                        case "EndDateDesc":
                            model.List = model.List.OrderByDescending(m => m.EndDate).ToList();
                            break;

                        case "SameAsc":
                            model.List = model.List.OrderBy(m => m.Same).ToList();
                            break;
                        case "SameDesc":
                            model.List = model.List.OrderByDescending(m => m.Same).ToList();
                            break;
                    }
                }
            }

            return model;
        }
    }
}