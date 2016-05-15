using Data.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Roles
    {
        public static List<Role> GetAllRoles()
        {
            using (var dc = new DataContext())
            {
                return (from r in dc.Roles select r).ToList();
            }
        }
    }
}
