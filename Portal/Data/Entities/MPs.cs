using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public static List<MemberMaster> GetReports(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.MemberMaster.Include(x => x.Member).Include(x => x.Reports).Where(x=>!x.IsDeleted).ToList();
            }
        }
    }
}
