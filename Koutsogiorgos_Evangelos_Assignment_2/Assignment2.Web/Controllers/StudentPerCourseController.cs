using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class StudentPerCourseController : Controller
    {
        // GET: StudentCourse
        public ActionResult AllStudentCourses(string sort)
        {
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentCourses = studentCourseRepository.GetAll();
            studentCourseRepository.Dispose();

            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";

            switch (sort)
            {
                case "courseTitleDesc":
                    studentCourses = studentCourses.OrderByDescending(x => x.Course.Title);
                    break;
                default:
                    studentCourses = studentCourses.OrderBy(x => x.Course.Title);
                    break;
            }

            return View(studentCourses);
        }
    }
}