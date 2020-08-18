using Assignment2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Web.Models
{
    public class AssignmentViewModel
    {
        //======================== Properties ==============================================================================
        public int AssignmentId { get; set; }
        [Display(Name = "Assignment title")]
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "The title needs at least 3 characters"), MaxLength(30, ErrorMessage = "The maximum length is 30 characters")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Submission date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SubDateTime { get; set; }
        [Display(Name ="Courses")]
        public IEnumerable<SelectListItem> Courses { get; set; }
        public int? CourseId { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public List<int> StudentsId { get; set; }
        public bool FinalSubmit { get; set; }
    }
}