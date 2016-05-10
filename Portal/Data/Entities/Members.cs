using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;

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
                var q = dc.Roles.Where(role => role.RoleId == roleID).Select(role => role.Permissions).ToList();

                List<string> s = null;

                q.ForEach(permissions => s.AddRange(permissions.Select(permission => permission.Name)));

                return s;
            }
        }

        public static List<Member> GetAllMember(DataContext dc = null)
        {

            dc = dc ?? new DataContext();

            return (from p in dc.Members select p).ToList();
        }
    }
}
