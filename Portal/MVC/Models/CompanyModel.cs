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

            List<Comment> lastComments = Data.Entities.Comments.GetLast(20);

            foreach (Comment c in lastComments)
            {
                model.NewComments.Add(createCommentDTO(c));
            }
            return model;
        }

        private static CommentDTO createCommentDTO(Comment c)
        {
            return new CommentDTO
            {
                //AuthorFullName = c.Author.Name + " " + c.Author.Surname,
                CommentId = c.CommentId,
                AuthorId = c.AuthorId,
                //CompanyName = c.Company.Name,
                CompanyId = c.CompanyId,
                ProjectId = c.ProjectId,
                //ProjectNameYear = c.Project.Name + " " + c.Project.FinishDate.Year,
                Text = c.Text,
                Time = c.Time,
                Type = c.Type
            };
        }

        private static CompanyDTO createCompanyDTO(Company c)
        {
            return new CompanyDTO
            {
                Address = c.Address,
                City = c.City,
                CompanyId = c.CompanyId,
                //Contacts = c.Contacts.Any() ? createContactsDTO(c.Contacts) : null,
                Description = c.Description,
                Email = c.Email,
                Field = c.Field,
                Name = c.Name,
                Phone = c.Phone,
                Type = c.Type,
                Website = c.Website
            };
        }

        private static List<ContactPersonDTO> createContactsDTO(ICollection<ContactPerson> contacts)
        {
            List<ContactPersonDTO> cp = new List<ContactPersonDTO>();
            foreach (ContactPerson c in contacts)
            {
                cp.Add(crateContactDTO(c));
            }
            return cp;
        }

        private static ContactPersonDTO crateContactDTO(ContactPerson c)
        {
            return new ContactPersonDTO
            {
                ContactPersonId = c.ContactPersonId,
                Email = c.Email,
                Name = c.Name,
                Note = c.Note,
                Phone = c.Phone
            };
        }
    }
}