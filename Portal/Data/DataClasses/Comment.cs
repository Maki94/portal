using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }

        public virtual Member Author { get; set; }
        public virtual Company Company { get; set; }
        public virtual Project Project { get; set; }
    }
}
