using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities.Custom_Validations
{
    public class MyValidationMethods : ValidationAttribute
    {
        public static ValidationResult ValidateEndDateGreaterThanOtherStartDate(DateTime endDate, DateTime startDate, ValidationContext context)
        {
            if (endDate > startDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The end date can't be earlier than start date.");
            }

        }
    }
}
