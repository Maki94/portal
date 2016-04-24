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
        public DataContext(): base("DefaultConnection")
        {
        }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ExecutiveBoard> ExecutiveBoards { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberProject> MemberProjects { get; set; }
        public DbSet<MemberTeam> MemberTeams { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
