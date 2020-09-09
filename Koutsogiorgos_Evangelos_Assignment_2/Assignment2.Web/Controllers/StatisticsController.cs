using Assignment2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var chart = new Chart(width: 550, height: 300)
               .AddTitle("Count Students, Assignments, Trainers and Courses")
               .AddSeries(chartType: "column",
                  xValue: new[] { "Students", "Assignments", "Trainers", "Courses" },
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

            var chart = new Chart(width: 550, height: 300)
               .AddTitle("Students per course")
               .AddSeries(chartType: "column",
                  xValue: courseTitles,
                  yValues: numberOfstudentsPerCourse)
               .Write("png");

            return null;
        }

        //=============================================== Trainers per course ==============================================
        public ActionResult Chart3()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll().ToList();
            int coursesCount = courses.Count();
            courseRepository.Dispose();

            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainerCourses = trainerCourseRepository.GetAll().ToList();
            trainerCourseRepository.Dispose();

            string[] courseTitles = new string[coursesCount];
            int[] numberOfTrainersPerCourse = new int[coursesCount];
            for (int i = 0; i < coursesCount; i++)
            {
                courseTitles[i] = courses[i].Title;
                numberOfTrainersPerCourse[i] = trainerCourses.FindAll(x => x.CourseId == courses[i].CourseId).Count();
            }

            var chart = new Chart(width: 550, height: 300)
               .AddTitle("Trainers per course")
               .AddSeries(chartType: "column",
                  xValue: courseTitles,
                  yValues: numberOfTrainersPerCourse)
               .Write("png");

            return null;
        }

        //=============================================== Assignments per course ===========================================
        public ActionResult Chart4()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll().ToList();
            int coursesCount = courses.Count();
            courseRepository.Dispose();

            AssignmentRepository assignmentRepository = new AssignmentRepository();
            var assignments = assignmentRepository.GetAll().ToList();
            assignmentRepository.Dispose();

            string[] courseTitles = new string[coursesCount];
            int[] numberOfAssignmentsPerCourse = new int[coursesCount];
            for (int i = 0; i < coursesCount; i++)
            {
                courseTitles[i] = courses[i].Title;
                numberOfAssignmentsPerCourse[i] = assignments.FindAll(x => x.CourseId == courses[i].CourseId).Count();
            }

            var chart = new Chart(width: 550, height: 300)
               .AddTitle("Assignments per course")
               .AddSeries(chartType: "column",
                  xValue: courseTitles,
                  yValues: numberOfAssignmentsPerCourse)
               .Write("png");

            return null;
        }

        //=============================================== Average mark of students per course ==============================
        public ActionResult Chart5()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll().ToList();
            int coursesCount = courses.Count();
            courseRepository.Dispose();

            AssignmentRepository assignmentRepository = new AssignmentRepository();
            var assignments = assignmentRepository.GetAll().ToList();
            assignmentRepository.Dispose();

            StudentAssignmentRepository studentAssignmentRepository = new StudentAssignmentRepository();
            var studentsPerAssignment = studentAssignmentRepository.GetAll().ToList();
            studentAssignmentRepository.Dispose();

            string[] courseTitles = new string[coursesCount];
            int[] avgMarksPerCourse = new int[coursesCount];
            for (int i = 0; i < coursesCount; i++)
            {
                courseTitles[i] = courses[i].Title;

                List<int> assignmentsId = assignments.FindAll(x => x.CourseId == courses[i].CourseId).Select(y => y.AssignmentId).ToList();
                double? avgMarkPerCourseCalculate = 0;
                for (int j = 0; j < assignmentsId.Count(); j++)
                {
                    avgMarkPerCourseCalculate = avgMarkPerCourseCalculate + studentsPerAssignment.FindAll(a => a.AssignmentId == assignmentsId[j]).Average(b => b.TotalMark);
                }

                avgMarksPerCourse[i] = (int)Math.Round((double)avgMarkPerCourseCalculate / assignmentsId.Count(), MidpointRounding.AwayFromZero);
            }

            var chart = new Chart(width: 550, height: 300)
               .AddTitle("Average mark of students per course")
               .AddSeries(chartType: "column",
                  xValue: courseTitles,
                  yValues: avgMarksPerCourse)
               .SetYAxis(null, 0)
               .Write("png");

            return null;
        }
    }
}