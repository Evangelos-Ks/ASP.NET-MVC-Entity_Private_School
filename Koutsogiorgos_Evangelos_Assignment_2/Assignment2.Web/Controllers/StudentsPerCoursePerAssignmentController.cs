using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class StudentsPerCoursePerAssignmentController : Controller
    {
        // GET: StudentsPerCoursePerAssignment
        public ActionResult AllStudentsPerCoursePerAssignment()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();

            return View(courses);
        }
    }
}