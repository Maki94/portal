using Data;
using Data.DTOs;
using MVC.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class MpController : Controller
    {
        public ActionResult Index()
        {
            MPModel model = MPModel.Load();
            return View(model);
        }

        public ActionResult Izvestaj(int id)
        {
            return View();
        }

        public ActionResult DisplayPDF(int id)
        {
            ReportDTO r = MVC.Models.MPModel.createReportDTO(Data.Entities.MPs.GetRaportAt(id));
            
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(r.Text, 0, r.Text.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public void SavePdfFile(string padavanId, string text)
        {
            Data.Entities.MPs.SavePDF(MemberSession.GetMemberId(), int.Parse(padavanId), text);
        }
    }
}