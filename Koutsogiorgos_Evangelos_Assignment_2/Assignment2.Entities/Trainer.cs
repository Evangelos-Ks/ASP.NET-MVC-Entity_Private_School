using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities
{
    public class Trainer
    {
        //======================== Properties ================================================
        public int TrainerId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [MinLength(3, ErrorMessage = "The minimum length is 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [MinLength(3, ErrorMessage = "The minimum length is 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string LastName { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Trainer photo")]
        public string PhotoUrl { get; set; }

        //======================== Navigation Properties ================================================
        public virtual ICollection<Course> Courses { get; set; }

    }
}
