using System.Web.Mvc;
using Assignment2.Services;

namespace Assignment2.Web.Controllers
{
    public class TrainersPerCourseController : Controller
    {
        // GET: TrainersPerCourse
        public ActionResult AllTrainerCourse()
        {
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainerCourse = trainerCourseRepository.GetAll();

            return View(trainerCourse);
        }
    }
}