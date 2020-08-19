using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using PagedList;
using Assignment2.Web.Models;
using System.IO;
using System;

namespace Assignment2.Web.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult AllTrainers(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            TrainerRepository trainerRepository = new TrainerRepository();
            var trainers = trainerRepository.GetAll();
            trainerRepository.Dispose();

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
                trainers = trainers.Where(x => x.FirstName.ToUpper().Contains(search.ToUpper()) ||
                x.LastName.ToUpper().Contains(search.ToUpper()) ||
                x.Subject.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.FirstName = string.IsNullOrEmpty(sort) ? "firstNameDesc" : "";
            ViewBag.LastName = sort == "lastNameAsc" ? "lastNameDesc" : "lastNameAsc";
            ViewBag.Subject = sort == "subjectAsc" ? "subjectDesc" : "subjectAsc";


            switch (sort)
            {
                case "firstNameDesc":
                    trainers = trainers.OrderByDescending(x => x.FirstName);
                    break;
                case "lastNameAsc":
                    trainers = trainers.OrderBy(x => x.LastName);
                    break;
                case "lastNameDesc":
                    trainers = trainers.OrderByDescending(x => x.LastName);
                    break;
                case "subjectAsc":
                    trainers = trainers.OrderBy(x => x.Subject);
                    break;
                case "subjectDesc":
                    trainers = trainers.OrderByDescending(x => x.Subject);
                    break;
                default:
                    trainers = trainers.OrderBy(x => x.FirstName);
                    break;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(trainers.ToPagedList(pageNumber, pSize));
        }

        // GET: TestTrainer/Details/5
        public ActionResult DetailsTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            trainerRepository.Dispose();

            return View(trainer);
        }

        // GET: TestTrainer/Edit/5
        public ActionResult EditTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Dispose();

            if (trainer == null)
            {
                return HttpNotFound();
            }

            //Get all Courses
            CourseRepository courseRepository = new CourseRepository();
            List<Course> courses = courseRepository.GetAll().ToList();
            courseRepository.Dispose();

            //Get CourseId where trainerId is the id of the trainer that we want to edit
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            IEnumerable<int> coursesId = trainerCourseRepository.GetAll()
                                                        .Where(tc => tc.TrainerId == id)
                                                        .Select(tc => tc.CourseId);
            trainerCourseRepository.Dispose();

            //Create a list with courses which have assigned to the trainer
            List<Course> existingCourses = new List<Course>();
            foreach (int courseId in coursesId)
            {
                Course course = courses.FirstOrDefault(c => c.CourseId == courseId);
                existingCourses.Add(course);
                courses.Remove(course);
            }

            //Create trainerViewModel
            TrainerViewModel trainerViewModel = new TrainerViewModel()
            {
                TrainerId = trainer.TrainerId,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Subject = trainer.Subject,
                PhotoUrl = trainer.PhotoUrl,
                ExistingCourses = Methods.CreateSelectListOfCourses(existingCourses),
                Courses = Methods.CreateSelectListOfCourses(courses)
            };

            return View(trainerViewModel);
        }

        // POST: TestTrainer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainer([Bind(Include = "TrainerId,FirstName,LastName,Subject,PhotoUrl,ImageFile,ExistingCoursesCoursesId,CoursesId")] TrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Save upload file
                if (trainerViewModel.ImageFile != null)
                {
                    string extention = Path.GetExtension(trainerViewModel.ImageFile.FileName);
                    string fileName = trainerViewModel.FirstName + trainerViewModel.LastName + DateTime.Now.ToString("yyyyMMddmmss") + extention;
                    trainerViewModel.PhotoUrl = "../../Content/Trainers_Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Trainers_Images/"), fileName);
                    trainerViewModel.ImageFile.SaveAs(fileName);
                }

                //Update trainerCourse
                if (trainerViewModel.CoursesId != null || trainerViewModel.ExistingCoursesCoursesId != null)
                {
                    TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
                    //Delete trainerCourse
                    if (trainerViewModel.ExistingCoursesCoursesId != null)
                    {
                        List<TrainerCourse> trainerCourses = trainerCourseRepository.GetAll()
                                                            .Where(tc => tc.TrainerId == trainerViewModel.TrainerId).ToList();
                        foreach (int id in trainerViewModel.ExistingCoursesCoursesId)
                        {
                            TrainerCourse trainerCourse = trainerCourses.FirstOrDefault(tc => tc.CourseId == id);
                            trainerCourseRepository.Delete(trainerCourse);
                        }
                    }
                    if (trainerViewModel.CoursesId != null)
                    {
                        //Insert trainerCourse
                        foreach (int id in trainerViewModel.CoursesId)
                        {
                            TrainerCourse trainerCourse = new TrainerCourse()
                            {
                                TrainerId = trainerViewModel.TrainerId,
                                CourseId = id
                            };
                            trainerCourseRepository.Insert(trainerCourse);
                        }
                    }

                    trainerCourseRepository.Dispose();
                }

                //Create trainer and update
                Trainer trainer = new Trainer()
                {
                    TrainerId = trainerViewModel.TrainerId,
                    FirstName = trainerViewModel.FirstName,
                    LastName = trainerViewModel.LastName,
                    PhotoUrl = trainerViewModel.PhotoUrl,
                    Subject = trainerViewModel.Subject
                };

                TrainerRepository trainerRepository = new TrainerRepository();
                trainerRepository.Update(trainer);
                trainerRepository.Dispose();

                return RedirectToAction("AllTrainers");
            }
            return View(trainerViewModel);
        }

        // GET: TestTrainer/Create
        public ActionResult CreateTrainer()
        {
            //Get all courses
            CourseRepository courseRepository = new CourseRepository();
            IEnumerable<Course> allCourses = courseRepository.GetAll();
            courseRepository.Dispose();

            //Create TrainerViewModel
            TrainerViewModel trainerViewModel = new TrainerViewModel()
            {
                Courses = Methods.CreateSelectListOfCourses(allCourses)
            };

            return View(trainerViewModel);
        }

        // POST: TestTrainer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainer([Bind(Include = "TrainerId,FirstName,LastName,Subject,ImageFile,CoursesId")] TrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Save upload file
                if (trainerViewModel.ImageFile != null)
                {
                    string extention = Path.GetExtension(trainerViewModel.ImageFile.FileName);
                    string fileName = trainerViewModel.FirstName + trainerViewModel.LastName + DateTime.Now.ToString("yyyyMMddmmss") + extention;
                    trainerViewModel.PhotoUrl = "../../Content/Trainers_Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Trainers_Images/"), fileName);
                    trainerViewModel.ImageFile.SaveAs(fileName);
                }

                //Create Trainer
                Trainer trainer = new Trainer()
                {
                    TrainerId = trainerViewModel.TrainerId,
                    FirstName = trainerViewModel.FirstName,
                    LastName = trainerViewModel.LastName,
                    Subject = trainerViewModel.Subject,
                    PhotoUrl = trainerViewModel.PhotoUrl
                };

                //Add trainer
                TrainerRepository trainerRepository = new TrainerRepository();
                trainerRepository.Insert(trainer);
                trainerRepository.Dispose();

                //Create and add trainerCourse
                if (trainerViewModel.CoursesId != null)
                {
                    TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
                    foreach (int courseId in trainerViewModel.CoursesId)
                    {
                        TrainerCourse trainerCourse = new TrainerCourse()
                        {
                            CourseId = courseId,
                            TrainerId = trainer.TrainerId
                        };
                        trainerCourseRepository.Insert(trainerCourse);
                    }
                    trainerCourseRepository.Dispose();
                }

                return RedirectToAction("AllTrainers");
            }

            return View(trainerViewModel);
        }

        // GET: TestTrainer/Delete/5
        public ActionResult DeleteTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Dispose();
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: TestTrainer/Delete/5
        [HttpPost, ActionName("DeleteTrainer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete TrainerCourses
            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            IEnumerable<TrainerCourse> trainerCourses = trainerCourseRepository.GetAll().Where(tc => tc.TrainerId == id);
            foreach (TrainerCourse trainerCourse in trainerCourses)
            {
                trainerCourseRepository.Delete(trainerCourse);
            }
            trainerCourseRepository.Dispose();

            //Delete trainer
            TrainerRepository trainerRepository = new TrainerRepository();
            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Delete(trainer);
            trainerRepository.Dispose();

            return RedirectToAction("AllTrainers");
        }
    }
}