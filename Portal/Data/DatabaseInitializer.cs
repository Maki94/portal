using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using Data.Entities;
using System.Data.SqlTypes;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var members = new List<Member>
            {
                new Member { Username = "zantsu", Name = "Milos", Surname = "Jajac", Nickname = "jajac", Faculty = "Elfak", DateOfBirth = new DateTime(1994, 5, 26), JoinDate = DateTime.Now, /*Status = Enumerations.MemberStatus.pocetnik, */Facebook = "#", LinkedIn = "#" },
                new Member { Username = "tesla", Name = "Nikola", Surname = "Mitic", Nickname = "tesla", DateOfBirth = (DateTime) SqlDateTime.MinValue, JoinDate = DateTime.Now,/* Status = Enumerations.MemberStatus.bitan */},
                new Member { Username = "mare", Name = "Marko", Surname = "Mihajlovic", Nickname = "mare", DateOfBirth = (DateTime) SqlDateTime.MinValue, JoinDate = DateTime.Now,/* Status = Enumerations.MemberStatus.car */},
                new Member { Username = "strale", Name = "Strahinja", Surname = "Mijajlovic", Nickname = "strale", DateOfBirth = (DateTime) SqlDateTime.MinValue, JoinDate = DateTime.Now,/* Status = Enumerations.MemberStatus.kralj */},
            };

            var projects = new List<Project>
            {
                new Project { Name = "JobFair", Description = "Opis za JobFair" },
                new Project { Name = "Elektrijada", Description = "Opis za Elektrijadu" },
                new Project { Name = "Takmicenje", Description = "Opis za takmicenje" },
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject { MemberId = 1, ProjectId = 1, Member = members[0], Project = projects[0], Role = "posetilac" },
                new MemberProject { MemberId = 1, ProjectId = 2, Member = members[0], Project = projects[1], Role = "ucesnik" },
                new MemberProject { MemberId = 2, ProjectId = 1, Member = members[1], Project = projects[0], Role = "organizator" },
                new MemberProject { MemberId = 2, ProjectId = 2, Member = members[1], Project = projects[1], Role = "organizator" },
                new MemberProject { MemberId = 2, ProjectId = 3, Member = members[1], Project = projects[2], Role = "organizator" },
                new MemberProject { MemberId = 3, ProjectId = 1, Member = members[2], Project = projects[0], Role = "organizator" },
                new MemberProject { MemberId = 4, ProjectId = 3, Member = members[3], Project = projects[2], Role = "ucesnik" },
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

            memberprojects.ForEach(x => context.MemberProjects.Add(x));
            //memberteams.ForEach(x => context.MemberTeams.Add(x));

            context.SaveChanges();
        }
    }
}
