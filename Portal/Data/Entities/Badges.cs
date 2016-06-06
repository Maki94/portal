using Data.DataClasses;
using Data.DTOs;
using System;
using System.Collections.Generic;
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
                    BadgeId = b.BadgeId, Name = b.Name, Description = b.Description, Image = b.Image,
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
                    Image = array,
                });

                dc.SaveChanges();
            }
        }

        public static void DeleteBadge(int badgeId)
        {
            using (var dc = new DataContext())
            {
                Badge b = Badges.GetBadgesAt(badgeId, dc);
                dc.Badges.Remove(b);
                dc.SaveChanges();
            }
        }

        public static Badge GetBadgesAt(int badgeId, DataContext dc = null)
        {
            using (dc = dc ?? new DataContext())
            {
                return (from a in dc.Badges where a.BadgeId == badgeId select a).First();
            }
        }



    }
}
