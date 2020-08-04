using Assignment2.Entities.Custom_Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment2.Web.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Display(Name = "Course title")]
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "The title needs at least 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        [Display(Name = "Start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(MyValidationMethods), "ValidateEndDateGreaterThanStartDate")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Fees")]
        [Range(0, int.MaxValue, ErrorMessage = "The fees must be greater or equal to 0.")]
        public int? TuitionFees { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public List<string> StudentsId { get; set; }
        public IEnumerable<SelectListItem> Trainers { get; set; }
        public List<string> TrainersId { get; set; }
    }
}