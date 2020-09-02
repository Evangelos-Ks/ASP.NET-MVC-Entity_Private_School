using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;

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
        [Range(minimum:0, maximum:100, ErrorMessage = "The range of the mark is 0-100")]
        public int? OralMark { get; set; }
        [Range(minimum: 0, maximum: 100, ErrorMessage = "The range of the mark is 0-100")]
        public int? WritingMark { get; set; }
        public int? TotalMark { get; set; }

        //======================== Constractor =============================================================================
        public StudentAssignment() { }

        public StudentAssignment(int? OralMark, int? WritingMark)
        {
            this.OralMark = OralMark;
            this.WritingMark = WritingMark;
            TotalMark = CalculateTotalMark(OralMark, WritingMark);
        }

        //======================== Navigation Properties ===================================================================
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }

        //======================== Methods =================================================================================
        public int? CalculateTotalMark(int? oralMark, int? WritingMark)
        {
            if (!string.IsNullOrWhiteSpace(oralMark.ToString()) || !string.IsNullOrWhiteSpace(WritingMark.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(oralMark.ToString()) && !string.IsNullOrWhiteSpace(WritingMark.ToString()))
                {
                   return (int)Math.Round(((double)(oralMark + WritingMark) / 2.0d), MidpointRounding.AwayFromZero);
                }
                else if (!string.IsNullOrWhiteSpace(WritingMark.ToString()))
                {
                    return WritingMark;
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
