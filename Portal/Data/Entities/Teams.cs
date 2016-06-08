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
        public static Team AddTeam()
        {
            using (var dc = new DataContext())
            {
                return new Team();
            }
        }

        public static Team GetTeamAt(int teamId)
        {
            using (var dc = new DataContext())
            {
                return (from t in dc.Teams where t.TeamId == teamId select t).First();
            }
        }

        public static void EditTeam(int teamId)
        {
            using (var dc = new DataContext())
            {

            }
        }

        public static void DeleteTeam(int teamId)
        {
            using (var dc = new DataContext())
            {

            }
        }

        public static void AddMembersToTeam(int teamId)
        {
            using (var dc = new DataContext())
            {

            }
        }
    }
}
