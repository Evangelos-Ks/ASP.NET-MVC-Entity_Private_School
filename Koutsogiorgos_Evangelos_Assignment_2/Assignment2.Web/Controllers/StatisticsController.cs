using Assignment2.Database;
using Assignment2.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Assignment2.Web.Controllers
{
    public class StatisticsController : Controller
    {
        public ActionResult StatisticsIndex() 
        {
            return View();
        }

        public ActionResult Chart1()
        {
            StudentRepository studentRepository = new StudentRepository();
            int studentsCount = studentRepository.GetAll().Count();
            studentRepository.Dispose();

            AssignmentRepository assignmentRepository = new AssignmentRepository();
            int assignmentsCount = assignmentRepository.GetAll().Count();
            assignmentRepository.Dispose();

            TrainerRepository trainerRepository = new TrainerRepository();
            int trainersCount = trainerRepository.GetAll().Count();
            trainerRepository.Dispose();

            CourseRepository courseRepository = new CourseRepository();
            int coursesCount = courseRepository.GetAll().Count();
            courseRepository.Dispose();

            var chart = new Chart(width: 500, height: 300)
               .AddTitle("Count Students, Assignments, Trainers and Courses")
               .AddSeries(chartType: "column",
                  xValue: new[] { "Students", "Assignments", "Trainers", "Courses"},
                  yValues: new[] { studentsCount, assignmentsCount, trainersCount, coursesCount })
               .Write("png");

            return null;
        }
    }
}