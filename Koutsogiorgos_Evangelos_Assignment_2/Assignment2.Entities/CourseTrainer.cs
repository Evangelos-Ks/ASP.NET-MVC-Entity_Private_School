using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Entities
{
    public class CourseTrainer
    {
        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int TrainerId { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Course Course { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
