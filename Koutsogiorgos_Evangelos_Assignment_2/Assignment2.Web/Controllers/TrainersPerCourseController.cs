using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class TrainersPerCourseController : Controller
    {
        // GET: TrainersPerCourse
        public ActionResult AllTrainerCourse(string sort, string search)
        {
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainerCourse = trainerCourseRepository.GetAll();
            trainerCourseRepository.Dispose();

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
            return View(trainerCourse);
        }
    }
}