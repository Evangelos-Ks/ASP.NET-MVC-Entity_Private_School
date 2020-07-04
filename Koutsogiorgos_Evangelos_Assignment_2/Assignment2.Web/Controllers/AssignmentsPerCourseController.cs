using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Assignment2.Services;
using PagedList;

namespace Assignment2.Web.Controllers
{
    public class AssignmentsPerCourseController : Controller
    {
        // GET: AssignmentsPerCourse
        public ActionResult AllAssignmentsPerCourse(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            courseRepository.Dispose();

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
                courses = courses.Where(x => x.Title.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
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

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(courses.GroupBy(x => x.Title).ToPagedList(pageNumber, pSize));
        }
    }
}