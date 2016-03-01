using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Data2;
using Data2.DTOs;


namespace MVC.Models
{
    public class LeaveDaysCalculationModel
    {
      
        public LeaveDaysCalculationDTO Days { get; set; }
             
        public int? newBonusNum { get; set; }
        public string newBonusComment { get; set; }
        public List<BonusDTO> BonusesList { get; set; }

  
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}" , ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int EmployeeID { get; set; }

        public static LeaveDaysCalculationModel Load(int empID, DateTime? when = null)
        {
            LeaveDaysCalculationModel model = new LeaveDaysCalculationModel();
            if (when == null)
                model.Date = DateTime.Today;
            else
                model.Date = (DateTime)when;
        
            model.Days = Data2.LeaveDaysCalculation.LeaveDaysNumber(empID, model.Date);
          
            model.EmployeeID = empID;

            model.newBonusNum = null;
            model.newBonusComment = null;
            model.BonusesList = new List<BonusDTO>();
            List<BonusDaysHistory> listBDH = Employees.getAllEmployeeBonusDays(model.EmployeeID);
            foreach (var a in listBDH)
            {
                model.BonusesList.Add(CreateBonusDTO(a));
            }
            return model;
        }

        public static BonusDTO CreateBonusDTO(BonusDaysHistory bonus)
        {
            return new BonusDTO()
            {
                BonusNumber = bonus.bunusCount,
                SubmitterName = Employees.getEmployeeFullName(bonus.submitterID),
                Date = bonus.date,
                Comment = bonus.comment
            };
        }
    }
}