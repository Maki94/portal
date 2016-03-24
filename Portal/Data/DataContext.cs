using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<MemberProject> MemberProjects { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MemberTeam> MemberTeams { get; set; }
    }
}
