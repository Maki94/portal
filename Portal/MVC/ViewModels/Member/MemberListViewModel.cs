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
        public List<MemberDTO> MemberThumbnails { get; set; }

        public Data.DataClasses.Member Profile { get; set; }
        public MemberListViewModel()
        {
            MemberThumbnails = Members.GetAllMemberThumbnails();
            Profile = Members.GetMemberAt(MemberSession.GetMemberId());
        }
    }
}