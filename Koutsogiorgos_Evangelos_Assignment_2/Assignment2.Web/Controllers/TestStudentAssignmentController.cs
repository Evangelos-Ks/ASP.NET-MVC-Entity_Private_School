using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Database;
using Assignment2.Entities;

namespace Assignment2.Web.Controllers
{
    public class TestStudentAssignmentController : Controller
    {
        private MyDatabase db = new MyDatabase();

        // GET: TestStudentAssignment
        public ActionResult Index()
        {
            var studentsAssignments = db.StudentsAssignments.Include(s => s.Assignment).Include(s => s.Student);
            return View(studentsAssignments.ToList());
        }

        // GET: TestStudentAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssignment studentAssignment = db.StudentsAssignments.Find(id);
            if (studentAssignment == null)
            {
                return HttpNotFound();
            }
            return View(studentAssignment);
        }

        // GET: TestStudentAssignment/Create
        public ActionResult Create()
        {
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName");
            return View();
        }

        // POST: TestStudentAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId,StudentId,OralMark,TotalMark")] StudentAssignment studentAssignment)
        {
            if (ModelState.IsValid)
            {
                db.StudentsAssignments.Add(studentAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", studentAssignment.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // GET: TestStudentAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssignment studentAssignment = db.StudentsAssignments.Find(id);
            if (studentAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", studentAssignment.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // POST: TestStudentAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentId,StudentId,OralMark,TotalMark")] StudentAssignment studentAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", studentAssignment.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // GET: TestStudentAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssignment studentAssignment = db.StudentsAssignments.Find(id);
            if (studentAssignment == null)
            {
                return HttpNotFound();
            }
            return View(studentAssignment);
        }

        // POST: TestStudentAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAssignment studentAssignment = db.StudentsAssignments.Find(id);
            db.StudentsAssignments.Remove(studentAssignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
