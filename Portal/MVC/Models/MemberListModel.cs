using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MemberListModel
    {
        public List<MemberDTO> MemberThumbnails { get; set; }
        public Data.DataClasses.Member Profile { get; set; }

        public MemberListModel()
        {
            MemberThumbnails = Members.GetAllMemberThumbnails();
            Profile = Members.GetMemberAt(MemberSession.GetMemberId());
        }
    }
}