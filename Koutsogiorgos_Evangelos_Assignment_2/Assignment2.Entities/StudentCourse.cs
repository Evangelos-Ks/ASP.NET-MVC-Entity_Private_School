using System.ComponentModel.DataAnnotations;


namespace Assignment2.Entities
{
    public class StudentCourse
    {
        //======================== Properties ================================================
        public int StudentCourseId { get; set; }
        [Display(Name = "Student's name")]
        [Required]
        public int StudentId { get; set; }
        [Display(Name = "Course title")]
        [Required]
        public int CourseId { get; set; }
        public int? TuitionFees { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
