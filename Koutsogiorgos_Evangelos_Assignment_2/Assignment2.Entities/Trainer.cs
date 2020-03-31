using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Entities
{
    public class Trainer
    {
        //======================== Properties ================================================
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        //======================== Navigation Properties ================================================
        public virtual ICollection<Course> Courses { get; set; }

    }
}
