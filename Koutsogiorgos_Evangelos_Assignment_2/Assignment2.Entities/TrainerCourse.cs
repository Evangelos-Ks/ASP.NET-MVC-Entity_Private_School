using System.ComponentModel.DataAnnotations;

namespace Assignment2.Entities
{
    public class TrainerCourse
    {
        //======================== Properties ================================================
        public int TrainerCourseId { get; set; }
        [Display(Name = "Trainers's name")]
        [Required]
        public int TrainerId { get; set; }
        [Display(Name = "Course title")]
        [Required]
        public int CourseId { get; set; }

        //======================== Navigation Properties ================================================
        public virtual Trainer Trainer { get; set; }
        public virtual Course Course { get; set; }
    }
}
