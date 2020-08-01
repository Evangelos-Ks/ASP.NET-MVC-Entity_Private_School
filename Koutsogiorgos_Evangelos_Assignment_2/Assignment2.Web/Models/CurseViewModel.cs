using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Assignment2.Web.Models
{
    public class CurseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
    }
}