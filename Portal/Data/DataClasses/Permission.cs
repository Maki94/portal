using System.Collections.Generic;

namespace Data.DataClasses
{
    public class Permission
    {
        public Permission()
        {
            Roles = new List<Role>();
        }
        public int PermissionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual void AddRoles(params Role[] roles)
        {
            foreach (Role role in roles)
            {
                Roles.Add(role);
            }
        }
    }
}
