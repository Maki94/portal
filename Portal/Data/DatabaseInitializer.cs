using System;
using System.Collections.Generic;
using System.Data.Entity;
using Data.DataClasses;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var permissions = new List<Permission>
            {
                new Permission { Name = "ViewAboutPage" },
                new Permission { Name = "Logged" },
            };

            var roles = new List<Role>
            {
                new Role { Name = "Clan"},
                new Role { Name = "Blagajnik" },
                new Role { Name = "Sekretar" },
                new Role { Name = "HR" },
                new Role { Name = "FR" },
                new Role { Name = "Administrator" }
            };

            roles[5].AddPermission(permissions[0], permissions[1]);
            roles[0].AddPermission(permissions[0], permissions[1]);
            
            permissions[0].AddRoles(roles[5], roles[0]);
            permissions[1].AddRoles(roles[5], roles[0]);

            var members = new List<Member>
            {
                new Member { Name = "Milos", Surname = "Jajac", Nickname = "jajac", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 5, 26), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "zantsusan@gmail.com", Phone = "0641234123", Role = roles[5] },
                new Member { Name = "Mika", Surname = "Mikic", Nickname = "mika", Faculty = "Elfak", DateOfBirth = new DateTime(1999, 7, 13), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "mikamikic@gmail.com", Phone = "0691231231", Role = roles[0] },
                new Member { Name = "Marko", Surname = "Mihajlovic", Nickname = "Maki", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 7, 13), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "maki@gmail.com", Phone = "0651231231", Role = roles[0] }
            };

            var teams = new List<Team>
            {
                new Team { Name = "Team 1" },
                new Team { Name = "Team 2" },
            };

            var memberteams = new List<MemberTeam>
            {
                new MemberTeam { MemberId = 1, Member = members[0], TeamId = 1, Team = teams[0] },
                new MemberTeam { MemberId = 1, Member = members[0], TeamId = 2, Team = teams[1] },
                new MemberTeam { MemberId = 2, Member = members[1], TeamId = 2, Team = teams[1] },
            };

            var projects = new List<Project>
            {
                new Project { Name = "JobFair", Description = "Opis za JobFair", TeamId = 1, Team = teams[0] },
                new Project { Name = "Elektrijada", Description = "Opis za Elektrijadu", TeamId = 1, Team = teams[0] },
                new Project { Name = "Takmicenje", Description = "Opis za takmicenje", TeamId = 2, Team = teams[1] },
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject { MemberId = 1, ProjectId = 1, Member = members[0], Project = projects[0], Function = "posetilac" },
                new MemberProject { MemberId = 1, ProjectId = 2, Member = members[0], Project = projects[1], Function = "ucesnik" },
                new MemberProject { MemberId = 2, ProjectId = 1, Member = members[1], Project = projects[0], Function = "organizator" },
                new MemberProject { MemberId = 2, ProjectId = 2, Member = members[1], Project = projects[1], Function = "organizator" },
            };

            context.Roles.AddRange(roles);
            context.Permissions.AddRange(permissions);
            
            // snimam jednom ovako pre da bi se roles & permissions snimili gore navedenim redosledom
            // bez ovoga se redosled promeni, zato sto se administrator i clan koriste u pravljenju membera
            context.SaveChanges();

            context.MemberProjects.AddRange(memberprojects);
            //memberprojects.ForEach(x => context.MemberProjects.Add(x));

            context.SaveChanges();
        }
    }
}
