﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Anecdote
    {
        public int AnecdoteId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Member Author { get; set; }

        [ForeignKey("HR")]
        public int HRId { get; set; }
        public virtual Member HR { get; set; }
    }
}
