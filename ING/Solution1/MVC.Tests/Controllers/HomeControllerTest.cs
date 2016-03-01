using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Data2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC;
using MVC.Controllers;
using Data2;

namespace MVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void MyTest1()
        {
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                List<StartEndStruct> list = (from h in dc.Holidays
                    select new StartEndStruct {start = h.startDate, end = h.endDate}).ToList();
                List<EmployeeStatusesHistory> l = new List<EmployeeStatusesHistory>();
                    list = list.OrderBy(a => a.start).ToList();
                StartEndStruct s = list.First();
                DateTime ab = s.start;


            }
            catch (Exception e)
            {

            }
        }


    }
}
