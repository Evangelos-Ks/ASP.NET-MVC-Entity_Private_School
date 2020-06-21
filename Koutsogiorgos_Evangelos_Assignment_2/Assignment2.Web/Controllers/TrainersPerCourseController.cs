using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class TrainersPerCourseController : Controller
    {
        // GET: TrainersPerCourse
        public ActionResult AllTrainerCourse(string sort)
        {
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainerCourse = trainerCourseRepository.GetAll();
            trainerCourseRepository.Dispose();

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