using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Assignment2.Services;
using Assignment2.Entities;

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