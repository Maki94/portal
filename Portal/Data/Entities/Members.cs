using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using Data.DTOs;

namespace Data.Entities
{
    public class Members
    {
        public static Member AddMember(string username, string name, string surname, 
                                       string nickname, Enumerations.MemberStatus status, DateTime joindate)
        {
            using (var dc = new DataContext())
            {
                Member m = new Member
                {
                    Gmail = username,
                    Name = name,
                    Surname = surname,
                    Nickname = nickname,
                    //Status = status,
                    JoinDate = joindate,
                };

                dc.Members.Add(m);
                dc.SaveChanges();

                return m;
            }
        }

        public static Member GetMember(int memberID)
        {
            using (var dc = new DataContext())
            {
                return (from m in dc.Members where m.MemberId == memberID select m).First();
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

        public static List<string> GetPermissions(int roleID)
        {
            using (var dc = new DataContext())
            {
                var q = (from r in dc.Roles where r.RoleId == roleID select r).First().Permissions.ToList();

                List<string> s = q.Select(x => x.Name).ToList();

                return s;
            }
        }

        public static List<MemberThumbnailDTO> GetMemberThumbnails()
        {
            using (var dc = new DataContext())
            {
                List<Member> members = (from m in dc.Members select m).ToList();
                List<MemberThumbnailDTO> memberthumbnails = new List<MemberThumbnailDTO>();

                foreach (var mem in members)
                {
                    memberthumbnails.Add(new MemberThumbnailDTO
                    {
                        Name = mem.Name,
                        Surname = mem.Surname,
                        Nickname = mem.Nickname,
                        Faculty = mem.Faculty,
                        Avatar = mem.Avatar,
                    });
                }

                return memberthumbnails;
            }
        }

        public static void EditProfile(int memberID, string name, string surname, string nickname,
                                       string faculty, DateTime? dob, Enumerations.MemberStatus? status,
                                       string phone, string facebook, string linkedin, string skype)
        {
            using (var dc = new DataContext())
            {
                // ovde treba umesto ovog da se pozove get member ali nisam 101% siguran
                // kako da se snadjem lepo sa datacontextom u getmember funkciji, nebitno zasad
                var mem = (from m in dc.Members where m.MemberId == memberID select m).First();

                mem.Name = name ?? mem.Name;
                mem.Surname = surname ?? mem.Surname;
                mem.Nickname = nickname ?? mem.Nickname;
                mem.Faculty = faculty ?? mem.Faculty;
                mem.Status = status ?? mem.Status;
                mem.DateOfBirth = dob ?? mem.DateOfBirth;
                mem.Phone = phone ?? mem.Phone;
                mem.Facebook = facebook ?? mem.Facebook;
                mem.LinkedIn = linkedin ?? mem.LinkedIn;

                dc.SaveChanges();
            }
        }
    }
}
