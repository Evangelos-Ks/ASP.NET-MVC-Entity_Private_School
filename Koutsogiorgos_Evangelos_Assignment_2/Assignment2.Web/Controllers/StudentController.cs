using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Collections.Generic;
using PagedList;
using Assignment2.Web.Models;
using System.IO;
using System;

namespace Assignment2.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult AllStudents(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            StudentRepository studentRepository = new StudentRepository();
            var students = studentRepository.GetAll();
            studentRepository.Dispose();

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
                students = students.Where(n => n.FirstName.ToUpper().Contains(search.ToUpper()) || n.LastName.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.FirstName = string.IsNullOrEmpty(sort) ? "firstNameDesc" : "";
            ViewBag.LastName = sort == "lastNameAsc" ? "lastNameDesc" : "lastNameAsc";
            ViewBag.DateOfBirth = sort == "dateOfBirthAsc" ? "dateOfBirthDesc" : "dateOfBirthAsc";

            switch (sort)
            {
                case "firstNameDesc":
                    students = students.OrderByDescending(x => x.FirstName);
                    break;
                case "lastNameAsc":
                    students = students.OrderBy(x => x.LastName);
                    break;
                case "lastNameDesc":
                    students = students.OrderByDescending(x => x.LastName);
                    break;
                case "dateOfBirthAsc":
                    students = students.OrderBy(x => x.DateOfBirth);
                    break;
                case "dateOfBirthDesc":
                    students = students.OrderByDescending(x => x.DateOfBirth);
                    break;
                default:
                    students = students.OrderBy(x => x.FirstName);
                    break;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(students.ToPagedList(pageNumber, pSize));
        }

        // GET: TestStudent/Details/5
        public ActionResult DetailsStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            studentRepository.Dispose();

            return View(student);

        }

        // GET: TestStudent/Edit/5
        public ActionResult EditStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            studentRepository.Dispose();

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: TestStudent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,PhotoUrl")] Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository();
                studentRepository.Update(student);
                studentRepository.Dispose();
                return RedirectToAction("AllStudents");
            }
            return View(student);
        }

        // GET: TestStudent/Create
        public ActionResult CreateStudent()
        {
            CourseRepository courseRepository = new CourseRepository();
            IEnumerable<Course> courses = courseRepository.GetAll();
            courseRepository.Dispose();

            StudentViewModel courseViewModel = new StudentViewModel()
            {
                AllCourses = CreateSelectListOfCourses(courses)
            };

            return View(courseViewModel);
        }

        // POST: TestStudent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,PhotoUrl,Discount,AllCoursesId,ImageFile")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                //Save upload file
                if (studentViewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(studentViewModel.ImageFile.FileName);
                    string extention = Path.GetExtension(studentViewModel.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyymmddmm") + extention;
                    studentViewModel.PhotoUrl = "../../Content/Students_Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Students_Image/"), fileName);
                    studentViewModel.ImageFile.SaveAs(fileName);
                }

                //Create and insert a new student
                Student student = new Student()
                {
                    StudentId = studentViewModel.StudentId,
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                    DateOfBirth = studentViewModel.DateOfBirth,
                    Discount = studentViewModel.Discount,
                    PhotoUrl = studentViewModel.PhotoUrl
                };

                StudentRepository studentRepository = new StudentRepository();
                studentRepository.Insert(student);
                studentRepository.Dispose();

                //Create and Insert studentCourses
                if (studentViewModel.AllCoursesId != null)
                {
                    StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                    foreach (var courseId in studentViewModel.AllCoursesId)
                    {
                        StudentCourse studentCourse = new StudentCourse()
                        {
                            CourseId = courseId,
                            StudentId = student.StudentId
                        };
                        studentCourseRepository.Insert(studentCourse);
                    }
                    studentCourseRepository.Dispose();
                }

                return RedirectToAction("AllStudents");
            }

            //Populate Selectlist of courses
            CourseRepository courseRepository = new CourseRepository();
            IEnumerable<Course> courses = courseRepository.GetAll();
            courseRepository.Dispose();

            StudentViewModel courseViewModel = new StudentViewModel()
            {
                AllCourses = CreateSelectListOfCourses(courses)
            };

            return View(studentViewModel);
        }

        // GET: TestStudent/Delete/5
        public ActionResult DeleteStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            studentRepository.Dispose();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: TestStudent/Delete/5
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            //Delete studentCourse
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            List<StudentCourse> studentCourses = studentCourseRepository.GetAll().Where(sc => sc.StudentId == id).ToList();
            foreach (var studentCourse in studentCourses)
            {
                studentCourseRepository.Delete(studentCourse);
            }
            studentCourseRepository.Dispose();

            //Delete student
            StudentRepository studentRepository = new StudentRepository();
            Student student = studentRepository.GetById(id);
            studentRepository.Delete(student);
            studentRepository.Dispose();

            return RedirectToAction("AllStudents");
        }

        //============================================== Protected Methods =================================================
        protected IEnumerable<SelectListItem> CreateSelectListOfCourses(IEnumerable<Course> Courses)
        {
            var selectList = Courses.Select(c => new SelectListItem
                                                {
                                                    Value = c.CourseId.ToString(),
                                                    Text = c.Title
                                                })
                                                .OrderBy(o => o.Text);
            return selectList;
        }

    }
}