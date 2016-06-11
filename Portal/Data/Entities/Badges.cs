using Data.DataClasses;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Badges
    {
        public static List<BadgeThumbnailDTO> GetAllBadges()
        {
            using (var dc = new DataContext())
            {
                var badges = (from b in dc.Badges select b).ToList();
                return badges.Select(b => new BadgeThumbnailDTO
                {
                    BadgeId = b.BadgeId,
                    Name = b.Name,
                    Description = b.Description,
                    Image = b.Image
                }).ToList();
            }
        }

        public static void CreateBadge(string name, string desc, byte[] array)
        {
            using (var dc = new DataContext())
            {
                dc.Badges.Add(new Badge
                {
                    Name = name,
                    Description = desc,
                    Image = array
                });

                dc.SaveChanges();
            }
        }

        public static void DeleteBadge(int badgeId)
        {
            using (var dc = new DataContext())
            {
                Badge b = new Badge() { BadgeId = badgeId };
                dc.Badges.Attach(b);
                dc.Entry(b).State = EntityState.Deleted;
                //var deletedBadge = dc.Badges.Remove(b);
                dc.SaveChanges();
            }
        }

        public static Badge GetBadgeAt(int badgeId)
        {
            using (var dc = new DataContext())
            {
                return (from a in dc.Badges where a.BadgeId == badgeId select a).First();
            }
        }

        public static List<Badge> GetAllBadgesOfMember(int memberId)
        {
            using (var dc = new DataContext())
            {
                List<MemberBadge> mb = GetAllMemberBadgesOfMember(memberId);
                List<Badge> b = new List<Badge>();

                foreach (MemberBadge m in mb)
                {
                    b.Add(GetBadgeAt(m.BadgeId));
                }
 
                return b;
            }
        }

        public static List<MemberBadge> GetAllMemberBadgesOfMember(int memberId)
        {
            using (var dc = new DataContext())
            {
                return (from mb in dc.MemberBadges where mb.MemberId == memberId select mb).ToList();
            }
        }


    }
}
