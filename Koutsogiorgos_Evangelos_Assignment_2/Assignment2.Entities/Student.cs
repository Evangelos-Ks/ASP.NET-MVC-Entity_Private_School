using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Entities
{
    public class Student
    {
        //======================== Properties ================================================
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public DateTime Date { get; set; }
        //public string PhotoUrl { get; set; }

        //======================== Navigation Properties ================================================
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
