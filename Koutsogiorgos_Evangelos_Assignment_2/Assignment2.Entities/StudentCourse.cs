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
        [Key]
        [Column(Order = 1)]
        public int StudentId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }
        public decimal TuitionFees { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
