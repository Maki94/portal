using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class RequestDTO
    {
        public bool ErrorOccurred { get; set; }
        public string Error { get; set; }
        public int LeaveDuration { get; set; }


    }
}
