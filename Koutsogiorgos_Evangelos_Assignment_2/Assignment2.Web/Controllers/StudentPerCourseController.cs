﻿using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class StudentPerCourseController : Controller
    {
        // GET: StudentCourse
        public ActionResult AllStudentCourses()
        {
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentCourses = studentCourseRepository.GetAll();

            //List<Student> students = new List<Student>();
            //foreach (var studentcourse in studentCourses)
            //{
            //    students.Add(studentcourse.Student);
            //}
            //ViewBag.Students = students;
            //studentCourseRepository.Dispose();

            return View(studentCourses);
        }
    }
}