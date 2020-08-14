using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Web.Models
{
    public class TrainerViewModel
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
        [Display(Name = "Trainer photo")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Courses")]
        public IEnumerable<SelectListItem> Courses { get; set; }
        public List<int> CoursesId { get; set; }
        public IEnumerable<SelectListItem> ExistingCourses { get; set; }
        public List<int> ExistingCoursesCoursesId { get; set; }
    }
}