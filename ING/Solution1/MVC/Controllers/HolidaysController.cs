using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data2;
using Data2.DTOs;
using MVC.Extensions;

namespace MVC.Controllers
{
    public class HolidaysController : Controller
    {
        // GET: Holidays
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewHolidayList)]
        public ActionResult Index(int? yearFilter = null, Boolean? sameFilter = null, String sort =null)
        {
            try
            {

                Models.HolidaysPageModel model = Models.HolidaysPageModel.Load(yearFilter, sameFilter, sort: sort);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewHolidayList)]
        [HttpPost]
        public ActionResult Index(Models.HolidaysPageModel model)
        {
            try
            {

                Models.HolidaysPageModel m = Models.HolidaysPageModel.Load(model.YearFilter, model.SameFilter);
                return View(m);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Holiday_Add_Remove)]
        public ActionResult Create()
        {
            return View();
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Holiday_Add_Remove)]
        [HttpPost]
        public ActionResult Create(HolidayDTO model)
        {
            try
                {
                
                        Data2.Holidays.addHoliday(model.Name, model.StartDate, model.EndDate, model.Same);
                        return RedirectToAction("Index");
                    

                }
                catch (Exception)
                {
                    return RedirectToAction("ERROR", "Home");
                }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Holiday_Add_Remove)]
        public ActionResult Remove(int id)
        {
            try
            {

                Holiday h = Holidays.getHolidayAt(id);
                if (h.startDate.Year == DateTime.Today.Year && h.startDate > DateTime.Today)
                {
                    Holidays.deleteHoliday(id);
                }
                else
                {
                    Data2.Holidays.removeHoliday(id);
                }

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }
        
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Holiday_Add_Remove)]
        [HttpPost]
        public ActionResult Edit(HolidayDTO model)
        {
            try
            {

                Holidays.editHoliday(model.HolidayID, model.Name, model.StartDate, model.EndDate, model.Same);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }



        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewHolidayList)]
        public ActionResult Sort(String sort, Models.HolidaysPageModel model)
        {
            String old = (String)Session["HolidaySort"];
            if (old.Contains(sort + "Asc"))
            {
                old = old.Replace((sort + "Asc"), "");
                old += sort;
                old += "Desc ";
            }
            else if (old.Contains(sort + "Desc"))
            {
                old = old.Replace((sort + "Desc"), "");
                old += sort;
                old += "Asc ";

            }
            else
            {
                old += sort;
                old += "Asc ";
            }
            Session["HolidaySort"] = old;
            if(model!=null)
                return RedirectToAction("Index", new { sort = old, yearFilter=model.YearFilter, sameFilter=model.SameFilter});
            else
                return RedirectToAction("Index", new {sort = old});
        }

    }
}