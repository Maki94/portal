using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Entities;

namespace MVC.ViewModels.Badge
{
    public class BadgeListViewModel
    {
        public List<BadgeThumbnailDTO> Badges { get; set; }

        public BadgeListViewModel()
        {
            Badges = Data.Entities.Badges.GetAllBadges();
        }
    }
}