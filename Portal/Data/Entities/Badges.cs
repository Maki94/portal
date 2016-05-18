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
                var badgeThumbnails = new List<BadgeThumbnailDTO>();
                foreach (var b in badges)
                {
                    badgeThumbnails.Add(new BadgeThumbnailDTO
                    {
                        Name = b.Name,
                        Description = b.Description,
                        Image = b.Image,
                    });
                }
                return badgeThumbnails;
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
    }
}
