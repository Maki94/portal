﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataClasses
{
    public class DefaultPicture
    {
        public int DefaultPictureId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
