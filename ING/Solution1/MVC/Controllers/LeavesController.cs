using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Data2.DTOs;
using Data2;
using iTextSharp.text;
using MVC.Models;
using RazorPDF;
using Rotativa.Options;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using RazorPDF.Legacy.Text.Pdf;
using Rotativa;

namespace MVC.Controllers
{
    public class LeavesController : Controller
    {
        //GET: Leaves

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult Index(int? status = null, int? team = null, int? type = null, DateTime? startDate = null,
            DateTime? endDate = null, int? empFilter = null, string sort = null, DateTime? onDateFilter = null)
        {
            try
            {

                LoginDTO user = GetUser.LoginDTO();
                if (user != null && user.Role == (int)Enums.Roles.Employee && user.EmployeeID != empFilter)
                {
                    return RedirectToAction("Index", new
                    {
                        status = status,
                        team = team,
                        type = type,
                        startDate = startDate,
                        endDate = endDate,
                        empFilter = user.EmployeeID,
                        sort = sort,
                        onDateFilter = onDateFilter
                    });
                }

                Models.LeavePageModel model = Models.LeavePageModel.Load(null, status: status, team: team, type: type,
                    startDate: startDate, endDate: endDate, empFilter: empFilter, sort: sort,
                    onDateFilter: onDateFilter);
                //za meni
                ViewBag.empFilter = empFilter != null;
                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.ViewLeavesList)]
        [HttpPost]
        public ActionResult Index(Models.LeavePageModel m)
        {
            try
            {

                Models.LeavePageModel model = Models.LeavePageModel.Load(m.Search, m.StatusFilter, m.TeamFilter, m.TypeFilter,
                    m.StartDateFilter, m.EndDateFilter, empFilter: m.EmpFilter, onDateFilter: m.OnDateFilter);
                ViewBag.empFilter = m.EmpFilter != null;
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.EditEmp)]
        public ActionResult History(int id)
        {
            try
            {

                LeaveDTO pom = Models.LeavePageModel.createDTOForID(id);
                Models.LeaveHistoryPageModel m = Models.LeaveHistoryPageModel.Load(pom);
                return View(m);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }

        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult SendRequest()
        {
            return View();
        }

        

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewLeavesList)]
        [HttpPost]
        public async Task<ActionResult> SendRequest(Models.SendRequestModel model)
        {
            try
            {

                Data2.LeaveHistories.sendRequest(GetUser.EmployeeID(), (Data2.Enums.leaveTypes)model.Type, (Data2.Enums.paid)model.Paid, model.StartDate, model.EndDate, model.Comment);

                model.canOpen = CheckPermission.CheckIsLogIn();
                var json = new JavaScriptSerializer().Serialize(model);


                var PDF = new Rotativa.ActionAsPdf("SendRequestPdfForm", new { json = json })
                {
                    PageSize = Size.A4
                };
                byte[] bytePDF = PDF.BuildPdf(this.ControllerContext);
                await SendRequestEmail(model, bytePDF);


                return RedirectToAction("Index", "Leaves", new { empFilter = GetUser.EmployeeID() });
                //if (model.Error == "")
                //    return RedirectToAction("Index", "Leaves", new { empFilter = GetUser.EmployeeID() });
                //else
                //    return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult SendRequestPdfForm(string json = null)
        {
            try
            {
                SendRequestModel model = new JavaScriptSerializer().Deserialize<SendRequestModel>(json);
                model.StartDate = ConvertTo.MyTimeZone(model.StartDate);
                model.EndDate = ConvertTo.MyTimeZone(model.EndDate);
                if (model.canOpen)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }

        }
        
        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.ViewLeavesList)]
        public async Task SendRequestEmail(SendRequestModel sender, byte[] pdf = null)
        {
            Employee user = GetUser.Employee();
            MemoryStream stream = new MemoryStream(pdf);


            var body = "<p>Request From: {0} ({1})</p><p>Comment: {2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("masadordevic@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("mitic.nikolca94@gmail.com");  // replace with valid value
            message.Subject = "Leave Request";
            message.Body = string.Format(body, user.firstName+" "+user.lastName,user.username,sender.Comment);
            message.IsBodyHtml = true;
            message.Attachments.Add(new Attachment(stream, "Request.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf));


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "masadordevic@gmail.com",  // replace with valid value
                    Password = "ingpassword"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
     

        public ActionResult CreatePDF(int id)
        {
            try
            {

                LeaveDTO pom = Models.LeavePageModel.createDTOForID(id);
                Models.LeaveHistoryPageModel m = Models.LeaveHistoryPageModel.Load(pom);
                return new Rotativa.ViewAsPdf("History", m);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        public ActionResult ShowPDF(Models.SendRequestModel model)
        {
            return View("Request");
        }


        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.Respond)]
        public ActionResult Approve(int leaveID)
        {
            try
            {

                LeaveHistories.approveRequest(leaveID, GetUser.EmployeeID());
                LeaveDTO pom = Models.LeavePageModel.createDTOForID(leaveID);
                Models.LeaveHistoryPageModel m = Models.LeaveHistoryPageModel.Load(pom);

                return new Rotativa.ViewAsPdf("History", m)
                {
                    FileName = "Request From" + pom.FirstName + " " + pom.LastName + "-" + pom.Date.ToShortDateString() + ".pdf"
                };

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.Respond)]
        public ActionResult Reject(int leaveID)
        {
            try
            {

                LeaveHistories.rejectRequest(leaveID, GetUser.EmployeeID());
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.Respond)]
        public ActionResult Respond(int leaveID)
        {
            try
            {

                return RedirectToAction("History", new { id = leaveID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Enums.Permissions.ViewLeavesList)]
        public ActionResult Cancel(int leaveID)
        {
            try
            {
                LeaveHistories.cancelRequest(leaveID);
                return RedirectToAction("Index", new { empID = GetUser.EmployeeID() });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Enums.Permissions.ViewLeavesList)]
        public ActionResult Edit(int leaveID)
        {
            try
            {

                Models.SendRequestModel model = Models.SendRequestModel.Load(leaveID);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Enums.Permissions.ViewLeavesList)]
        [HttpPost]
        public ActionResult Edit(Models.SendRequestModel model)
        {
            try
            {
                LeaveHistories.EditLeave(model.leaveID, model.StartDate, model.EndDate, model.Comment, model.Type);
                return RedirectToAction("Index", "Leaves", new { empFilter = GetUser.EmployeeID() });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Leaves", new { empFilter = GetUser.EmployeeID() });
            }

        }


        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult Sort(String sort, Models.LeavePageModel model)
        {
            String old = (String) Session["LeaveSort"];
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
            Session["LeaveSort"] = old;
            if (model != null)
            {//not elegant, but working
                return RedirectToAction("Index", new
                {
                    status = model.StatusFilter,
                    team = model.TeamFilter,
                    type = model.TypeFilter,
                    startDate = model.StartDateFilter,
                    endDate = model.EndDateFilter,
                    empFilter = model.EmpFilter,
                    sort = old,
                    onDateFilter = model.OnDateFilter
                });
            }
            else
            {
                return RedirectToAction("Index", new {sort = old});
            }

        }

        [HttpPost]
        public ActionResult checkRequest(string type, DateTime startDate, DateTime endDate, string comment)
        {
            try
            {
                int t = Int32.Parse(type);
                RequestDTO result = Data2.LeaveHistories.checkRequest(GetUser.EmployeeID(),
                    (Data2.Enums.leaveTypes)t, startDate, endDate, comment);

                return Json(new { errorOccurred = result.ErrorOccurred, error = result.Error, leaveDuration = result.LeaveDuration }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { errorOcurred = true, error = "Something went wrong" }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}