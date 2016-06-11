using System.Collections.Generic;

namespace Data.DataClasses
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public Permission()
        {
            Roles = new List<Role>();
        }
    }
}
