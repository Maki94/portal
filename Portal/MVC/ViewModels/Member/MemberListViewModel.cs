using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Member
{
    public class MemberListViewModel
    {
        public List<MemberThumbnailDTO> MemberThumbnails { get; set; }

        public MemberListViewModel()
        {
            MemberThumbnails = Members.GetMemberThumbnails();
        }
    }
}