using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class AddFileDTO
    {
        public int statusID { get; set; }
        public int employeeID { get; set; }
        [DisplayName("File name")]
        public  string FileName { get; set; }
        public  byte[] File { get; set; }
    }

    public class  AddFile
    {
        public static AddFileDTO CreateDTO(int eid)
        {
            AddFileDTO create = new AddFileDTO();
            create.employeeID = eid;
            return create;
        }
    }
}
