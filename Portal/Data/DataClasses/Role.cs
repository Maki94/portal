using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Role
    {
        public Role()
        {
            Permissions = new List<Permission>();
        }
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual void AddPermission(params Permission[] permissions)
        {
            foreach (Permission permission in permissions)
            {
                Permissions.Add(permission);
            }
        }
    }
}
