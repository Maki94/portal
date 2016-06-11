using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
        }
    }
}
