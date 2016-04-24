using Data.DataClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // odavde za string to int PK

    public class MemberLogin : IdentityUserLogin<int> { }
    public class MemberClaim : IdentityUserClaim<int> { }
    public class MemberRole : IdentityUserRole<int> { }

    public class Role : IdentityRole<int, MemberRole>, IRole<int>
    {
        public string Description { get; set; }
        public Role() { }
        public Role(string name) : this()
        {
            this.Name = name;
        }

        public Role(string name, string description) : this(name)
        {
            this.Description = description;
        }
    }

    public class MemberStore : UserStore<Member, Role, int, MemberLogin, MemberRole, MemberClaim>
    {
        public MemberStore(DataContext context) : base(context)
        {

        }
    }

    // user store

    public class ApplicationUserStore : UserStore<Member, Role, int,
        MemberLogin, MemberRole, MemberClaim>, IUserStore<Member, int>, IDisposable
    {
        public ApplicationUserStore() : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }


        public ApplicationUserStore(DbContext context)
            : base(context)
        {

        }
    }


    // role store

    public class ApplicationRoleStore : RoleStore<Role, int, MemberRole>,
        IQueryableRoleStore<Role, int>, IRoleStore<Role, int>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }


        public ApplicationRoleStore(DbContext context)
            : base(context)
        {

        }
    }
}
