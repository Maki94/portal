using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Entities;
using Data.DataClasses;

namespace MVC.Models
{
    public class BadgeListModel
    {
        public List<BadgeDTO> Badges { get; set; }

        public BadgeListModel()
        {
            Badges = Data.Entities.Badges.GetAllBadges();
        }

        public static List<BadgeDTO> CreateBadgeDTOs(List<Badge> badges)
        {
            List<BadgeDTO> badgeDTOs = new List<BadgeDTO>();
            foreach (var b in badges)
            {
                badgeDTOs.Add(new BadgeDTO
                {
                    BadgeId = b.BadgeId,
                    Name = b.Name,
                    Description = b.Description,
                    Image = b.Image
                });
            }

            return badgeDTOs;
        }
    }
}