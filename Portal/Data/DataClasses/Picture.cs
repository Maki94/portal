using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
