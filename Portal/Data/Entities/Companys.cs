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
    }
}
