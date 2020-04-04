using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;

namespace Assignment2.Web.Controllers
{
    public class AssignmentController : Controller
    {
        // GET: Assignment
        public ActionResult AllAssignments()
        {
            AssignmentRepository assignmentRepository = new AssignmentRepository();
            var assignments = assignmentRepository.GetAll();
            //assignmentRepository.Dispose();

            assignments = assignments.OrderBy(x => x.Title);

            return View(assignments);
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
            //AssignmentRepository.Dispose();

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