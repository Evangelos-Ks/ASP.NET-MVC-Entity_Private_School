using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities.Custom_Validations
{
    public class GraterThanDate : ValidationAttribute
    {
        //public DateTime? StartDate { get; set; }

        //public GraterThanDate(DateTime? startDate)
        //{
        //    StartDate = startDate;
        //}

        public ValidationResult ValidateEndDateGreaterThanOtherStartDate(DateTime endDate, ValidationContext context)
        {
            var startDateField = typeof(DateTime).GetProperty("StartDate");
            //https://forums.asp.net/t/2088132.aspx?ASP+Net+MVC+Conditional+Validation+End+date+must+be+greater+than+or+equal+to+start+date
            //var propertyValue = context.ObjectType.GetProperty("StartDate");
            if (StartDate != null)
            {

                if (endDate > StartDate)
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
