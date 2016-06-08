using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataClasses;
using Data.DTOs;

namespace Data.Entities
{
    class Teams
    {
        public static Team AddTeam(string name, DateTime created, DateTime deleted, string googlegroup = null)
        {
            using (var dc = new DataContext())
            {
                Team t = new Team
                {
                    Name = name,
                    CreationDate = created,
                    Email = googlegroup,
                    DismantleDate = deleted
                };

                dc.Teams.Add(t);
                dc.SaveChanges();

                return t;
            }
        }

        public static Team GetTeamAt(int teamId)
        {
            using (var dc = new DataContext())
            {
                return (from t in dc.Teams where t.TeamId == teamId select t).First();
            }
        }
        

        public static bool DeleteTeam(int teamId)
        {
            using (var dc = new DataContext())
            {
                Team t = Teams.GetTeamAt(teamId);
                var deletedTeam = dc.Teams.Remove(t);
                dc.SaveChanges();

                return t == deletedTeam;
            }
        }

        public static void AddMembersToTeam(int teamId, List<int> memberIds, List<Enumerations.TeamRole> roles)
        {
            for (int i=0; i<memberIds.Count; i++)
            {
                Members.AddMemberToTeam(memberIds[i], teamId, roles[i]);
            }
        }
    }
}
