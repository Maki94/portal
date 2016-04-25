using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class AnecdoteMember
    {
        public int AnecdoteMemberId { get; set; }
        public int AnecdoteId { get; set; }
        public int MemberId { get; set; }

        public virtual Anecdote Anecdote { get; set; }
        public virtual Member Member { get; set; }
    }
}
