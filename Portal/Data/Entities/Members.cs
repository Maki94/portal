using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.DataClasses;
using Data.DTOs;

namespace Data.Entities
{
    public class Members
    {
        public static Member AddMember(string gmail, string password, string name,
                                       string surname, int roleId, DateTime date, string nickname = null)
        {
            using (var dc = new DataContext())
            {
                var role = (from r in dc.Roles where r.RoleId == roleId select r).First();

                Member m = new Member
                {
                    Gmail = gmail,
                    Password = password,
                    Name = name,
                    Surname = surname,
                    Nickname = nickname,
                    Role = role,
                    JoinDate = date
                };

                dc.Members.Add(m);
                dc.SaveChanges();

                return m;
            }
        }

        public static Member GetMemberAt(int memberID)
        {
            using (var dc = new DataContext())
            {
                return dc.Members.Include(x => x.Role).Where(x => x.MemberId == memberID).First();
            }
        }

        public static List<Member> GetMemberBirthday(DateTime date)
        {
            using (var dc = new DataContext())
            {
                return (from m in dc.Members where m.DateOfBirth != null && ((DateTime)m.DateOfBirth).Day == date.Day && ((DateTime)m.DateOfBirth).Month == date.Month select m).ToList();
            }
        }

        public static List<Member> GetWithoutMaster(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                List<Member> mem = dc.Members.Where(x => !x.IsDeleted).ToList();
                List<MemberMaster> mm = dc.MemberMaster.Include(x => x.Member).Where(x => !x.IsDeleted && x.FinishDate==null).ToList();
                List<Member> pom = new List<Member>();

                foreach (Member m in mem)
                {
                    if (mm.Select(x => x.Member).ToList().Contains(m))
                    {
                        pom.Add(m);
                    }
                }
                foreach (Member m in pom)
                {
                    mem.Remove(m);
                }
                return mem;
            }
        }

        public static List<Member> GetMemberAnniversary(DateTime date)
        {
            //int[] anniversary = { 100, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000};
            
            using (var dc = new DataContext())
            {
                return (from m in dc.Members where m.JoinDate != null  && (((DateTime)m.JoinDate).Day == date.Day && ((DateTime)m.JoinDate).Month == date.Month) select m).ToList();
            }
        }

        public static Member MemberExists(string gmail, string pass)
        {
            using (var dc = new DataContext())
            {
                var find = (from m in dc.Members where m.Gmail == gmail && m.Password == pass select m);
                if (find.Any())
                    return find.First();
                else
                    return null;
            }
                
        }

        public static bool GmailExists(string gmail)
        {
            using (var dc = new DataContext())
            {
                return (from m in dc.Members where m.Gmail == gmail select m).Any();
            }
        }

        public static List<int> GetPermissions(int roleID)
        {
            using (var dc = new DataContext())
            {
                var permissions = (from r in dc.Roles where r.RoleId == roleID select r).First().Permissions.ToList();

                return permissions.Select(x => x.PermissionId).ToList();
            }
        }

        public static List<MemberDTO> GetAllMemberThumbnails()
        {
            using (var dc = new DataContext())
            {
                List<Member> members = (from m in dc.Members select m).ToList();

                List<MemberDTO> memberthumbnails = new List<MemberDTO>();

                foreach (var m in members)
                {
                    memberthumbnails.Add(new MemberDTO
                    {
                        MemberId = m.MemberId,
                        Name = m.Name,
                        Surname = m.Surname,
                        Nickname = m.Nickname ?? "",
                        Faculty = m.Faculty ?? "",
                        Role = (Enumerations.Role)m.Role.RoleId,
                        Status = m.Status
                    });
                }

                return memberthumbnails;
            }
        }

