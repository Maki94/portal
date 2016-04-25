using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Anecdote
    {
        public int AnecdoteId { get; set; }
        public string Description { get; set; }

        public virtual Member Author { get; set; }
        public virtual Member HR { get; set; }
    }
}
