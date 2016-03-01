using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Data2;
using Data2.DTOs;
using ContentType = Org.BouncyCastle.Crypto.Tls.ContentType;
using System.Web.Script.Serialization;
using MVC.Extensions;

namespace MVC.Controllers
{
   
    public class EmployeesController : Controller
    {
        // GET: Employees
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewEmployeeList)]
        public ActionResult Index(int? teamFilter = null, Boolean? onHoliday = null, int?status = null,int? role =null)
        {
            try
            {
                Models.EmployeePageModel model = Models.EmployeePageModel.Load(null, teamFilter: teamFilter, onHoliday: onHoliday, roleFilter: role, status: status);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewEmployeeList)]
        [HttpPost]
        public ActionResult Index(Models.EmployeePageModel m)
        {
            try
            {

                Models.EmployeePageModel model = Models.EmployeePageModel.Load(m.Search,m.StatusFilter, m.TeamFilter, onHoliday: m.onHoliday, roleFilter: m.RoleFilter);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        //[HttpPost]
        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        //public ActionResult Edit(Data2.DTOs.EmployeeDTO emp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Employees.updateEmployee(emp.EmployeeID, emp.Role,  emp.FirstName, emp.LastName, emp.Password, emp.DateOfBirth);
        //    }
        //    return RedirectToAction("Index");
        //}

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        //public ActionResult Edit(int id)
        //{
        //    Data2.DTOs.EmployeeDTO model = Models.EmployeePageModel.CreateDTOForID(id);
        //    return View(model);
        //}

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        public ActionResult Details(int id, bool current = true)
        {
            try
            {

                if (CheckPermission.CheckUserID(id) || CheckPermission.CheckIsSessionMenager())
                {
                    Models.EmployeeProfileModel model = Models.EmployeeProfileModel.Load(id, current);
                    return View(model);
                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }  
        }

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        //[HttpPost]
        //public ActionResult Details(Models.EmployeeProfileModel model)
        //{
        //    if (ModelState.IsValid)
        //{
        //        Employees.updateEmployee(model.Employee.EmployeeID, model.Employee.Role, model.Employee.FirstName, model.Employee.LastName, model.Employee.Password, model.Employee.DateOfBirth);
        //    }

        //    if (model.Employee.Bonus != null)
        //    {
        //        //Data2.Employees.addBonusToEmployee(model.EmployeeID, 2, model.Bonus, model.BonusComment);
        //        Data2.Employees.addBonusToEmployee(model.Employee.EmployeeID, GetUser.EmployeeID(), (int)model.Employee.Bonus, null);
                
        //    }
        //    return RedirectToAction("Details", new { id = model.Employee.EmployeeID });

        //}

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult changeDetails(Models.EmployeeProfileModel model)
        {
            try
            {
                Employees.updateEmployee(model.Employee.EmployeeID, model.Employee.Role, model.Employee.FirstName,
                    model.Employee.LastName, model.Employee.Password, model.Employee.DateOfBirth);

                if (model.Contact != null)
                {
                    if (model.Contact.Phones != null)
                        foreach (PhoneDTO phone in model.Contact.Phones)
                        {
                            Employees.updateEmployeeTelephone(phone.PhoneID, phone.Phone);
                        }
                    if (model.Contact.Emails != null)
                        foreach (EmailDTO email in model.Contact.Emails)
                        {
                            Employees.updateEmployeeEmail(email.EmailID, email.Email);
                        }
                    if (model.Contact.Address != null)
                        foreach (AddressDTO address in model.Contact.Address)
                        {
                            Employees.updateEmployeeAddress(address.AddressID, address.Address);
                        }
                }
                

                return RedirectToAction("Details", new { id = model.Employee.EmployeeID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.EditEmp)]
        public ActionResult editKids(Models.EmployeeProfileModel model)
        {
            try
            {

                foreach (var kid in model.Kids.Kids)
                {
                    Employees.changeKid(kid.kidID, kid.FirstName, kid.DateOfBirth, kid.Gender);
                }
                return RedirectToAction("Details", new { id = model.Employee.EmployeeID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }
        
            

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult AddEmployee()
        {
            return View();
        }

        
        [HttpPost]
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult AddEmployee(Data2.DTOs.EmployeeDTO emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee e = Employees.addEmployee(emp.Role, GetUser.EmployeeID(), emp.FirstName, emp.LastName,
                        emp.Username, emp.Password, emp.DateOfBirth, emp.ContractStart, emp.ContractEnd);
                    return Json(new {success = true});
                    
                }
                return Json(new { success = false,  result = ModelState.Errors()});

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR","Home");
            }
           
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        public ActionResult ShowKids(int id)
        {
            try
            {

                if (CheckPermission.CheckUserID(id))
                {
                    Models.EmployeesKidsModel model = Models.EmployeesKidsModel.Load(id);
                    return View(model);
                }

                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }      
        }

        [AuthorizeUserAttribute(Permission = (int) Data2.Enums.Permissions.EditEmp)]
        public ActionResult AddKid(int id, string name, bool gender, DateTime dateOfBirth)
        {
            try
            {

                Employees.addKidToEmployee(id, name, gender, dateOfBirth);
                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        //[HttpPost]
        //public ActionResult AddKid(KidDTO k)
        //{
        //    Employees.addKidToEmployees((k.Parents).ToArray<int>(), k.FirstName, k.Gender, k.DateOfBirth);

        //    return RedirectToAction("ShowKids", new { id = k.Parents[0] });
        //}

        //[AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        //public ActionResult AddKid(int? id = null, int parentsNum = 1)
        //{
        //    if (id != null)
        //        ViewBag.parent = Employees.getEmplyeeAt((int)id).firstName + " " + Employees.getEmplyeeAt((int)id).lastName;
        //    else
        //        ViewBag.parent = "";

        //    ViewBag.parentsNum = parentsNum;
        //    ViewBag.id = id;
           
        //    return View();
        //}

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        [HttpPost]
        public ActionResult CalculateLeaveDays(Models.LeaveDaysCalculationModel m)
        {
            try
            {

                if (!(m.newBonusNum == null || m.newBonusNum == 0) && !CheckPermission.CheckUserID(m.EmployeeID))
                {
                    Employees.addBonusToEmployee(m.EmployeeID, GetUser.EmployeeID(), m.newBonusNum.Value, m.newBonusComment);
                }
                Models.LeaveDaysCalculationModel model = Models.LeaveDaysCalculationModel.Load(m.EmployeeID, m.Date);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewProfile)]
        public ActionResult CalculateLeaveDays(int id)
        {
            try
            {

                if (CheckPermission.CheckIsSessionMenager() || CheckPermission.CheckUserID(id))
                {
                    Models.LeaveDaysCalculationModel model = Models.LeaveDaysCalculationModel.Load(id);
                    return View(model);
                }

                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
         
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult History(int id)
        {
            try
            {

                Models.EmployeeStatusModel model = Models.EmployeeStatusModel.Load(id);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult ChangeHistory(int empID)
        {
            try
            {
                EmployeeStatusesHistory esh = Employees.getLastStatus(empID);
                Data2.DTOs.EmployeeStatusDTO dto = Models.EmployeeStatusModel.createStatusDTO(esh);
                return View(dto);
            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
            
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        [HttpPost]
        public ActionResult ChangeHistory(Data2.DTOs.EmployeeStatusDTO m, HttpPostedFileBase fileUploader)
        {
            try
            {
                m.File = null;
                if (fileUploader != null)
                {
                    using (var binaryReader = new BinaryReader(fileUploader.InputStream))
                    {
                        m.File = binaryReader.ReadBytes(fileUploader.ContentLength);
                    }
                }
                if (m.File != null)
                {
                    if (m.FileName == null)
                    {
                        m.FileName = fileUploader.FileName;
                    }
                    else
                    {
                        m.FileName += ".pdf";
                    }
                }
           
                Employees.updateEmployeeStatus(m.EmployeeID, GetUser.EmployeeID(), (Data2.Enums.employeeStatus)m.Status, m.File, m.FileName, m.StartDate, m.EndDate, m.ContractDuration ,m.comment);

                return RedirectToAction("History", new { id = m.EmployeeID });
            }
            catch (Exception)
            {
               return RedirectToAction("ERROR", "Home");
            }
        }


        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult ShowPDF(int status)
        {
            try
            {
                EmployeeStatusesHistory esh = Employees.getStatusAt(status);
                System.Data.Linq.Binary pdf = esh.filePDF;
                if (pdf != null)
                {
                    byte[] bytes = pdf.ToArray();
                    MemoryStream stream = new MemoryStream(bytes);
                    return new FileStreamResult(stream, "application/pdf");
                }

                return RedirectToAction("Index", "Employees");
            }
            catch (Exception)
            {
               return RedirectToAction("ERROR", "Home");
            }

        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        [HttpGet]
        public ActionResult AddPDF()
        {
            return View();
        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        [HttpPost]
        public ActionResult AddPDF(AddFileDTO m, HttpPostedFileBase fileUploader)
        {
            try
            {

                m.File = null;
                if (fileUploader != null)
                {
                    using (var binaryReader = new BinaryReader(fileUploader.InputStream))
                    {
                        m.File = binaryReader.ReadBytes(fileUploader.ContentLength);
                    }
                }
                if (m.File != null)
                {
                    if (m.FileName == null)
                    {
                        m.FileName = fileUploader.FileName;
                    }
                    else
                    {
                        m.FileName += ".pdf";
                    }
                }
                Employees.addPDF(m.statusID, m.FileName, m.File);
                return RedirectToAction("History", new { id = m.employeeID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewLeavesList)]
        public ActionResult Teams(int id)
        { 
            return RedirectToAction("Index", "Teams", new { empFilter = id });
        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult addTelephoneNUmber(int id, string number)
        {
            try
            {

                Employees.addPhoneNumberToEmployee(id, number);
                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult addEmail(int id, string email)
        {
            try
            {

                Employees.addEmailToEmployee(id, email);
                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult addAdress(int id, string adress)
        {
            try
            {

                Employees.addAdressToEmployee(id, adress);
                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }


        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult removeTelephoneNumber(int id,int empID)
        {
            try
            {

                Employees.removePhoneNumberFromEmployee(id);
                return RedirectToAction("Details", new { id = empID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult removeEmail(int id, int empID)
        {
            try
            {

                Employees.removeEmailFromEmployee(id);
                return RedirectToAction("Details", new { id = empID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }
        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.EditEmp)]
        public ActionResult removeAdress(int id, int empID)
        {
            try
            {

                Employees.removeAdressFromEmployee(id);
                return RedirectToAction("Details", new { id = empID });

            }
            catch (Exception)
            {
                return RedirectToAction("ERROR", "Home");
            }
        }



        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.ViewEmployeeList)]
        public ActionResult Sort(String sort, Models.EmployeePageModel model)
        {
            String old = (String)Session["EmpSort"];            
            if(old.Contains(sort+"Asc"))
            {
                old = old.Replace((sort + "Asc"),"");
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
            Session["EmpSort"] = old;
            if (model != null)
            {
                return RedirectToAction("Index",
                    new
                    {
                        teamFilter = model.TeamFilter,
                        onHoliday = model.onHoliday,
                        status = model.StatusFilter,
                        role = model.RoleFilter
                    });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [AuthorizeUserAttribute(Permission = (int)Data2.Enums.Permissions.Team_Add_Edit_Remove_View)]
        public ActionResult SortTeamsOfEmp(String sort, int empID, bool current)
        {
            String old = (String)Session["TeamsOfEmpSort"];
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
            Session["TeamsOfEmpSort"] = old;
            return RedirectToAction("Details", new { id = empID, current = current });
        }

    }


}