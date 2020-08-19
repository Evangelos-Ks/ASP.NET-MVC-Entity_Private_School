using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Collections.Generic;
using PagedList;
using Assignment2.Web.Models;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Web.Controllers
{
    public class AssignmentController : Controller
    {
        // GET: Assignment
        public ActionResult AllAssignments(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            var assignments = assignmentRepository.GetAll();
            assignmentRepository.Dispose();

            //Paging
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

            //searching
            if (!string.IsNullOrEmpty(search))
            {
                assignments = assignments.Where(x => x.Course.Title.ToUpper().Contains(search.ToUpper()) ||
                x.Title.ToUpper().Contains(search.ToUpper()) || x.Description.ToUpper().Contains(search.ToUpper()));
            }

            //sorting
            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";
            ViewBag.AssignmentTitle = sort == "assignmentTitleAsc" ? "assignmentTitleDesc" : "assignmentTitleAsc";
            ViewBag.Description = sort == "descriptionAsc" ? "descriptionDesc" : "descriptionAsc";
            ViewBag.SubmissionDate = sort == "submissionDateAsc" ? "submissionDateDesc" : "submissionDateAsc";

            switch (sort)
            {
                case "courseTitleDesc":
                    assignments = assignments.OrderByDescending(x => x.Course.Title);
                    break;
                case "assignmentTitleAsc":
                    assignments = assignments.OrderBy(x => x.Title);
                    break;
                case "assignmentTitleDesc":
                    assignments = assignments.OrderByDescending(x => x.Title);
                    break;
                case "descriptionAsc":
                    assignments = assignments.OrderBy(x => x.Description);
                    break;
                case "descriptionDesc":
                    assignments = assignments.OrderByDescending(x => x.Description);
                    break;
                case "submissionDateAsc":
                    assignments = assignments.OrderBy(x => x.SubDateTime);
                    break;
                case "submissionDateDesc":
                    assignments = assignments.OrderByDescending(x => x.SubDateTime);
                    break;
                default:
                    assignments = assignments.OrderBy(x => x.Title);
                    break;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(assignments.ToPagedList(pageNumber, pSize));
        }

        // GET: TestAssignment/Details/5
        public ActionResult DetailsAssignment(int? id)
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            Assignment assignment = assignmentRepository.GetById(id);
            assignmentRepository.Dispose();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (assignment == null)
            {
                return HttpNotFound();
            }

            return View(assignment);

        }

        // GET: TestAssignment/Edit/5
        public ActionResult EditAssignment(int? id)
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = assignmentRepository.GetById(id);
            assignmentRepository.Dispose();

            if (assignment == null)
            {
                return HttpNotFound();
            }

            //Find Existing students
            StudentAssignmentRepository studentAssignmentRepository = new StudentAssignmentRepository();
            List<Student> existingStudents = studentAssignmentRepository.GetAll()
                                                    .Where(sa => sa.AssignmentId == id)
                                                    .Select(sa => sa.Student).ToList();
            studentAssignmentRepository.Dispose();

            //Find not existing students which have assigned in the assignment's course
            int? courseId = assignment.CourseId;
            List<Student> studentsPerCourse = new List<Student>();
            if (courseId != null)
            {
                CourseRepository courseRepository = new CourseRepository();
                studentsPerCourse = courseRepository.GetById(courseId).StudentCourses
                                                       .Select(sc => sc.Student).ToList();
                courseRepository.Dispose();

                foreach (Student student in existingStudents)
                {
                    Student findStudent = studentsPerCourse.Find(s => s.StudentId == student.StudentId);
                    studentsPerCourse.Remove(findStudent);
                }
            }

            //Create assignmentViewModel
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                AssignmentId = assignment.AssignmentId,
                Description = assignment.Description,
                SubDateTime = assignment.SubDateTime,
                Title = assignment.Title,
                CourseId = assignment.CourseId,
                Students = Methods.CreateSelectListOfStudents(studentsPerCourse),
                ExistingStudents = Methods.CreateSelectListOfStudents(existingStudents)
            };

            return View(assignmentViewModel);
        }

        // POST: TestAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAssignment([Bind(Include = "AssignmentId,Title,Description,SubDateTime,CourseId,ExistingStudentsId,StudentsId")] AssignmentViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Assignment
                Assignment assignment = new Assignment()
                {
                    AssignmentId = assignmentViewModel.AssignmentId,
                    Title = assignmentViewModel.Title,
                    Description = assignmentViewModel.Description,
                    CourseId = assignmentViewModel.CourseId,
                    SubDateTime = assignmentViewModel.SubDateTime
                };

                //Insert assignment
                AssignmentRepository assignmentRepository = new AssignmentRepository();
                assignmentRepository.Update(assignment);
                assignmentRepository.Dispose();

                //Update the realation student-assignment
                StudentAssignmentRepository studentAssignmentRepository = new StudentAssignmentRepository();
                //Remove students from assignments
                if (assignmentViewModel.ExistingStudentsId != null)
                {
                    List<StudentAssignment> allStudentAssignments = studentAssignmentRepository.GetAll().ToList();
                    foreach (int studentId in assignmentViewModel.ExistingStudentsId)
                    {
                        StudentAssignment studentAssignment = allStudentAssignments.Find(sa => sa.StudentId == studentId);
                        studentAssignmentRepository.Delete(studentAssignment);
                    }
                }
                //Add student into assignments
                if (assignmentViewModel.StudentsId != null)
                {
                    foreach (int studentId in assignmentViewModel.StudentsId)
                    {
                        StudentAssignment studentAssignment = new StudentAssignment()
                        {
                            StudentId = studentId,
                            AssignmentId = assignment.AssignmentId
                        };
                        studentAssignmentRepository.Insert(studentAssignment);
                    }
                }
                studentAssignmentRepository.Dispose();

                return RedirectToAction("AllAssignments");
            }

            //Find Existing students
            StudentAssignmentRepository studentAssignmentRepository2 = new StudentAssignmentRepository();
            List<Student> existingStudents = studentAssignmentRepository2.GetAll()
                                                    .Where(sa => sa.AssignmentId == assignmentViewModel.AssignmentId)
                                                    .Select(sa => sa.Student).ToList();
            studentAssignmentRepository2.Dispose();

            //Find not existing students which have assigned in the assignment's course
            int? courseId = assignmentViewModel.CourseId;
            List<Student> studentsPerCourse = new List<Student>();
            if (courseId != null)
            {
                CourseRepository courseRepository = new CourseRepository();
                studentsPerCourse = courseRepository.GetById(courseId).StudentCourses
                                                       .Select(sc => sc.Student).ToList();
                courseRepository.Dispose();

                foreach (Student student in existingStudents)
                {
                    studentsPerCourse.Remove(student);
                }
            }

            //Create selectListItems
            assignmentViewModel.Students = Methods.CreateSelectListOfStudents(studentsPerCourse);
            assignmentViewModel.ExistingStudents = Methods.CreateSelectListOfStudents(existingStudents);

            return View(assignmentViewModel);
        }

        // GET: TestAssignment/Create
        public ActionResult CreateAssignment()
        {
            //Get courses
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll().OrderBy(c => c.CourseId);
            courseRepository.Dispose();

            //Get students
            StudentRepository studentRepository = new StudentRepository();
            IEnumerable<Student> students = studentRepository.GetAll();
            studentRepository.Dispose();

            //Get studentCourses
            StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
            IEnumerable<StudentCourse> studentCourses = studentCourseRepository.GetAll();
            studentCourseRepository.Dispose();

            //Create assignmentViewModel
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                Courses = Methods.CreateSelectListOfCourses(courses)
            };

            return View(assignmentViewModel);
        }

        // POST: TestAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssignment([Bind(Include = "AssignmentId,Title,Description,SubDateTime,CourseId,FinalSubmit,StudentsId")] AssignmentViewModel assignmentVM)
        {
            if (assignmentVM.FinalSubmit == true)
            {
                if (ModelState.IsValid)
                {
                    //Create assignment
                    Assignment assignment = new Assignment()
                    {
                        AssignmentId = assignmentVM.AssignmentId,
                        Description = assignmentVM.Description,
                        SubDateTime = assignmentVM.SubDateTime,
                        Title = assignmentVM.Title,
                        CourseId = assignmentVM.CourseId
                    };

                    //Insert assignment
                    AssignmentRepository assignmentRepository = new AssignmentRepository();
                    assignmentRepository.Insert(assignment);
                    assignmentRepository.Dispose();

                    //Create studentAssignment and insert
                    if (assignmentVM.StudentsId != null)
                    {
                        StudentAssignmentRepository studentAssignmentRepository = new StudentAssignmentRepository();
                        foreach (int studentId in assignmentVM.StudentsId)
                        {
                            StudentAssignment studentAssignment = new StudentAssignment()
                            {
                                StudentId = studentId,
                                AssignmentId = assignment.AssignmentId
                            };
                            studentAssignmentRepository.Insert(studentAssignment);
                        }
                        studentAssignmentRepository.Dispose();
                    }

                    return RedirectToAction("AllAssignments");
                }
            }

            //Get all courses
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            courseRepository.Dispose();

            //Create selectItems of courses
            assignmentVM.Courses = Methods.CreateSelectListOfCourses(courses);

            if (assignmentVM.CourseId != null)
            {
                //Get students
                StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                List<Student> students = studentCourseRepository.GetAll().Where(sc => sc.CourseId == assignmentVM.CourseId)
                                        .Select(sc => sc.Student).ToList();
                studentCourseRepository.Dispose();

                //Create selectItems of Students
                assignmentVM.Students = Methods.CreateSelectListOfStudents(students);
            }

            return View(assignmentVM);
        }

        // GET: TestAssignment/Delete/5
        public ActionResult DeleteAssignment(int? id)
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = assignmentRepository.GetById(id);
            assignmentRepository.Dispose();
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: TestAssignment/Delete/5
        [HttpPost, ActionName("DeleteAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete studentAssignments
            StudentAssignmentRepository studentAssignmentRepository = new StudentAssignmentRepository();
            IEnumerable<StudentAssignment> studentAssignments = studentAssignmentRepository.GetAll()
                                                                .Where(sa => sa.AssignmentId == id);
            foreach (StudentAssignment studentAssignment in studentAssignments)
            {
                studentAssignmentRepository.Delete(studentAssignment);
            }
            studentAssignmentRepository.Dispose();

            //Delete assignment
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            Assignment assignment = assignmentRepository.GetById(id);
            assignmentRepository.Delete(assignment);
            assignmentRepository.Dispose();

            return RedirectToAction("AllAssignments");
        }
    }
}