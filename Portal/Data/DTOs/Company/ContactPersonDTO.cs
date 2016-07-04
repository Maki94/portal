using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Company
{
    public class ContactPersonDTO
    {
        public int ContactPersonId { get; set; }

        [DisplayName("Name ")]
        public string Name { get; set; }

        [DisplayName("Note ")]
        public string Note { get; set; }

        [DisplayName("Email ")]
        public string Email { get; set; }

        [DisplayName("Phone ")]
        public string Phone { get; set; }

        [DisplayName("Phone ")]
        public DateTime StartDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
