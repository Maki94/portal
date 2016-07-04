using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MPs
    {
        public static List<Member> GetPadawansFrom(int idm, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.Members.Where(x => x.MasterId == idm).ToList();
            }
        }
    }
}
