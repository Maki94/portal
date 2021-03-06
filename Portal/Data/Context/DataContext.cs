﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(): base("DefaultConnection")
        {
        }

        //public DbSet<Anecdote> Anecdotes { get; set; }
        //public DbSet<AnecdoteMember> AnecdoteMembers { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Excuse> Excuses { get; set; }
        //public DbSet<ExecutiveBoard> ExecutiveBoards { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberBadge> MemberBadges { get; set; }
        public DbSet<MemberComment> MemberComments { get; set; }
        public DbSet<MemberCompany> MemberCompanies { get; set; }
        public DbSet<MemberMeeting> MemberMeetings { get; set; }
        public DbSet<MemberPollOption> MemberPollOptions { get; set; }
        public DbSet<MemberProject> MemberProjects { get; set; }
        public DbSet<MemberTeam> MemberTeams { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageNotification> MessageNotifications { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<DefaultPicture> DefaultPictures { get; set; }
        public DbSet<MemberMaster> MemberMaster { get; set; }
        public DbSet<Report> Reports { get; set; }

        private void SoftDelete(DbEntityEntry entry)
        {
            Type entryEntityType = entry.Entity.GetType();

            string tableName = GetTableName(entryEntityType);
            string primaryKeyName = GetPrimaryKeyName(entryEntityType);

            string sql =
                string.Format(
                    "UPDATE {0} SET IsDeleted = 1 WHERE {1} = @id",
                        tableName, primaryKeyName);

            Database.ExecuteSqlCommand(
                sql,
                new SqlParameter("@id", entry.OriginalValues[primaryKeyName]));

            // prevent hard delete            
            entry.State = EntityState.Detached;
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                                  .Where(p => p.State == EntityState.Deleted))
                SoftDelete(entry);

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Badge>()
            .Map(m => m.Requires("IsDeleted").HasValue(false))
            .Ignore(m => m.IsDeleted);

            //// uklanjanje on delete cascade za member1 i member2
            //// u Conversation kako bi se uklonio multiple cascade paths error
            //modelBuilder.Entity<Conversation>()
            //    .HasRequired<Member>(m => m.Member1)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Conversation>()
            //    .HasRequired<Member>(m => m.Member2)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //// uklanjanje on delete cascade za meetingCreator i HR
            //// u Meeting kako bi se uklonio multiple cascade paths error
            //modelBuilder.Entity<Meeting>()
            //    .HasRequired<Member>(m => m.MeetingCreator)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Meeting>()
            //    .HasRequired<Member>(m => m.HR)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //// uklanjanje on delete cascade za member i comment
            //// u MemberComment kako bi se uklonio multiple cascade paths error
            //modelBuilder.Entity<MemberComment>()
            //    .HasRequired<Member>(m => m.Member)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<MemberComment>()
            //    .HasRequired<Comment>(c => c.Comment)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
