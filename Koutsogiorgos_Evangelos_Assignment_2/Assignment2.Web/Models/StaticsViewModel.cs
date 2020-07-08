using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Web.Models
{
    public class StaticsViewModel
    {
        //====================================================== Properties ================================================
        protected int StudentsCount { get; set; }
        protected int AssignmentsCount { get; set; }
        protected int TrainersCount { get; set; }
        protected int CoursesCount { get; set; }
        protected int StudentsPerCourseCount { get; set; }
        protected int TrainersPerCourseCount { get; set; }
        protected int AssignmentsPerCourseCount { get; set; }
        protected int StudentsMarksAveragePerCourse { get; set; }
        protected int StudentsMarksAveragePerAssignment { get; set; }
        protected int StudentsMarksAveragePerAssignmentPerCourse { get; set; }
    }
}