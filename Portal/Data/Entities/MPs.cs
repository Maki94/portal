using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.DTOs;

namespace Data.Entities
{
    public class MPs
    {
        public static List<Member> GetPadawansFrom(int idm, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.MemberMaster.Include(x=>x.Member).Where(x => x.MasterId == idm && !x.IsDeleted).Select(x=>x.Member).ToList();
            }
        }

        public static Report GetRaportAt(int id, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.Reports.Where(x => x.ReportId == id).First();
            }
        }

        public static List<MemberMaster> GetReports(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.MemberMaster.Include(x => x.Member).Include(x => x.Reports).Where(x=>!x.IsDeleted).ToList();
            }
        }

        public static void SavePDF(int idm, int idp, string text, DataContext dc =null)
        {
            
            using (dc = dc ?? new DataContext())
            {
                MemberMaster mm = dc.MemberMaster.Where(x => x.MasterId == idm && x.MemberId == idp).First();
                Member master = Members.GetMemberAt(idm);
                Member padawan = Members.GetMemberAt(idp);

                byte[] bytes;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate()))
                    {
                        using (iTextSharp.text.pdf.PdfWriter w = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();
                            doc.NewPage();
                            doc.Add(new iTextSharp.text.Paragraph(text));
                            doc.AddTitle(master.Name + " " + master.Surname + " report about " + padawan.Name + " " + padawan.Surname + " " + DateTime.Now.ToShortDateString());
                            doc.Close();
                            bytes = ms.ToArray();
                        }
                    }
                }

                Report r = new Report
                {
                    Text = bytes,
                    Time = DateTime.Now,
                    MemberMasterId = mm.MemberMasterId
                };

                dc.Reports.Add(r);
                dc.SaveChanges();
            }

        }

        public static void AddMasterTo(int masterId, int padawanId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Member padawan = Data.Entities.Members.GetMemberAt(padawanId);

                MemberMaster mm = new MemberMaster
                {
                    StartDate = DateTime.Now,
                    MasterId = masterId,
                    Member = padawan
                };

                dc.MemberMaster.Add(mm);
                dc.SaveChanges();
            }
        }
    }
}
