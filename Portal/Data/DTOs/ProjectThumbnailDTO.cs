using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProjectThumbnailDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public Enumerations.ProjectState State { get; set; }
    }
}
