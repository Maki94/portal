using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemberThumbnailDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Faculty { get; set; }
        public byte[] Avatar { get; set; }
    }
}
