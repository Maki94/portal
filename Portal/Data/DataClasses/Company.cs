using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Enumerations.CompanyField Field { get; set; }
        public Enumerations.CompanyType Type { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ContactPerson> Contacts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
