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
                    Name = "Milos",
                    Surname = "Jajac",
                    Nickname = "jajac",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1994, 5, 26),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "zantsusan@gmail.com",
                    Phone = "0641234123",
                    Role = roles[5],
                    LastChatParticipant = 2
                },
                new Member
                {
                    Name = "Mika",
                    Surname = "Mikic",
                    Nickname = "mika",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1999, 7, 13),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "mikamikic@gmail.com",
                    Phone = "0691231231",
                    Role = roles[5],
                    LastChatParticipant = 1
                },
                new Member
                {
                    Name = "Marko",
                    Surname = "Mihajlovic",
                    Nickname = "Maki",
                    Faculty = "Elfak",
                    DateOfBirth = new DateTime(1994, 7, 13),
                    JoinDate = DateTime.Now,
                    Password = "Admin@123",
                    Gmail = "maki@gmail.com",
                    Phone = "0651231231",
                    Role = roles[5],
                    LastChatParticipant = 1
                }
            };

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
                new Project {Name = "JobFair", Description = "Opis za JobFair", Team = teams[0]},
                new Project {Name = "Elektrijada", Description = "Opis za Elektrijadu", Team = teams[0]},
                new Project {Name = "Takmicenje", Description = "Opis za takmicenje", Team = teams[1]}
            };

            var memberprojects = new List<MemberProject>
            {
                new MemberProject {Member = members[0], Project = projects[0], Function = "posetilac"},
                new MemberProject {Member = members[0], Project = projects[1], Function = "ucesnik"},
                new MemberProject {Member = members[1], Project = projects[1], Function = "organizator"},
                new MemberProject {Member = members[2], Project = projects[2], Function = "organizator"}
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
                    HideResultsUntilFinished = false,
                    HideVoters = false,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2016, 7, 20),
                    State = Enumerations.PollState.aktivan,
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
                new PollOption {Poll = polls[1], Answer = "Odgovor2 za glasanje2"}
            };

            context.PollOptions.AddRange(polloptions);
            context.SaveChanges();

            var memberpolloptions = new List<MemberPollOption>
            {
                // za prvo glasanje
                new MemberPollOption {Member = members[0], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[0]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[1]},
                new MemberPollOption {Member = members[1], PollOption = polloptions[3]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[3]},

                // za drugo glasanje
                new MemberPollOption {Member = members[1], PollOption = polloptions[6]},
                new MemberPollOption {Member = members[2], PollOption = polloptions[6]}
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
            context.SaveChanges();
        }
    }
}