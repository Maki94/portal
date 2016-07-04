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
        public List<MemberDTO> fullMembers { get; set; }
        public List<MemberDTO> whitoutMaster { get; set; }
        public List<MemberMasterDTO> reports { get; set; }

        
        public static MPModel Load()
        {
            MPModel mpm = new MPModel();
            mpm.master = new MPDTO();
            mpm.master.Padawans = new List<MemberDTO>();

            mpm.fullMembers = new List<MemberDTO>();
            mpm.whitoutMaster = new List<MemberDTO>();
            mpm.reports = new List<MemberMasterDTO>();

            List<Member> padawans = Data.Entities.MPs.GetPadawansFrom(MemberSession.GetMemberId());

            foreach(Member p in padawans)
            {
                if(!p.IsDeleted)
                {
                    mpm.master.Padawans.Add(MVC.Models.CompanyModel.createMemberDTO(p));
                }
            }

            List<Member> full = Data.Entities.Members.GetFullMembers();

            foreach (Member f in full)
            {
                if (!f.IsDeleted)
                {
                    mpm.fullMembers.Add(MVC.Models.CompanyModel.createMemberDTO(f));
                }
            }

            List<Member> without = Data.Entities.Members.GetWithoutMaster();

            foreach (Member m in without)
            {
                mpm.whitoutMaster.Add(MVC.Models.CompanyModel.createMemberDTO(m));
            }

            List<MemberMaster> report = Data.Entities.MPs.GetReports();

            foreach (MemberMaster mm in report)
            {
                mpm.reports.Add(createMemberMasterDTO(mm));
            }

            return mpm;
        }

        public static MemberMasterDTO createMemberMasterDTO(MemberMaster mm)
        {
            return new MemberMasterDTO
            {
                Padawan = MVC.Models.CompanyModel.createMemberDTO(mm.Member),
                Master = MVC.Models.CompanyModel.createMemberDTO(Data.Entities.Members.GetMemberAt(mm.MemberId)),
                Reports = createReportsDTO(mm.Reports),
                MemberMasterId = mm.MemberMasterId
            };
        }

        public static List<ReportDTO> createReportsDTO(ICollection<Report> reports)
        {
            List<ReportDTO> rep = new List<ReportDTO>();
            foreach (Report r in reports)
            {
                rep.Add(createReportDTO(r));
            }
            return rep;
        }

        public static ReportDTO createReportDTO(Report r)
        {
            return new ReportDTO
            {
                MemberMasterId = r.MemberMasterId,
                ReportId = r.ReportId,
                Text = r.Text,
                Time = r.Time
            };
        }

    }
}