﻿using System;
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
            DataContext dc = new DataContext();

            Member m = new Member
            {
                Username = username,
                Name = name,
                Surname = surname,
                Nickname = nickname,
                Status = status,
                JoinDate = joindate,
            };

            dc.Members.Add(m);
            dc.SaveChanges();

            return m;
        }

        public static Member GetMember(int memberID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();
            return (from m in dc.Members where m.MemberID == memberID select m).First();
        }

        public static List<string> GetMemberEmails(int memberID, DataContext dc = null)
        {
            dc = dc ?? new DataContext();
            return (from e in dc.Emails
                    where e.OwnerType == Enumerations.OwnerType.member &&
                          e.MemberID == memberID select e.Address).ToList();
        }
    }
}
