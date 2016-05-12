﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using Data.DataClasses;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // ovo stampa sql query u Output prozor
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            var permissions = new List<Permission>
            {
                new Permission { Name = "ViewAboutPage" },
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

            // odmah snimam role i permisije da bi se snimile ovim redom jer ako
            // se prvo "povezu" addovanjem i snime onda se snimi prvo administrator
            // i njegova permisija pa tek onda sve ostale role
            context.Roles.AddRange(roles);
            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            // dodajemo administratoru permisiju da vidi About page
            roles[5].Permissions.Add(permissions[0]);

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
                new MemberTeam { Member = members[0], Team = teams[0] },
                new MemberTeam { Member = members[0], Team = teams[1] },
                new MemberTeam { Member = members[1], Team = teams[1] },
                new MemberTeam { Member = members[2], Team = teams[1] }
            };

            var projects = new List<Project>
            {
                new Project { Name = "JobFair", Description = "Opis za JobFair", TeamId = 1, Team = teams[0] },
                new Project { Name = "Elektrijada", Description = "Opis za Elektrijadu", TeamId = 1, Team = teams[0] },
                new Project { Name = "Takmicenje", Description = "Opis za takmicenje", TeamId = 2, Team = teams[1] },
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject { Member = members[0], Project = projects[0], Function = "posetilac" },
                new MemberProject { Member = members[0], Project = projects[1], Function = "ucesnik" },
                new MemberProject { Member = members[1], Project = projects[1], Function = "organizator" },
                new MemberProject { Member = members[2], Project = projects[2], Function = "organizator" },
            };

            context.MemberProjects.AddRange(memberprojects);

            context.SaveChanges();
        }
    }
}
