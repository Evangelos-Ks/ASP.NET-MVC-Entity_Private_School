using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Mvc;
using Assignment2.Services;
using PagedList;

namespace Assignment2.Web.Controllers
{
    public class TrainersPerCourseController : Controller
    {
        // GET: TrainersPerCourse
        public ActionResult AllTrainerCourse(string sort, string search, string currentFilter, int? page, int? pageSize, int? currentPageSize)
        {
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainerCourse = trainerCourseRepository.GetAll();
            trainerCourseRepository.Dispose();

            //============================================== Paging ========================================================
            int pSize;

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            if (currentPageSize == null)
            {
                pSize = pageSize ?? 3;
            }
            else
            {
                pSize = pageSize ?? (int)currentPageSize;
            }

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
                trainerCourse = trainerCourse.Where(x => x.Course.Title.ToUpper().Contains(search.ToUpper()) ||
                x.Trainer.FirstName.ToUpper().Contains(search.ToUpper()) || x.Trainer.LastName.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";

            switch (sort)
            {
                case "courseTitleDesc":
                    trainerCourse = trainerCourse.OrderByDescending(x => x.Course.Title);
                    break;
                default:
                    trainerCourse = trainerCourse.OrderBy(x => x.Course.Title);
                    break;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(trainerCourse.GroupBy(x => x.Course).ToPagedList(pageNumber, pSize));
        }
    }
}