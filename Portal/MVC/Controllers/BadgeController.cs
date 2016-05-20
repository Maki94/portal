using Data.Entities;
using MVC.ViewModels.Badge;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class BadgeController : Controller
    {
        // GET: Badge
        public ActionResult Index()
        {
            return RedirectToAction("AllBadges");
        }

        public ActionResult AllBadges()
        {
            return View(new BadgeListViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateBadgeViewModel m)
        {
            if (ModelState.IsValid)
            {
                ////ovo je da se sacuva kao obican fajl tamo u Portal/MVC/Content
                //var filename = image.FileName;
                //var filePath = Server.MapPath("/Content/Uploads/Badges");
                //string savedFileName = Path.Combine(filePath, filename);
                //image.SaveAs(savedFileName);

                // ovo je da se sacuva u bazi
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    m.File.InputStream.Position = 0;
                    m.File.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();

                    Badges.CreateBadge(m.Name, m.Description, array);
                }
            }

            return RedirectToAction("Index");
        }
    }
}