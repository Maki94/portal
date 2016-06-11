using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class ContactPerson
    {
        public int ContactPersonId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
