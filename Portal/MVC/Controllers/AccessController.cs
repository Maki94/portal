using Data.DTOs;
using Data.Entities;
using MVC.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [AuthorizeMember]
    public class AccessController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(bool logOut = false, string message = null)
        {
            try
            {
                if (logOut)
                    Session["Member"] = null;

                if (Session["Member"] != null)
                    return RedirectToAction("Index", "Member");

                ViewBag.message = message;

                // ovo return Index ubaceno samo da se odma uloguje na jedan nalog
                //return Index(new MemberLoginModel
                //{
                //    Gmail = "zantsusan@gmail.com",
                //    Password = "Admin@123",
                //    RememberMe = true
                //});
                // ovo return Index ubaceno samo da se odma uloguje na jedan nalog
                return View(new MemberLoginModel());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return RedirectToAction("Index", "Access");
            }
        }

        public async Task<ActionResult> SendFeedback(string type, string text)
        {
            try
            {
                await SendRequestEmail(text, (Data.Enumerations.FeedbackType) int.Parse(type));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return RedirectToAction("Index");
        }

        public async Task SendRequestEmail(string text, Data.Enumerations.FeedbackType type, byte[] pdf = null)
        {
            MemberDTO user = MVC.Models.CompanyModel.createMemberDTO(Data.Entities.Members.GetMemberAt(MemberSession.GetMemberId()));
            //MemoryStream stream = new MemoryStream(pdf);


            var body = "<p>Feedback from: {0} ({1})</p><p>Type: {2}</p><p>Text: {3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("mnjs2016@googlegroups.com"));
            message.From = new MailAddress("mn.jsSWE@gmail.com");
            message.Subject = "Feedback";
            message.Body = string.Format(body, user.Name + " " + user.Surname, user.Gmail.ToString(), type.ToString(), text);
            message.IsBodyHtml = true;
            //message.Attachments.Add(new Attachment(stream, "Request.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf));


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "mn.jsSWE@gmail.com",
                    Password = "projekat"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(MemberLoginModel model)
        {
            try
            {
                Data.DTOs.LoginDTO member = Login.CreateLoginDTO(model.Gmail, model.Password, model.RememberMe);

                if (member.LoginStatus == (int)Data.Enumerations.LoginStatus.Successful)
                {
                    Session["Member"] = member;

                    Session.Timeout = member.RememberMe ? 525600 : 20;

                    return RedirectToAction("Index", "Member");
                }
                else if (member.LoginStatus == (int)Data.Enumerations.LoginStatus.IncorrectPassword)
                {
                    return RedirectToAction("Index", new { message = "Pogresna lozinka." });
                }
                else
                {
                    return RedirectToAction("Index", new { message = "Ne postoji taj nalog." });
                }
            }
            catch (Exception exception)
            {
                return RedirectToAction("Index", new { message = "Nesto ne valja" + exception });
            }
        }

        //[AuthorizeMember(Permission = (int)Data.Enumerations.Permission.UploadDefaultPicture)]
        public ActionResult UploadDefaultPicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadDefaultPicture(string name, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    byte[] array;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        array = ms.GetBuffer();
                    }
                    DefaultPictures.UploadPicture(array, name);
                }

                return RedirectToAction("UploadDefaultPicture");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Access");
            }
        }

        public ActionResult ChangePassword(string message)
        {
            try
            {
                ViewBag.message = message;
                return View(new MemberChangePasswordModel());
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Access");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(MemberChangePasswordModel m)
        {
            try
            {
                if (!Members.CheckMemberPassword(MemberSession.GetMemberId(), m.OldPassword))
                {
                    return RedirectToAction("ChangePassword", new { message = "Pogresna stara lozinka." });
                }
                else if (m.NewPassword != m.RepeatPassword)
                {
                    return RedirectToAction("ChangePassword", new { message = "Polja za novu lozinku nisu jednaka." });
                }
                else
                {
                    Members.ChangeMemberPassword(MemberSession.GetMemberId(), m.NewPassword);
                }

                return RedirectToAction("ChangePassword", new { message = "Nesto ne valja." });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Access");
            }
        }
    }
}