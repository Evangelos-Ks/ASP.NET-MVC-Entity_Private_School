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
        public Student StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public decimal TuitionFees { get; set; }

    }
}
