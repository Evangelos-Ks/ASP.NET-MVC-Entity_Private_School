using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;
using System.Security.Policy;
using PagedList;
using System.Collections.Generic;
using System;
using Assignment2.Web.Models;
using System.Net.Sockets;

namespace Assignment2.Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult AllCourses(string sort, string search, string currentFilter, int? page, int? pageSize)
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
                courses = courses.Where(c => c.Title.ToUpper().Contains(search.ToUpper()) || c.Type.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";
            ViewBag.Stream = sort == "streamAsc" ? "streamDesc" : "streamAsc";
            ViewBag.Type = sort == "typeAsc" ? "typeDesc" : "typeAsc";
            ViewBag.StartDate = sort == "startDateAsc" ? "startDateDesc" : "startDateAsc";
            ViewBag.EndDate = sort == "endDateAsc" ? "endDateDesc" : "endDateAsc";

            switch (sort)
            {
                case "courseTitleDesc":
                    courses = courses.OrderByDescending(x => x.Title);
                    break;
                case "streamAsc":
                    courses = courses.OrderBy(x => x.Stream);
                    break;
                case "streamDesc":
                    courses = courses.OrderByDescending(x => x.Stream);
                    break;
                case "typeAsc":
                    courses = courses.OrderBy(x => x.Type);
                    break;
                case "typeDesc":
                    courses = courses.OrderByDescending(x => x.Type);
                    break;
                case "startDateAsc":
                    courses = courses.OrderBy(x => x.StartDate);
                    break;
                case "startDateDesc":
                    courses = courses.OrderByDescending(x => x.StartDate);
                    break;
                case "endDateAsc":
                    courses = courses.OrderBy(x => x.EndDate);
                    break;
                case "endDateDesc":
                    courses = courses.OrderByDescending(x => x.EndDate);
                    break;
                default:
                    courses = courses.OrderBy(x => x.Title);
                    break;
            }

            ViewBag.CurrentSort = sort;
            ViewBag.CurrentFilter = search;
            ViewBag.CurrentPageSize = pSize;

            return View(courses.ToPagedList(pageNumber, pSize));
        }

        // GET: TestCourse/Details/5
        public ActionResult DetailsCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);

        }

        // GET: TestCourse/Edit/5
        public ActionResult EditCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();

            if (course == null)
            {
                return HttpNotFound();
            }

            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            var studentsCourses = studentCourseRepository.GetAll().Where(c => c.CourseId == id);
            studentCourseRepository.Dispose();
            IEnumerable<int> selectedStudentsId = studentsCourses.Select(sc => sc.StudentId);

            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            var trainersCourses = trainerCourseRepository.GetAll().Where(tc => tc.CourseId == id);
            trainerCourseRepository.Dispose();
            IEnumerable<int> selectedTrainersId = trainersCourses.Select(tc => tc.TrainerId);

            StudentRepository studentRepository = new StudentRepository();
            List<Student> allStudents = studentRepository.GetAll().ToList();
            studentRepository.Dispose();

            TrainerRepository trainerRepository = new TrainerRepository();
            List<Trainer> allTrainers = trainerRepository.GetAll().ToList();
            trainerRepository.Dispose();

            //find all students which are included in this course and remove them from the list allStudents
            List<Student> existingStudents = new List<Student>();
            foreach (var studentId in selectedStudentsId)
            {
                Student selectedStudent = allStudents.First(s => s.StudentId == studentId);
                existingStudents.Add(selectedStudent);
                allStudents.Remove(selectedStudent);
            }

            //find all trainers which are included in this course and remove them from the list allTrainers
            List<Trainer> existingTrainers = new List<Trainer>();
            foreach (var trainerId in selectedTrainersId)
            {
                Trainer selectedTrainer = allTrainers.First(t => t.TrainerId == trainerId);
                existingTrainers.Add(selectedTrainer);
                allTrainers.Remove(selectedTrainer);
            }

            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseId = (int)id,
                Stream = course.Stream,
                Type = course.Type,
                Title = course.Title,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                TuitionFees = course.CourseFees,
                StudentsForAddition = CreateSelectListOfStudents(allStudents),
                TrainersForAddition = CreateSelectListOfTrainers(allTrainers),
                StudentsForSubtraction = CreateSelectListOfStudents(existingStudents),
                TrainersForSubtraction = CreateSelectListOfTrainers(existingTrainers)
            };

            return View(courseViewModel);
        }

        // POST: TestCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(
            [Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate,TuitionFees,TrainersForSubtractionId,TrainersForAdditionId,StudentsForSubtractionId,StudentsForAdditionId")]
        CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                if (courseViewModel.TrainersForSubtractionId != null)
                {
                    TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
                    var trainersCourses = trainerCourseRepository.GetAll().Where(tc => tc.CourseId == courseViewModel.CourseId);
                    foreach (var id in courseViewModel.TrainersForSubtractionId)
                    {
                        TrainerCourse trainerCourse = trainersCourses.FirstOrDefault(tc => tc.TrainerId == Convert.ToInt32(id));
                        trainerCourseRepository.Delete(trainerCourse);
                    }
                    trainerCourseRepository.Dispose();
                }

                if (courseViewModel.TrainersForAdditionId != null)
                {
                    TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
                    foreach (var id in courseViewModel.TrainersForAdditionId)
                    {
                        TrainerCourse trainerCourse = new TrainerCourse()
                        {
                            CourseId = courseViewModel.CourseId,
                            TrainerId = Convert.ToInt32(id)
                        };
                        trainerCourseRepository.Insert(trainerCourse);
                    }
                    trainerCourseRepository.Dispose();
                }

                if (courseViewModel.StudentsForSubtractionId != null)
                {
                    StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                    var studentsCourses = studentCourseRepository.GetAll().Where(sc => sc.CourseId == courseViewModel.CourseId);
                    foreach (var id in courseViewModel.StudentsForSubtractionId)
                    {
                        StudentCourse studentCourse = studentsCourses.FirstOrDefault(sc => sc.StudentId == Convert.ToInt32(id));
                        studentCourseRepository.Delete(studentCourse);
                    }
                    studentCourseRepository.Dispose();
                }

                if (courseViewModel.StudentsForAdditionId != null)
                {
                    StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                    foreach (var id in courseViewModel.StudentsForAdditionId)
                    {
                        StudentCourse studentCourse = new StudentCourse()
                        {
                            CourseId = courseViewModel.CourseId,
                            StudentId = Convert.ToInt32(id),
                            TuitionFees = courseViewModel.TuitionFees
                        };
                        studentCourseRepository.Insert(studentCourse);
                    }
                    studentCourseRepository.Dispose();
                }

                CourseRepository courseRepository = new CourseRepository();
                Course course = courseRepository.GetById(courseViewModel.CourseId);
                course.CourseFees = courseViewModel.TuitionFees;
                course.Stream = courseViewModel.Stream;
                course.Title = courseViewModel.Title;
                course.StartDate = courseViewModel.StartDate;
                course.EndDate = courseViewModel.EndDate;
                course.Type = courseViewModel.Type;
                courseRepository.Update(course);
                courseRepository.Dispose();

                StudentCourseRepository studentCourseRepository3 = new StudentCourseRepository();
                var studentsCourses3 = studentCourseRepository3.GetAll().Where(sc => sc.CourseId == courseViewModel.CourseId);
                foreach (var studentCourse in studentsCourses3)
                {
                    studentCourse.TuitionFees = courseViewModel.TuitionFees;
                    studentCourseRepository3.Update(studentCourse);
                }
                studentCourseRepository3.Dispose();

                return RedirectToAction("AllCourses");
            }

            StudentCourseRepository studentCourseRepository2 = new StudentCourseRepository();
            var studentsCourses2 = studentCourseRepository2.GetAll().Where(c => c.CourseId == courseViewModel.CourseId);
            studentCourseRepository2.Dispose();
            IEnumerable<int> selectedStudentsId = studentsCourses2.Select(sc => sc.StudentId);

            TrainerCourseRepository trainerCourseRepository2 = new TrainerCourseRepository();
            var trainersCourses2 = trainerCourseRepository2.GetAll().Where(tc => tc.CourseId == courseViewModel.CourseId);
            trainerCourseRepository2.Dispose();
            IEnumerable<int> selectedTrainersId = trainersCourses2.Select(tc => tc.TrainerId);

            StudentRepository studentRepository2 = new StudentRepository();
            List<Student> allStudents = studentRepository2.GetAll().ToList();
            studentRepository2.Dispose();

            TrainerRepository trainerRepository2 = new TrainerRepository();
            List<Trainer> allTrainers = trainerRepository2.GetAll().ToList();
            trainerRepository2.Dispose();

            //find all students which are included in this course and remove them from the list allStudents
            List<Student> existingStudents = new List<Student>();
            foreach (var studentId in selectedStudentsId)
            {
                Student selectedStudent = allStudents.First(s => s.StudentId == studentId);
                existingStudents.Add(selectedStudent);
                allStudents.Remove(selectedStudent);
            }

            //find all trainers which are included in this course and remove them from the list allTrainers
            List<Trainer> existingTrainers = new List<Trainer>();
            foreach (var trainerId in selectedTrainersId)
            {
                Trainer selectedTrainer = allTrainers.First(t => t.TrainerId == trainerId);
                existingTrainers.Add(selectedTrainer);
                allTrainers.Remove(selectedTrainer);
            }

            courseViewModel.StudentsForAddition = CreateSelectListOfStudents(allStudents);
            courseViewModel.TrainersForAddition = CreateSelectListOfTrainers(allTrainers);
            courseViewModel.StudentsForSubtraction = CreateSelectListOfStudents(existingStudents);
            courseViewModel.TrainersForSubtraction = CreateSelectListOfTrainers(existingTrainers);

            return View(courseViewModel);
        }

        // GET: TestCourse/Create
        public ActionResult CreateCourse()
        {
            StudentRepository studentRepository = new StudentRepository();
            var students = studentRepository.GetAll();
            studentRepository.Dispose();

            TrainerRepository trainerRepository = new TrainerRepository();
            var trainers = trainerRepository.GetAll();
            trainerRepository.Dispose();

            CourseViewModel courseViewModel = new CourseViewModel();
            courseViewModel.Students = CreateSelectListOfStudents(students);
            courseViewModel.Trainers = CreateSelectListOfTrainers(trainers);

            return View(courseViewModel);
        }

        // POST: TestCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate,TuitionFees,StudentsId,TrainersId")] CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course()
                {
                    CourseId = courseViewModel.CourseId,
                    Title = courseViewModel.Title,
                    Stream = courseViewModel.Stream,
                    Type = courseViewModel.Type,
                    StartDate = courseViewModel.StartDate,
                    EndDate = courseViewModel.EndDate,
                    CourseFees = courseViewModel.TuitionFees
                };

                CourseRepository courseRepository = new CourseRepository();
                courseRepository.Insert(course);
                courseRepository.Dispose();

                if (courseViewModel.StudentsId != null)
                {
                    StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                    for (int i = 0; i < courseViewModel.StudentsId.Count(); i++)
                    {
                        StudentCourse studentCourse = new StudentCourse()
                        {
                            CourseId = course.CourseId,
                            StudentId = Convert.ToInt32(courseViewModel.StudentsId[i]),
                            TuitionFees = courseViewModel.TuitionFees
                        };
                        studentCourseRepository.Insert(studentCourse);

                    }
                    studentCourseRepository.Dispose();
                }

                if (courseViewModel.TrainersId != null)
                {
                    TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
                    for (int i = 0; i < courseViewModel.TrainersId.Count; i++)
                    {
                        TrainerCourse trainerCourse = new TrainerCourse()
                        {
                            CourseId = course.CourseId,
                            TrainerId = Convert.ToInt32(courseViewModel.TrainersId[i])
                        };
                        trainerCourseRepository.Insert(trainerCourse);
                    }
                    trainerCourseRepository.Dispose();
                }

                return RedirectToAction("AllCourses");
            }

            if (courseViewModel.Students == null)
            {
                StudentRepository studentRepository = new StudentRepository();
                var students = studentRepository.GetAll();
                studentRepository.Dispose();

                courseViewModel.Students = CreateSelectListOfStudents(students);
            }

            if (courseViewModel.Trainers == null)
            {
                TrainerRepository trainerCourseRepository = new TrainerRepository();
                var trainers = trainerCourseRepository.GetAll();
                trainerCourseRepository.Dispose();

                courseViewModel.Trainers = CreateSelectListOfTrainers(trainers);
            }

            return View(courseViewModel);
        }

        // GET: TestCourse/Delete/5
        public ActionResult DeleteCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: TestCourse/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            List<StudentCourse> studentscourses = studentCourseRepository.GetAll().Where(c => c.CourseId == id).ToList();
            foreach (var studentCourse in studentscourses)
            {
                studentCourseRepository.Delete(studentCourse);
            }
            studentCourseRepository.Dispose();

            TrainerCourseRepository trainerCourseRepository = new TrainerCourseRepository();
            List<TrainerCourse> trainerscourses = trainerCourseRepository.GetAll().Where(c => c.CourseId == id).ToList();
            foreach (var trainercourse in trainerscourses)
            {
                trainerCourseRepository.Delete(trainercourse);
            }
            trainerCourseRepository.Dispose();

            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.GetById(id);
            courseRepository.Delete(course);
            courseRepository.Dispose();

            return RedirectToAction("AllCourses");
        }

        protected IEnumerable<SelectListItem> CreateSelectListOfStudents(IEnumerable<Student> students)
        {
            var selectlist = students.Select(s =>
                                new SelectListItem()
                                {
                                    Value = s.StudentId.ToString(),
                                    Text = string.Format(s.FirstName + " " + s.LastName)
                                })
                                 .OrderBy(s => s.Text)
                                 .ToList();

            return selectlist;
        }

        protected IEnumerable<SelectListItem> CreateSelectListOfTrainers(IEnumerable<Trainer> trainers)
        {
            var selectlist = trainers.Select(t =>
                                new SelectListItem()
                                {
                                    Value = t.TrainerId.ToString(),
                                    Text = string.Format(t.FirstName + " " + t.LastName)
                                })
                                 .OrderBy(t => t.Text)
                                 .ToList();

            return selectlist;
        }
    }
}