        public static List<int> SearchMembers(string keyword = "")
        {
            using (var dc = new DataContext())
            {
                List<string> searchTerms = new List<string>();
                searchTerms = keyword.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(m => m.ToLower());

                // trazimo da li ime, prezime ili nadimak clana sadrzi
                // neku od reci koje su unete kao search parametar
                List<Member> members = new List<Member>();
                if (searchTerms.Count == 0)
                {
                    members = (from m in dc.Members select m).ToList();
                }
                else
                {
                    members = (from m in dc.Members where
                                        searchTerms.Any(s => m.Name.Contains(s)) ||
                                        searchTerms.Any(s => m.Surname.Contains(s)) ||
                                        searchTerms.Any(s => m.Nickname.Contains(s))
                                            select m).ToList();
                }

                List<int> memberIds = members.Select(m => m.MemberId).ToList();

                return memberIds;
            }
        }

        public static void EditProfile(int memberID, string nickname, byte[] avatar,
                                       string faculty, DateTime? dob, Enumerations.MemberStatus? status,
                                       string phone, string facebook, string linkedin, string skype)
        {
            using (var dc = new DataContext())
            {
                var mem = (from m in dc.Members where m.MemberId == memberID select m).First();

                mem.Nickname = nickname ?? mem.Nickname;
                mem.Faculty = faculty ?? mem.Faculty;
                mem.Status = status ?? mem.Status;
                mem.DateOfBirth = dob ?? mem.DateOfBirth;
                mem.Phone = phone ?? mem.Phone;
                mem.Facebook = facebook ?? mem.Facebook;
                mem.LinkedIn = linkedin ?? mem.LinkedIn;
                mem.Avatar = avatar ?? mem.Avatar;

                dc.SaveChanges();
            }
        }

        public static bool CheckMemberPassword(int memberID, string password)
        {
            using (var dc = new DataContext())
            {
                var mem = GetMemberAt(memberID);

                return mem.Password == password;
            }
        }

        public static void ChangeMemberPassword(int memberId, string newPassword)
        {
            using (var dc = new DataContext())
            {
                var mem = GetMemberAt(memberId);
                mem.Password = newPassword;
                dc.SaveChanges();
            }
        }

        public static bool DeleteMember(int memberId)
        {
            using (var dc = new DataContext())
            {
                Member m = Members.GetMemberAt(memberId);
                var deletedMember = dc.Members.Remove(m);
                dc.SaveChanges();

                return m == deletedMember;
            }
        }

        public static void ChangeBadgesOfMember(int memberId, List<int> badgesIds)
        {
            using (var dc = new DataContext())
            {
                List<MemberBadge> OldBs = Badges.GetAllMemberBadgesOfMember(memberId);
                List<MemberBadge> NewBs = new List<MemberBadge>();
                
                foreach (int bId in badgesIds)
                {
                    MemberBadge mb = new MemberBadge
                    {
                        MemberId = memberId,
                        BadgeId = bId
                    };
                    NewBs.Add(mb);
                }

                List<MemberBadge> DeleteBs = OldBs.Except(NewBs).ToList();
                dc.MemberBadges.RemoveRange(DeleteBs);

                dc.SaveChanges();

                List<MemberBadge> AddBs = NewBs.Except(OldBs).ToList();
                dc.MemberBadges.AddRange(AddBs);
                
                dc.SaveChanges();
            }
        }

        public static List<Badge> GetMemberBadges(int memberId)
        {
            using (var dc = new DataContext())
            {
                List<Badge> badges = dc.MemberBadges.Where(x => x.MemberId == memberId).Select(x => x.Badge).ToList();

                return badges;
            }
        }

        public static void AddMemberToTeam(int memberId, int teamId, Enumerations.TeamRole role)
        {
            using (var dc = new DataContext())
            {

                MemberTeam mt = new MemberTeam
                {
                    MemberId = memberId,
                    TeamId = teamId,
                    TeamRole = role
                };

                dc.MemberTeams.Add(mt);
                dc.SaveChanges();
            }
        }

        public static List<Member> GetFullMembers(DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return dc.Members.Where(x => x.Status == Enumerations.MemberStatus.Full && !x.IsDeleted).ToList();
            }
        }
    }
}
