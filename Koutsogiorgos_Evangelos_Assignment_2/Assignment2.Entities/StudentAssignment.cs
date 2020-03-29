using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Entities
{
    public class StudentAssignment
    {
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public int OralMark { get; set; }
        public int TotalMark { get; set; }
    }
}
