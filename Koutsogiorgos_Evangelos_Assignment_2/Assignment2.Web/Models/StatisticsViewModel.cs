using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Assignment2.Web.Models
{
    public class StatisticsViewModel
    {
        //====================================================== Properties ================================================
        public int StudentsCount { get; set; }
        public int AssignmentsCount { get; set; }
        public int TrainersCount { get; set; }
        public int CoursesCount { get; set; }
        public int StudentsPerCourseCount { get; set; }
        public int TrainersPerCourseCount { get; set; }
        public int AssignmentsPerCourseCount { get; set; }
        public int StudentsMarksAveragePerCourse { get; set; }
        public int StudentsMarksAveragePerAssignment { get; set; }
        public int StudentsMarksAveragePerAssignmentPerCourse { get; set; }
    }
}