using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Company
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }

        [DisplayName("Name ")]
        public string Name { get; set; }

        [DisplayName("Address ")]
        public string Address { get; set; }

        [DisplayName("City")]
        public string City { get; set; }
        
        [DisplayName("Field")]
        public Enumerations.CompanyField Field { get; set; }


        [DisplayName("Type")]
        public Enumerations.CompanyType Type { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Website")]
        public string Website { get; set; }


        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public List<ContactPersonDTO> Contacts { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
