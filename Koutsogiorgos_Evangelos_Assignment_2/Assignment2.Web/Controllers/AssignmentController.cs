using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Collections.Generic;
using PagedList;

namespace Assignment2.Web.Controllers
{
    public class AssignmentController : Controller
    {
        // GET: Assignment
        public ActionResult AllAssignments(string sort, string search, string currentFilter, int? page, int? pageSize, int? currentPageSize)
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            var assignments = assignmentRepository.GetAll();
            assignmentRepository.Dispose();

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
                assignments = assignments.Where(x => x.Course.Title.ToUpper().Contains(search.ToUpper()) ||
                x.Title.ToUpper().Contains(search.ToUpper()) || x.Description.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
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
                    assignments = assignments.OrderBy(x => x.Course.Title);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = assignmentRepository.GetById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            assignmentRepository.Dispose();

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

            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            ViewBag.CourseId = new SelectList(courses, "CourseId", "Title", assignment.CourseId);


            assignmentRepository.Dispose();
            courseRepository.Dispose();

            if (assignment == null)
            {
                return HttpNotFound();
            }

            return View(assignment);
        }

        // POST: TestAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAssignment([Bind(Include = "AssignmentId,Title,Description,SubDateTime,CourseId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository();
                assignmentRepository.Update(assignment);
                assignmentRepository.Dispose();
                return RedirectToAction("AllAssignments");
            }
            return View(assignment);
        }

        // GET: TestAssignment/Create
        public ActionResult CreateAssignment()
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            ViewBag.CourseId = new SelectList(courses, "CourseId", "Title");
            courseRepository.Dispose();
            return View();
        }

        // POST: TestAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssignment([Bind(Include = "AssignmentId,Title,Description,SubDateTime,CourseId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository();
                assignmentRepository.Insert(assignment);
                assignmentRepository.Dispose();
                return RedirectToAction("AllAssignments");
            }

            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            ViewBag.CourseId = new SelectList(courses, "CourseId", "Title", assignment.CourseId);
            courseRepository.Dispose();

            return View(assignment);
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
            AssignmentRepository assignmentRepository = new AssignmentRepository();

            Assignment assignment = assignmentRepository.GetById(id);
            assignmentRepository.Delete(assignment);
            assignmentRepository.Dispose();
            return RedirectToAction("AllAssignments");
        }
    }
}