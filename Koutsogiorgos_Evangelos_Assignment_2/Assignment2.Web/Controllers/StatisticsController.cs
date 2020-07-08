using Assignment2.Database;
using Assignment2.Services;
using Assignment2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Web.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult StatisticsIndex()
        {
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel();
            
            StudentRepository studentRepository = new StudentRepository();
            statisticsViewModel.StudentsCount = studentRepository.GetAll().Count();
            studentRepository.Dispose();

            AssignmentRepository assignmentRepository = new AssignmentRepository();
            statisticsViewModel.AssignmentsCount = assignmentRepository.GetAll().Count();
            assignmentRepository.Dispose();

            TrainerRepository trainerRepository = new TrainerRepository();
            statisticsViewModel.TrainersCount = trainerRepository.GetAll().Count();
            trainerRepository.Dispose();

            CourseRepository courseRepository = new CourseRepository();
            statisticsViewModel.CoursesCount = courseRepository.GetAll().Count();
            courseRepository.Dispose();

            return View(statisticsViewModel);
        }

    }


}
