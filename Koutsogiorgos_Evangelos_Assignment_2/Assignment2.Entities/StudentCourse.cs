using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public int TuitionFees { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
