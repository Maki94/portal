using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using Data.DataClasses;

namespace Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // ovo stampa sql query u Output prozor
            context.Database.Log = s => Debug.WriteLine(s);

            var roles = new List<Role>();
            foreach (var r in Enum.GetValues(typeof (Enumerations.Role)))
            {
                roles.Add(new Role {Name = r.ToString()});
            }

            var permissions = new List<Permission>();
            foreach (var p in Enum.GetValues(typeof (Enumerations.Permission)))
            {
                permissions.Add(new Permission {Name = p.ToString()});
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
                new Member
                {
                    Name = "Miloš",
                    Surname = "Jajac",
                    Nickname = "jajac",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1994, 5, 26),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "milosjajac@gmail.com",
                    Phone = "0641234123",
                    Role = roles[5],
                    LastChatParticipant = 2,
                    Skype = "jajac94",
                    Facebook = "https://www.facebook.com/milos.jajac",
                },
                new Member
                {
                    Name = "Nikola",
                    Surname = "Mitić",
                    Nickname = "tesla",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1999, 7, 13),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "nikolamitic@gmail.com",
                    Phone = "0691231231",
                    Role = roles[5],
                    LastChatParticipant = 1,
                    Skype = "mitic94",
                    Facebook = "https://www.facebook.com/mitic.nikolca",
                },
                new Member
                {
                    Name = "Marko",
                    Surname = "Mihajlović",
                    Nickname = "maki",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1994, 7, 13),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "maki@gmail.com",
                    Phone = "0651231231",
                    Role = roles[5],
                    LastChatParticipant = 1,
                    Skype = "marko94",
                    Facebook = "https://www.facebook.com/mihajlovic.maki",
                }
            };

            var badges = new List<Badge>
            {
                new Badge { Name = "JobFair manijak", Description = "bla"},
                new Badge { Name = "Pobednik elektrijade", Description = "nesto" },
            };

            var memberbadges = new List<MemberBadge>
            {
                new MemberBadge { Member = members[0], Badge = badges[0] },
                new MemberBadge { Member = members[0], Badge = badges[1] },
            };

            context.MemberBadges.AddRange(memberbadges);
            context.SaveChanges();

            var teams = new List<Team>
            {
                new Team {Name = "Team 1"},
                new Team {Name = "Team 2"}
            };

            var memberteams = new List<MemberTeam>
            {
                new MemberTeam {Member = members[0], Team = teams[0]},
                new MemberTeam {Member = members[0], Team = teams[1]},
                new MemberTeam {Member = members[1], Team = teams[1]},
                new MemberTeam {Member = members[2], Team = teams[1]}
            };

            var projects = new List<Project>
            {
                new Project {Name = "JobFair", Description = "Opis za JobFair", Team = teams[0], StartDate = new DateTime(2016, 5, 20), FinishDate = new DateTime(2016, 7, 20), Place = "Nis", State = Enumerations.ProjectState.aktivan, Website = "www.jobfairnis.rs"},
                new Project {Name = "Elektrijada", Description = "Opis za Elektrijadu", Team = teams[0], StartDate = new DateTime(2016, 6, 20), FinishDate = new DateTime(2016, 7, 15), Place = "Nis", State = Enumerations.ProjectState.aktivan, Website = "www.elektrijada.com"},
                new Project {Name = "Takmicenje", Description = "Opis za takmicenje", Team = teams[1], StartDate = new DateTime(2016, 3, 15), FinishDate = new DateTime(2016, 4, 15), Place = "Nis", State = Enumerations.ProjectState.zatvoren, Website = "www.google.com"},
                new Project {Name = "Takmicenje 2", Description = "Opis za takmicenje", Team = teams[1], StartDate = new DateTime(2015, 3, 15), FinishDate = new DateTime(2015, 4, 15), Place = "Nis", State = Enumerations.ProjectState.zatvoren, Website = "www.google.com"},
                new Project {Name = "Takmicenje 3", Description = "Opis za takmicenje", Team = teams[1], StartDate = new DateTime(2014, 3, 15), FinishDate = new DateTime(2014, 4, 15), Place = "Nis", State = Enumerations.ProjectState.zatvoren, Website = "www.google.com"}
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject {Member = members[0], Project = projects[0], Function = "posetilac"},
                new MemberProject {Member = members[0], Project = projects[1], Function = "ucesnik"},
                new MemberProject {Member = members[1], Project = projects[1], Function = "organizator"},
                new MemberProject {Member = members[2], Project = projects[2], Function = "organizator"},
                new MemberProject {Member = members[0], Project = projects[3], Function = "ucesnik"},
                new MemberProject {Member = members[1], Project = projects[3], Function = "organizator"},
                new MemberProject {Member = members[2], Project = projects[4], Function = "organizator"}
            };

            context.MemberProjects.AddRange(memberprojects);
            context.MemberTeams.AddRange(memberteams);

            context.SaveChanges();

            var polls = new List<Poll>
            {
                new Poll
                {
                    Topic = "Glasanje1",
                    Description = "Ovo je opis za glasanje1.",
                    AllowMultiple = true,
                    HideResultsUntilFinished = false,
                    HideVoters = false,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2016, 7, 20),
                    State = Enumerations.PollState.aktivan,
                    PollCreator = members[0]
                },
                new Poll
                {
                    Topic = "Glasanje2",
                    Description = "Ovo je opis za glasanje2.",
                    AllowMultiple = false,
                    HideResultsUntilFinished = true,
                    HideVoters = true,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2016, 7, 20),
                    State = Enumerations.PollState.aktivan,
                    PollCreator = members[1]
                },
                new Poll
                {
                    Topic = "Glasanje3",
                    Description = "Ovo je opis za glasanje3.",
                    AllowMultiple = true,
                    HideResultsUntilFinished = false,
                    HideVoters = true,
                    StartDate = new DateTime(2016, 5, 20),
                    EndDate = new DateTime(2016, 5, 27),
                    State = Enumerations.PollState.zatvoren,
                    PollCreator = members[1]
                },
                new Poll
                {
                    Topic = "Glasanje4",
                    Description = "Ovo je opis za glasanje4.",
                    AllowMultiple = false,
                    HideResultsUntilFinished = false,
                    HideVoters = false,
                    StartDate = new DateTime(2016, 3, 20),
                    EndDate = new DateTime(2016, 3, 23),
                    State = Enumerations.PollState.zatvoren,
                    PollCreator = members[1]
                }
            };

            context.Polls.AddRange(polls);
            context.SaveChanges();

            var polloptions = new List<PollOption>
            {
                new PollOption {Poll = polls[0], Answer = "Odgovor1 za glasanje1"},
                new PollOption {Poll = polls[0], Answer = "Odgovor2 za glasanje1"},
                new PollOption {Poll = polls[0], Answer = "Odgovor3 za glasanje1"},
                new PollOption {Poll = polls[0], Answer = "Odgovor4 za glasanje1"},
                new PollOption {Poll = polls[0], Answer = "Odgovor5 za glasanje1"},

                new PollOption {Poll = polls[1], Answer = "Odgovor1 za glasanje2"},
                new PollOption {Poll = polls[1], Answer = "Odgovor2 za glasanje2"},

                new PollOption {Poll = polls[2], Answer = "Odgovor1 za glasanje3"},
                new PollOption {Poll = polls[2], Answer = "Odgovor2 za glasanje3"},
                new PollOption {Poll = polls[2], Answer = "Odgovor3 za glasanje3"},
                new PollOption {Poll = polls[2], Answer = "Odgovor4 za glasanje3"},
                new PollOption {Poll = polls[2], Answer = "Odgovor5 za glasanje3"},

                new PollOption {Poll = polls[3], Answer = "Odgovor1 za glasanje4"},
                new PollOption {Poll = polls[3], Answer = "Odgovor2 za glasanje4"},
                new PollOption {Poll = polls[3], Answer = "Odgovor1 za glasanje4"},
                new PollOption {Poll = polls[3], Answer = "Odgovor2 za glasanje4"}
            };

            context.PollOptions.AddRange(polloptions);
            context.SaveChanges();

            var memberpolloptions = new List<MemberPollOption>
            {
                // za prvo glasanje
                //new MemberPollOption {Member = members[0], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[1]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[3]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[3]},

                // za drugo glasanje
                new MemberPollOption {Member = members[0], PollOption = polloptions[5]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[6]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[6]},

                // za trece (zatvoreno) glasanje
                new MemberPollOption {Member = members[0], PollOption = polloptions[7]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[7]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[7]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[8]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[9]},

                // za cetvrto (zatvoreno) glasanje
                new MemberPollOption {Member = members[0], PollOption = polloptions[12]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[12]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[13]},
            };

            context.MemberPollOptions.AddRange(memberpolloptions);

            var messages = new List<Message>
            {
                new Message {Receiver = members[0], Sender = members[1],  Text = "Hello", Time = DateTime.Now},
                new Message {Receiver = members[1], Sender = members[0],  Text = "Hello", Time = DateTime.Now},
                new Message {Receiver = members[1], Sender = members[0],  Text = "Whats Up", Time = DateTime.Now},
                new Message {Receiver = members[1], Sender = members[0],  Text = "Nth", Time = DateTime.Now},
                new Message {Receiver = members[1], Sender = members[0],  Text = "Ok", Time = DateTime.Now},
                new Message {Receiver = members[1], Sender = members[0],  Text = "Bye", Time = DateTime.Now},
                new Message {Receiver = members[0], Sender = members[1],  Text = "Hello Again", Time = DateTime.Now},
                new Message {Receiver = members[0], Sender = members[1],  Text = "What's up", Time = DateTime.Now}
            };

            context.Messages.AddRange(messages);

            var company = new List<Company>
            {
                new Company { Address = "a", City = "a", Description = "a", Email = "a", Field = Enumerations.CompanyField.AutoIndustrija, Name = "a", Phone = "123", Type = Enumerations.CompanyType.money, Website = "a"},
                new Company { Address = "b", City = "b", Description = "b", Email = "b", Field = Enumerations.CompanyField.Bank, Name = "b", Phone = "456", Type = Enumerations.CompanyType.money, Website = "b"},
                new Company { Address = "c", City = "c", Description = "c", Email = "c", Field = Enumerations.CompanyField.IT, Name = "c", Phone = "789", Type = Enumerations.CompanyType.money, Website = "c"},
                new Company { Address = "d", City = "d", Description = "d", Email = "d", Field = Enumerations.CompanyField.Bank, Name = "d", Phone = "101", Type = Enumerations.CompanyType.money, Website = "d" },
                new Company { Address = "e", City = "e", Description = "e", Email = "e", Field = Enumerations.CompanyField.IT, Name = "e", Phone = "112", Type = Enumerations.CompanyType.money, Website = "e" }
            };

            context.Companies.AddRange(company);
            var comment = new List<Comment>
            {
                new Comment {Author = members[1], Company = company[0], Project = projects[0], Text = "bla", Time = DateTime.Now, Type = Enumerations.CommentType.Classic }
            };

            context.Comments.AddRange(comment);

            var likes = new List<MemberComment>
            {
                new MemberComment { Comment = comment[0], Member = members[1] }
            };

            context.MemberComments.AddRange(likes);

            var delegates = new List<MemberCompany>
            {
                new MemberCompany {Company = company[0], Member = members[0], StartDate=DateTime.Now }
            };

            context.MemberCompanies.AddRange(delegates);

            var contact = new List<ContactPerson>
            {
                new ContactPerson {Company = company[0], Email="bla", Name="bla", Phone="123", Note = "bla", StartDate = DateTime.Now },
                new ContactPerson {Company = company[0], Email="aaa", Name="aaa", Phone="123", Note = "aaa", StartDate = DateTime.Now }
            };
            context.ContactPersons.AddRange(contact);
            context.SaveChanges();
        }
    }
}