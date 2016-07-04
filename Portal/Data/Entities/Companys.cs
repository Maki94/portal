using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.Company;
using Data.DataClasses;
using System.Data.Entity;

namespace Data.Entities
{
    public class Companys
    {
        public static List<Company> GetAll()
        {
            using (var dc = new DataContext())
            {
                return dc.Companies.Include(x => x.Contacts).Include(x=>x.Comments).Include(x => x.Comments.Select(z=>z.Author)).Include(x => x.Comments.Select(z => z.Project)).Include(x => x.Comments.Select(z=>z.Likes)).Include(x => x.Comments.Select(z=>z.Likes.Select(y => y.Member))).ToList();
            }
        }

        public static List<Company> GetDelegateTo(int id)
        {
            using (var dc = new DataContext())
            {
                List<MemberCompany> mc = dc.MemberCompanies.Where(x => x.MemberId == id && x.IsDeleted==false && x.FinishDate ==null).Include(x => x.Company).Include(x=>x.Company.Contacts).Include(x => x.Company.Comments).Include(x => x.Company.Comments.Select(z => z.Author)).Include(x => x.Company.Comments.Select(z => z.Likes)).Include(x => x.Company.Comments.Select(z => z.Likes.Select(y => y.Member))).Include(x => x.Company.Comments.Select(z => z.Project)).ToList();
                return mc.Select(x => x.Company).ToList();
            }
        }

        public static void AddCompany(string name, string address, string city, string description, string email, string phone, string site,
                                   Enumerations.CompanyField field, Enumerations.CompanyType type, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {                
                Company comp = new Company
                {
                    Address = address,
                    City = city,
                    Description = description,
                    Email = email,
                    Phone = phone,
                    Field = field,
                    Type = type,
                    Name = name,
                    Website = site
                };

                dc.Companies.Add(comp);

                dc.SaveChanges();
            }
        }

        
        public static void DeleteCompany(int idcomp, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Company c = dc.Companies.Where(x => x.CompanyId == idcomp).Include(x => x.Comments).Include(x=>x.Comments.Select(z=>z.Likes)).Include(x => x.Contacts).First();
                c.Contacts = c.Contacts ?? new List<ContactPerson>();
                c.Comments = c.Comments ?? new List<Comment>();

                c.IsDeleted = true;
                foreach(Comment comm in c.Comments)
                {
                    comm.IsDeleted = true;
                    comm.Likes = comm.Likes ?? new List<MemberComment>();
                    foreach(MemberComment l in comm.Likes)
                    {
                        l.IsDeleted = true;
                    }
                }
                foreach(ContactPerson cp in c.Contacts)
                {
                    cp.IsDeleted = true;
                }
                List<MemberCompany> mcs = dc.MemberCompanies.Where(x => x.Company == c).ToList();
                foreach(MemberCompany mc in mcs)
                {
                    mc.IsDeleted = true;
                }
                dc.SaveChanges();
            }
        }

        public static void DelegateTo(int idm, List<int> idc, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Member m = dc.Members.Where(x => x.MemberId == idm).First();
                foreach (int id in idc)
                {
                    Company c = dc.Companies.Where(x => x.CompanyId == id).First();

                    MemberCompany oldmc = dc.MemberCompanies.Where(x => x.Company == c && x.FinishDate == null).FirstOrDefault();

                    if (oldmc!=null)
                    {
                        oldmc.FinishDate = DateTime.Now;
                    }

                    MemberCompany newmc = new MemberCompany
                    {
                        Company = c,
                        Member = m,
                        StartDate = DateTime.Now
                    };

                    dc.MemberCompanies.Add(newmc);
                }

                dc.SaveChanges();
            }
        }

        public static void AddContactPerson(int idc, string email, string name, string phone, string note, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                Company c = dc.Companies.Where(x => x.CompanyId == idc).First();

                ContactPerson cp = new ContactPerson
                {
                    Company = c,
                    Email = email,
                    Name = name,
                    Note = note,
                    Phone = phone,
                    StartDate = DateTime.Now
                };

                dc.ContactPersons.Add(cp);
                dc.SaveChanges();
            }
        }

        public static void DeleteContactPerson(int idcp, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                ContactPerson cp = dc.ContactPersons.Where(x => x.ContactPersonId == idcp).First();
                cp.IsDeleted = true;
                dc.SaveChanges();
            }
        }
    }
}
