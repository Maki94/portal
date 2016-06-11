using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public Enumerations.FeedbackType Type { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("SentBy")]
        public int SentById { get; set; }
        public virtual Member SentBy { get; set; }
    }
}
