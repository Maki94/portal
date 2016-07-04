using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.DTOs;
using Data.DataClasses;

namespace MVC.Models
{
    public class MPModel
    {
        public MPDTO master { get; set; }

        private MPModel()
        {
            master = new MPDTO();
            master.Padawans = new List<MemberDTO>();
        }

        public static MPModel Load()
        {
            MPModel mpm = new MPModel();

            List<Member> padawans = Data.Entities.MPs.GetPadawansFrom(MemberSession.GetMemberId());

            foreach(Member p in padawans)
            {
                if(!p.IsDeleted)
                {
                    mpm.master.Padawans.Add(MVC.Models.CompanyModel.crateMemberDTO(p));
                }
            }
            return mpm;
        }
    }
}