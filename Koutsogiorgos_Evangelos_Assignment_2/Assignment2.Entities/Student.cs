using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities
{
    public class Student
    {
        //======================== Properties ================================================
        public int StudentId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [MinLength(3, ErrorMessage = "The minimum length is 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [ MinLength(3, ErrorMessage = "The minimum length is 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string LastName { get; set; }
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Student photo")]
        public string PhotoUrl { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive values are acceptable")]
        public int Discount { get; set; }

        //======================== Navigation Properties ================================================
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
