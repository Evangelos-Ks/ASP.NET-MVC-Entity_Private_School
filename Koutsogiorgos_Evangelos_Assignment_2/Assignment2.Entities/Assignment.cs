using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Entities
{
    public class Assignment
    {
        //======================== Properties ================================================
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubDateTime { get; set; }

        //======================== Navigation Properties ================================================
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }


    }
}
