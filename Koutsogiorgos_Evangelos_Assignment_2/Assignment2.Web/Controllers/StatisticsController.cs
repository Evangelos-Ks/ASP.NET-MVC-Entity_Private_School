using Assignment2.Database;
using Assignment2.Entities;
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

        //=============================================== Count Students, Assignments, Trainers and Courses ================
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

        //=============================================== Students per course ==============================================
        public ActionResult Chart2()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll().ToList();
            int coursesCount = courses.Count();
            courseRepository.Dispose();

            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentCourses = studentCourseRepository.GetAll().ToList();
            studentCourseRepository.Dispose();

            string[] courseTitles = new string[coursesCount];
            int[] numberOfstudentsPerCourse = new int[coursesCount];
            for (int i = 0; i < coursesCount; i++)
            {
                courseTitles[i] = courses[i].Title;
                numberOfstudentsPerCourse[i] = studentCourses.FindAll(x => x.CourseId == courses[i].CourseId).Count();
            }

            var chart = new Chart(width: 500, height: 300)
               .AddTitle("Students per course")
               .AddSeries(chartType: "column",
                  xValue: courseTitles,
                  yValues: numberOfstudentsPerCourse)
               .Write("png");

            return null;
        }
    }
}