using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Entities
{
    public class Course
    {
        //======================== Properties ================================================
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //======================== Navigation Properties ================================================
        public virtual IEnumerable<Assignment> Assignments { get; set; }
        public virtual IEnumerable<CourseTrainer> CourseTrainers { get; set; }
        public virtual IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
