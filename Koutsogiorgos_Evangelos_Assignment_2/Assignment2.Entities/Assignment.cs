using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Entities
{
    public class Assignment
    {
        //======================== Properties ================================================
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubDateTime { get; set; }
        public int OralMark { get; set; }
        public int TotalMark { get; set; }


    }
}
