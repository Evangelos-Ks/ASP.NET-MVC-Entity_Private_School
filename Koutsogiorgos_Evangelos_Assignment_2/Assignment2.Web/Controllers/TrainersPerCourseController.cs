using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;

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