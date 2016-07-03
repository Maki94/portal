using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.Company;
using Data.DataClasses;

namespace Data.Entities
{
    public class Comments
    {
        public static List<Comment> GetLast(int v)
        {
            using (var dc = new DataContext())
            {
                return (from c in dc.Comments where c.IsDeleted == false select c).ToList();
            }
        }
    }
}
