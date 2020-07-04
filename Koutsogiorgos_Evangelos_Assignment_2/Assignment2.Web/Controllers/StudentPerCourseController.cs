using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using PagedList;

namespace Assignment2.Web.Controllers
{
    public class StudentPerCourseController : Controller
    {
        // GET: StudentCourse
        public ActionResult AllStudentCourses(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentCourses = studentCourseRepository.GetAll();
            studentCourseRepository.Dispose();

            //============================================== Paging ========================================================
            if (!string.IsNullOrWhiteSpace(search) || search == "")
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            int pSize = pageSize ?? 3;
            int pageNumber = page ?? 1;

            ViewBag.PageSize = new List<SelectListItem>()
            {
             new SelectListItem() { Value="3", Text= "3" },
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" }
            };

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

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(studentCourses.GroupBy(x => x.Course.Title).ToPagedList(pageNumber, pSize));
        }
    }
}