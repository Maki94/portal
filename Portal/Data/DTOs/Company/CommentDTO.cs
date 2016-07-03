using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Company
{
    public class CommentDTO
    {
        public int CommentId { get; set; }

        [DisplayName("Type ")]
        public string Type { get; set; }
        [DisplayName("Comment ")]
        public string Text { get; set; }
        [DisplayName("Time ")]
        public DateTime Time { get; set; }

        public bool IsDeleted { get; set; }
        
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        
        public int ProjectId { get; set; }
        public string ProjectNameYear { get; set; }
    }
}
