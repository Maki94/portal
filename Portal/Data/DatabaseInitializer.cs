﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using Data;
using Data.DataClasses;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // ovo stampa sql query u Output prozor
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            var roles = new List<Role>();
            foreach (var r in Enum.GetValues(typeof(Data.Enumerations.Role)))
            {
                roles.Add(new Role { Name = r.ToString()});
            }

            var permissions = new List<Permission>();
            foreach (var p in Enum.GetValues(typeof(Data.Enumerations.Permission)))
            {
                permissions.Add(new Permission { Name = p.ToString() });
            }

            // odmah snimam role i permisije da bi se snimile ovim redom jer ako
            // se prvo "povezu" addovanjem i snime onda se snimi prvo administrator
            // i njegova permisija pa tek onda sve ostale role
            context.Roles.AddRange(roles);
            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            // dodajemo administratoru permisiju da vidi About page
            roles[5].Permissions.Add(permissions[0]);
            roles[5].Permissions.Add(permissions[1]);
            roles[5].Permissions.Add(permissions[2]);

            var members = new List<Member>
            {
                new Member { Name = "Milos", Surname = "Jajac", Nickname = "jajac", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 5, 26), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "zantsusan@gmail.com", Phone = "0641234123", Role = roles[5] },
                new Member { Name = "Mika", Surname = "Mikic", Nickname = "Mika", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 4, 18), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "nikolcar@gmail.com", Phone = "0643156260", Role = roles[5] },
                new Member { Name = "Marko", Surname = "Mihajlovic", Nickname = "Maki", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 7, 13), JoinDate = DateTime.Now, Password = "Admin@123", Gmail = "maki@gmail.com", Phone = "0651231231", Role = roles[5] }
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
                new Project { Name = "JobFair", Description = "Opis za JobFair", Team = teams[0] },
                new Project { Name = "Elektrijada", Description = "Opis za Elektrijadu", Team = teams[0] },
                new Project { Name = "Takmicenje", Description = "Opis za takmicenje", Team = teams[1] },
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject { Member = members[0], Project = projects[0], Function = "posetilac" },
                new MemberProject { Member = members[0], Project = projects[1], Function = "ucesnik" },
                new MemberProject { Member = members[1], Project = projects[1], Function = "organizator" },
                new MemberProject { Member = members[2], Project = projects[2], Function = "organizator" },
            };

            context.MemberProjects.AddRange(memberprojects);
            context.MemberTeams.AddRange(memberteams);

            context.SaveChanges();
        }
    }
}
