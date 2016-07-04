using Data.DataClasses;
using Data.DTOs;
using Data.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CompanyModel
    {
        public List<CommentDTO> NewComments { get; set; }
        public List<CompanyDTO> MyCompany { get; set; }
        public List<CompanyDTO> AllCompany { get; set; }
        public CompanyDTO Show { get; set; }

        public static CompanyModel Load(int id)
        {
            CompanyModel model = new CompanyModel();
            model.AllCompany = new List<CompanyDTO>();
            model.MyCompany = new List<CompanyDTO>();
            model.NewComments = new List<CommentDTO>();

            List<Company> allCompany = Data.Entities.Companys.GetAll();

            foreach (Company c in allCompany)
            {
                model.AllCompany.Add(createCompanyDTO(c));
            }

            List<Company> myCompany = Data.Entities.Companys.GetDelegateTo(id);

            foreach (Company c in myCompany)
            {
                model.MyCompany.Add(createCompanyDTO(c));
            }

            model.Show = model.MyCompany.FirstOrDefault();

            List<Comment> lastComments = Data.Entities.Comments.GetLast(20);

            foreach (Comment c in lastComments)
            {
                model.NewComments.Add(createCommentDTO(c));
            }
            return model;
        }

        private static CommentDTO createCommentDTO(Comment c)
        {
            List<MemberComment> truecomm =  c.Likes?.Where(x => x.IsDeleted == false).ToList();
            return new CommentDTO
            {
                AuthorFullName = c.Author.Name + " " + c.Author.Surname,
                CommentId = c.CommentId,
                AuthorId = c.AuthorId,
                CompanyName = c.Company.Name,
                CompanyId = c.CompanyId,
                ProjectId = c.ProjectId,
                ProjectNameYear = c.Project.Name + " " + c.Project.FinishDate.Year,
                Text = c.Text,
                Time = c.Time,
                Type = c.Type,
                CountLikes = truecomm != null ? truecomm.Count:0,
                LikersNames = truecomm!=null ? string.Join(", ", truecomm.Select(x => x.Member.Name + " " + x.Member.Surname).ToArray()) : "",
                Likers = createMembersDTO(truecomm!=null ? truecomm : new List<MemberComment>())
            };
        }

        private static List<MemberDTO> createMembersDTO(List<MemberComment> likes)
        {
            List<MemberDTO> md = new List<MemberDTO>();
            foreach (MemberComment m in likes)
            {
                if(!m.IsDeleted)
                    md.Add(crateMemberDTO(m.Member));
            }
            return md;
        }

        private static MemberDTO crateMemberDTO(Member member)
        {
            return new MemberDTO
            {
                MemberId = member.MemberId
            };
        }

        private static CompanyDTO createCompanyDTO(Company c)
        {
            return new CompanyDTO
            {
                Address = c.Address,
                City = c.City,
                CompanyId = c.CompanyId,
                Contacts = createContactsDTO(c.Contacts),
                Comments = createCommentsDTO(c.Comments),
                Description = c.Description,
                Email = c.Email,
                Field = c.Field,
                Name = c.Name,
                Phone = c.Phone,
                Type = c.Type,
                Website = c.Website
            };
        }

        private static List<CommentDTO> createCommentsDTO(ICollection<Comment> comments)
        {
            List<CommentDTO> md = new List<CommentDTO>();
            foreach (Comment c in comments)
            {
                if (!c.IsDeleted)
                    md.Add(createCommentDTO(c));
            }
            return md;
        }

        private static List<ContactPersonDTO> createContactsDTO(ICollection<ContactPerson> contacts)
        {
            if (contacts==null)
                return new List<ContactPersonDTO>();

            List<ContactPersonDTO> cp = new List<ContactPersonDTO>();
            foreach (ContactPerson c in contacts)
            {
                if(!c.IsDeleted)
                    cp.Add(crateContactDTO(c));
            }
            return cp;
        }

        private static ContactPersonDTO crateContactDTO(ContactPerson c)
        {
            return new ContactPersonDTO
            {
                ContactPersonId = c.ContactPersonId,
                StartDate = c.StartDate,
                Email = c.Email,
                Name = c.Name,
                Note = c.Note,
                Phone = c.Phone
            };
        }
    }
}