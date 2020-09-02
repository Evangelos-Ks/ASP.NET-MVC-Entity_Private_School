﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Web.Models
{
    public class StudentAssignmentViewModel
    {
        public int StudentAssignmentId { get; set; }
        public int? OralMark { get; set; }
        public int? WritingMark { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> StudentAssignmentIds { get; set; }
        public List<int> OralMarks { get; set; }
        public List<int> WritingMarks { get; set; }
    }
}