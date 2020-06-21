using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class StudentsPerCoursePerAssignmentController : Controller
    {
        // GET: StudentsPerCoursePerAssignment
        public ActionResult AllStudentsPerCoursePerAssignment(string sort)
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            courseRepository.Dispose();

            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";

            switch (sort)
            {
                case "courseTitleDesc":
                    courses = courses.OrderByDescending(x => x.Title);
                    break;
                default:
                    courses = courses.OrderBy(x => x.Title);
                    break;
            }
            return View(courses);
        }
    }
}