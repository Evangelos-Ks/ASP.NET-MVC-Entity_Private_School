using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities
{
    public class StudentAssignment
    {
        //======================== Properties ==============================================================================
        public int StudentAssignmentId { get; set; }
        [Display(Name = "Student's name")]
        [Required]
        public int StudentId { get; set; }
        [Display(Name = "Assignment")]
        [Required]
        public int AssignmentId { get; set; }
        public int? OralMark { get; set; }
        public int? WhritingMark { get; set; }
        public int? TotalMark { get; set; }

        //======================== Constractor =============================================================================
        public StudentAssignment() { }

        public StudentAssignment(int? OralMark, int? WhritingMark)
        {
            this.OralMark = OralMark;
            this.WhritingMark = WhritingMark;
            TotalMark = CalculateTotalMark(OralMark, WhritingMark);
        }

        //======================== Navigation Properties ===================================================================
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }

        //======================== Methods =================================================================================
        public int? CalculateTotalMark(int? oralMark, int? whritingMark)
        {
            if (!string.IsNullOrWhiteSpace(oralMark.ToString()) || !string.IsNullOrWhiteSpace(whritingMark.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(oralMark.ToString()) && !string.IsNullOrWhiteSpace(whritingMark.ToString()))
                {
                   return (int)Math.Round(((double)(oralMark + whritingMark) / 2.0d), MidpointRounding.AwayFromZero);
                }
                else if (!string.IsNullOrWhiteSpace(whritingMark.ToString()))
                {
                    return whritingMark;
                }
                else if (!string.IsNullOrWhiteSpace(oralMark.ToString()))
                {
                    return oralMark;
                }
            }
            
            return null;
        }

    }
}
