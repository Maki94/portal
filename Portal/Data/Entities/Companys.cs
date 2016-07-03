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
                return dc.Companies.Include(x => x.Contacts).ToList();
            }
        }

        public static List<Company> GetDelegateTo(int id)
        {
            using (var dc = new DataContext())
            {
                return dc.MemberCompanies.Where(x => x.MemberId == id && x.IsDeleted==false && x.FinishDate ==null).Include(x => x.Company).Include(x=>x.Company.Contacts).Select(x => x.Company).ToList();
            }
        }
    }
}
