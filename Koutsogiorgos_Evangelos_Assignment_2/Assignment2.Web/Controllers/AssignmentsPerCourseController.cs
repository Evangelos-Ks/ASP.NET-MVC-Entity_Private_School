using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class AssignmentsPerCourseController : Controller
    {
        // GET: AssignmentsPerCourse
        public ActionResult AllAssignmentsPerCourse()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            //courseRepository.Dispose();

            return View(courses);
        }
    }
}