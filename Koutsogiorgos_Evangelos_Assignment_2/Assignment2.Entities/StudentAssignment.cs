using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Entities
{
    public class StudentAssignment
    {
        //======================== Properties ================================================
        [Key]
        [Column(Order = 2)]
        public int StudentId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int AssignmentId { get; set; }
        public int OralMark { get; set; }
        public int TotalMark { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }



    }
}
