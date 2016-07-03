using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.Company;
using Data.DataClasses;

namespace Data.Entities
{
    public class Companys
    {
        public static List<Company> GetAll()
        {
            using (var dc = new DataContext())
            {
                return (from c in dc.Companies where c.IsDeleted == false select c).ToList();
            }
        }

        public static List<Company> GetDelegateTo(int id)
        {
            using (var dc = new DataContext())
            {
                return (from m in dc.MemberCompanies where m.Member.MemberId == id && m.IsDeleted == false select m.Company).ToList();
            }
        }
    }
}
