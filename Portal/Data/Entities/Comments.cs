using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.Company;
using Data.DataClasses;
using System.Data.Entity;

namespace Data.Entities
{
    public class Comments
    {
        public static List<Comment> GetLast(int v)
        {
            using (var dc = new DataContext())
            {
                return dc.Comments.Include(x => x.Author).Include(x => x.Company).Include(x => x.Project).Include(x => x.Likes).Include(x=>x.Likes.Select(z=>z.Member)).OrderByDescending(x=>x.Time).Take(v).ToList();
            }
        }

        public static void Like(int idm, int idc)
        {
            using (var dc = new DataContext())
            {
                Member m = dc.Members.Where(x => x.MemberId == idm).First();
                Comment c = dc.Comments.Where(x => x.CommentId == idc).Include(x=>x.Author).Include(x=>x.Likes).First();

                if (m != c.Author && !c.Likes.Where(x=>x.IsDeleted==false).Select(x => x.Member).Contains(m))
                {

                    MemberComment mc = new MemberComment
                    {
                        Comment = c,
                        Member = m
                    };

                    dc.MemberComments.Add(mc);

                    dc.SaveChanges();
                }
            }
        }

        public static void Unlike(int idm, int idc)
        {
            using (var dc = new DataContext())
            {
                MemberComment mc = dc.MemberComments.Where(x => x.MemberId == idm && x.CommentId == idc && x.IsDeleted == false).First();
                mc.IsDeleted = true;                   
                dc.SaveChanges();
            }
        }
    }
}
