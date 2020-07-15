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
        public StudentAssignment(int? OralMark, int? WhritingMark)
        {
            this.OralMark = OralMark;
            this.WhritingMark = WhritingMark;

            if (!string.IsNullOrWhiteSpace(OralMark.ToString()) || !string.IsNullOrWhiteSpace(WhritingMark.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(OralMark.ToString()) && !string.IsNullOrWhiteSpace(WhritingMark.ToString()))
                {
                    TotalMark = (int)Math.Round(((double)(OralMark + WhritingMark) / 2.0d));
                }
                else if (!string.IsNullOrWhiteSpace(WhritingMark.ToString()))
                {
                    TotalMark = WhritingMark;
                }
                else if (!string.IsNullOrWhiteSpace(OralMark.ToString()))
                {
                    TotalMark = OralMark;
                }
            }
        }

        //======================== Navigation Properties ===================================================================
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}
