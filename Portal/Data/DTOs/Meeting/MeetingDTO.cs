using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Meeting
{
    class MeetingDTO
    {
        public int MeetingId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Agenda { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        
        public int HRId { get; set; }
        public virtual MemberDTO HR { get; set; }
        
        public int MeetingCreatorId { get; set; }
        public virtual MemberDTO MeetingCreator { get; set; }
    }
}
