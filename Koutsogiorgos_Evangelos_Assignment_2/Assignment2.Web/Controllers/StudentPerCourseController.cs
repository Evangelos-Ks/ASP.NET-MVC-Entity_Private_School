using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class StudentPerCourseController : Controller
    {
        // GET: StudentCourse
        public ActionResult AllStudentCourses(string sort, string search)
        {
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentCourses = studentCourseRepository.GetAll();
            studentCourseRepository.Dispose();

            //============================================== searching =====================================================
            if (!string.IsNullOrEmpty(search))
            {
                studentCourses = studentCourses.Where(x => x.Course.Title.ToUpper().Contains(search.ToUpper()) ||
                 x.Student.FirstName.ToUpper().Contains(search.ToUpper()) || x.Student.LastName.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
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