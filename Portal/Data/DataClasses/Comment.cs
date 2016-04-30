using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Member Author { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
