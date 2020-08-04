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
        public static ValidationResult ValidateEndDateGreaterThanStartDate(DateTime? endDate, ValidationContext context)
        {
            var startDateField = context.ObjectType.GetProperty("StartDate");
            var startDateValue = startDateField.GetValue(context.ObjectInstance);

            if (startDateValue != null && endDate != null)
            {
                if (endDate > (DateTime)startDateValue)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The end date can't be earlier than start date.");
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
