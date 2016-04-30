using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class AnecdoteMember
    {
        public int AnecdoteMemberId { get; set; }

        [ForeignKey("Anecdote")]
        public int AnecdoteId { get; set; }
        public virtual Anecdote Anecdote { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
