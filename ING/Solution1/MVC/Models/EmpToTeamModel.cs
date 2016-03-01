using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IsSelectedAttribute : ValidationAttribute
    {
        public IsSelectedAttribute(string errorMessage) : base(errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                if (value == null)
                {
                    validationResult = new ValidationResult(ErrorMessageString);
                }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }
    }
    public class EmpToTeamModel
    {
        [Required]
        public IEnumerable<int> Employees { get; set; }
        [IsSelected("Please select atleast one program.")]
        public IEnumerable<int> Teams { get; set; }

        public int? SelectedEmployee { get; set; }
        public int? SelectedTeam { get; set; }

        public static EmpToTeamModel Load ()
        {
            EmpToTeamModel model = new EmpToTeamModel();
            model.SelectedEmployee = null;
            model.SelectedTeam = null;
           

            return model;
        }
    }
}