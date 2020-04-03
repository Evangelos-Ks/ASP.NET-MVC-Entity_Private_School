using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities
{
    public class Assignment
    {
        //======================== Properties ================================================
        public int AssignmentId { get; set; }
        [Display(Name ="Assignment title")]
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage ="The title needs at least 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Submission date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SubDateTime { get; set; }

        //======================== Navigation Properties ================================================
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<StudentAssignment> StudentAssignments { get; set; }


    }
}
