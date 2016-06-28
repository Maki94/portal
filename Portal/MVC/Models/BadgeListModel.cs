using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Entities;

namespace MVC.Models
{
    public class BadgeListModel
    {
        public List<BadgeDTO> Badges { get; set; }

        public BadgeListModel()
        {
            Badges = Data.Entities.Badges.GetAllBadges();
        }
    }
}