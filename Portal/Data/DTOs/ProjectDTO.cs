using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public Enumerations.ProjectState State { get; set; }
        public byte[] Report { get; set; }
        public byte[] Logo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public byte[] FlyerImage { get; set; }
        public byte[] Newsletter { get; set; }
        public byte[] Offer { get; set; }
    }
}
