using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using Data.Entities;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {

            var m1 = new Member { Username = "zantsu", Name = "Milos", Surname = "Jajac", Nickname = "jajac", JoinDate = DateTime.Now, Status = "clan" };
            var m2 = new Member { Username = "tesla", Name = "Nikola", Surname = "Mitic", Nickname = "tesla", JoinDate = DateTime.Now, Status = "clan" };
            var m3 = new Member { Username = "mare", Name = "Marko", Surname = "Mihajlovic", Nickname = "mare", JoinDate = DateTime.Now, Status = "clan" };
            var m4 = new Member { Username = "strale", Name = "Strahinja", Surname = "Mijajlovic", Nickname = "strale", JoinDate = DateTime.Now, Status = "clan" };

            var p1 = new Project { Title = "JobFair", Description = "Opis za JobFair" };
            var p2 = new Project { Title = "Elektrijada", Description = "Opis za Elektrijadu" };
            var p3 = new Project { Title = "Takmicenje", Description = "Opis za takmicenje" };

            var mp1 = new MemberProject { MemberID = 1, ProjectID = 1, Member = m1, Project = p1, Role = "posetilac" };
            var mp2 = new MemberProject { MemberID = 1, ProjectID = 2, Member = m1, Project = p2, Role = "ucesnik" };
            var mp3 = new MemberProject { MemberID = 2, ProjectID = 1, Member = m2, Project = p1, Role = "organizator" };
            var mp4 = new MemberProject { MemberID = 2, ProjectID = 2, Member = m2, Project = p2, Role = "organizator" };
            var mp5 = new MemberProject { MemberID = 2, ProjectID = 3, Member = m2, Project = p3, Role = "organizator" };
            var mp6 = new MemberProject { MemberID = 3, ProjectID = 1, Member = m3, Project = p1, Role = "organizator" };
            var mp7 = new MemberProject { MemberID = 4, ProjectID = 3, Member = m4, Project = p3, Role = "ucesnik" };

            context.MemberProjects.Add(mp1);
            context.MemberProjects.Add(mp2);
            context.MemberProjects.Add(mp3);
            context.MemberProjects.Add(mp4);
            context.MemberProjects.Add(mp5);
            context.MemberProjects.Add(mp6);
            context.MemberProjects.Add(mp7);

            context.SaveChanges();
        }
    }
}
