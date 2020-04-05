using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment2.Entities.Custom_Validations;

namespace Assignment2.Entities
{
    public class Course
    {
        //======================== Properties ================================================
        public int CourseId { get; set; }
        [Display(Name = "Course title")]
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "The title needs at least 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        [Display(Name = "Start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        //======================== Navigation Properties ================================================
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<TrainerCourse> TrainerCourses { get; set; }


    }
}
