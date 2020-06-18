using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;
using System.Security.Policy;

namespace Assignment2.Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult AllCourses(string sort)
        {
            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";
            ViewBag.Stream = sort == "streamAsc" ? "streamDesc" : "streamAsc";
            ViewBag.Type = sort == "typeAsc" ? "typeDesc" : "typeAsc";

            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            courseRepository.Dispose();

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
                default:
                    courses = courses.OrderBy(x => x.Title);
                    break;
            }


            return View(courses);
        }

        // GET: TestCourse/Details/5
        public ActionResult DetailsCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.GetById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            //CourseRepository.Dispose();

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

            return View(course);
        }

        // POST: TestCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.Update(course);
                courseRepository.Dispose();
                return RedirectToAction("AllCourses");
            }
            return View(course);
        }

        // GET: TestCourse/Create
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: TestCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.Insert(course);
                courseRepository.Dispose();
                return RedirectToAction("AllCourses");
            }

            return View(course);
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
            CourseRepository courseRepository = new CourseRepository();

            Course course = courseRepository.GetById(id);
            courseRepository.Delete(course);
            courseRepository.Dispose();
            return RedirectToAction("AllCourses");
        }
    }
}