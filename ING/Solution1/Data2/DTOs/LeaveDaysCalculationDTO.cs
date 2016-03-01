using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.DTOs
{
    public class LeaveDaysCalculationDTO
    {
        public int Total { get; set; }
        public int Kids { get; set; }
        public int TwoYearBonus { get; set; }
        public int Bonuses { get; set; }
        public int Potencial { get; set; }
        public int LastYearLeaveDays { get; set; }
        public int FutureUsedLeaveDays { get; set; }
        public int Expired { get; set; }
    }
}
